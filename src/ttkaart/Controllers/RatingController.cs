using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ttkaart.DAL;
using ttkaart.Filters;
using ttkaart.ViewModels;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem = "Ratings")]
    public class RatingController : TtkaartController
    {

        private List<SelectListItem> GetRegionSelect()
        {
            List<SelectListItem> regionSelectList = new List<SelectListItem>();

            List<Region> regions = Database.Instance.regionRepository.GetRegions();

            regions = regions.Where(r => r.name != "Zwolle").ToList();
            regions = regions.Where(r => r.name != "Utrecht").ToList();

            foreach (Region region in regions)
            {
                regionSelectList.Add(new SelectListItem()
                                        {
                                            Value = region.id.ToString(),
                                            Text = region.name
                                        } 
                                    );
            }

            return regionSelectList;
        }


        private List<SelectListItem> GetCategorySelect(List<RatingViewModel> ratings)
        {
            List<SelectListItem> categorySelectList = new List<SelectListItem>();

            if ( ratings.Any(r => r.category == 1 && !r.className.Contains("Duo") ) )
                categorySelectList.Add( new SelectListItem() { Value = "Senioren", Text = "Senioren" } );

            if (ratings.Any(r => r.category == 1 && r.className.Contains("Duo")))
                categorySelectList.Add(new SelectListItem() { Value = "Senioren-Duo", Text = "Senioren Duo" });

            if (ratings.Any(r => r.category == 2) )
                categorySelectList.Add(new SelectListItem() { Value = "Junioren", Text = "Junioren" });

            if ( categorySelectList.Count > 1)
                categorySelectList.Insert(0, new SelectListItem() { Value = "Totaaloverzicht", Text = "Totaaloverzicht" });

            return categorySelectList;
        }

        public ActionResult Explanation()
        {
            return View();
        }


        public ActionResult Season(int ?season, string seasonName, string category)
        {
            var service = new Service.PouleService();

            var model = new SeasonRatingViewModel();

            model.seasons = service.GetSeasonsSelect().Where(s => Convert.ToInt32(s.Text.Split(' ')[1]) >= 2006).ToList();

            if (season == null) season = Convert.ToInt32(model.seasons.First().Value);

            model.seasonName = model.seasons.Single(r => r.Value == season.ToString()).Text;

            var ratings = Database.Instance.ratingRepository.GetRatingsBySeason((int)season);

            model.categories = GetCategorySelect(ratings);

            if (String.IsNullOrEmpty(category))
                category = model.categories[0].Text;

            if (model.categories.Count(c => c.Value == category) == 0)
                category = model.categories[0].Text;

            switch (category)
            {
                case "Totaaloverzicht":
                    model.ratings = ratings;
                    break;
                case "Junioren":
                    model.ratings = ratings.Where(r => r.category == 2).ToList();
                    break;
                case "Senioren":
                    model.ratings = ratings.Where(r => r.category == 1 && !r.className.Contains("Duo")).ToList();
                    break;
                case "Senioren-Duo":
                    model.ratings = ratings.Where(r => r.category == 1 && r.className.Contains("Duo")).ToList();
                    break;
            }

            return View(model);
        }

        public ActionResult Region(int? region, string regionName, string category)
        {
            var model = new RegionRatingViewModel();

            model.regions = GetRegionSelect();

            if (region == null) region = Convert.ToInt32(model.regions.First().Value);

            model.regionName = model.regions.Single(r => r.Value == region.ToString()).Text;

            var ratings = Database.Instance.ratingRepository.GetRatingsByRegion( (int)region );

            model.ratings = ratings;

            model.categories = GetCategorySelect(ratings);
            model.categories.Add(new SelectListItem() { Value = "Puntentabellen", Text = "Puntentabellen" });

            if (String.IsNullOrEmpty(category))
                category = model.categories[0].Text;

            if (model.categories.Count(c => c.Value == category) == 0)
                category = model.categories[0].Text;

            switch (category)
            {
                case "Puntentabellen":
                    model.ratings = ratings;
                    break;
                case "Totaaloverzicht":
                    model.ratings = ratings;
                    break;
                case "Junioren":
                    model.ratings = ratings.Where(r => r.category == 2).ToList();
                    break;
                case "Senioren":
                    model.ratings = ratings.Where(r => r.category == 1 && !r.className.Contains("Duo")).ToList();
                    break;
                case "Senioren-Duo":
                    model.ratings = ratings.Where(r => r.category == 1 && r.className.Contains("Duo")).ToList();
                    break;
            }

            model.selectedCategory = category;

            if (category == "Puntentabellen")
            {
                model.ratingsAdults = ratings.Where(r => r.category == 1 && !r.className.Contains("Duo")).ToList();
                model.ratingsAdultsDuo = ratings.Where(r => r.category == 1 && r.className.Contains("Duo")).ToList();
                model.ratingsYouth = ratings.Where(r => r.category == 2).ToList();
            }

            return View(model);
        }

    }
}
