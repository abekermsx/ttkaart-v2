using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ttkaart.ViewModels;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem = "Competities")]
    public class PouleController : TtkaartController
    {
        public ActionResult Index(int? season, int? category, int? region, string seasonName, string categoryName, string regionName)
        {
            var s = new Service.PouleService();

            PoulesViewModel model = new PoulesViewModel();

            if ( season == null || category == null || region == null )
            {
                season = -1;
                category = -1;
                region = -1;

                var seasonCookie = Request.Cookies.Get("season");
                var categoryCookie = Request.Cookies.Get("category");
                var regionCookie = Request.Cookies.Get("region");

                if (seasonCookie != null) season = Int32.Parse(seasonCookie.Value);
                if (categoryCookie != null) category = Int32.Parse(categoryCookie.Value);
                if (regionCookie != null) region = Int32.Parse(regionCookie.Value);
            }
            
            model.seasons = s.GetSeasonsSelect();

            if (model.seasons.Where(c => c.Value == season.ToString()).Count() == 0)
                season = Int32.Parse(model.seasons[0].Value);

            model.categories = s.GetCategorySelect((int)season);
            
            if (model.categories.Where(c => c.Value == category.ToString()).Count() == 0)
                category = Int32.Parse(model.categories[0].Value);
            
            model.regions = s.GetRegionSelect((int)season, (int)category);

            if (model.regions.Where(c => c.Value == region.ToString()).Count() == 0)
                region = Int32.Parse(model.regions[0].Value);

            model.poules = s.GetPoules( (int)season, (int)category, (int)region);

            model.seasons.Where(c => c.Value == season.ToString()).First().Selected = true;
            model.categories.Where(c => c.Value == category.ToString()).First().Selected = true;
            model.regions.Where(c => c.Value == region.ToString()).First().Selected = true;


            model.name = model.seasons.Where(c => c.Value == season.ToString()).Single().Text + " - " +
                         model.categories.Where(c => c.Value == category.ToString()).Single().Text + " - " +
                         model.regions.Where(c => c.Value == region.ToString()).Single().Text;

            
            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Klasse":
                        if (sortDir.Equals("Oplopend")) model.poules = model.poules.OrderBy(m => m.classLevel).ThenBy(m => m.pouleName).ToList();
                        else model.poules = model.poules.OrderByDescending(m => m.classLevel).ThenByDescending(m => m.pouleName).ToList();
                        break;

                    case "Sterkte":
                        if (sortDir.Equals("Oplopend")) model.poules = model.poules.OrderBy(m => m.strength).ToList();
                        else model.poules = model.poules.OrderByDescending(m => m.strength).ToList();
                        break;
                }
            }

            Response.Cookies.Remove("season");
            Response.Cookies.Remove("category");
            Response.Cookies.Remove("region");

            Response.Cookies.Add(new HttpCookie("season", season.ToString()));
            Response.Cookies.Add(new HttpCookie("category", category.ToString()));
            Response.Cookies.Add(new HttpCookie("region", region.ToString()));

            return View(model);
        }



        public ActionResult Details(int id)
        {
            var s = new Service.PouleService();

            PouleViewModel model = s.GetPouleById(id);

            if (model == null)
                return NotFound();

            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (String.IsNullOrEmpty(sortField))
            {
                sortField = "Percentage";
                sortDir = "Aflopend";
            }

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Bondsnummer":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.playerNr).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.playerNr).ToList();
                        break;

                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.playerName).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.playerName).ToList();
                        break;

                    case "Team":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.clubName).ThenBy(m => m.teamNumber).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.clubName).ThenByDescending(m => m.teamNumber).ToList();
                        break;
                        
                    case "Gespeeld":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.setsPlayed).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.setsPlayed).ToList();
                        break;

                    case "Gewonnen":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.setsWon).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.setsWon).ToList();
                        break;

                    case "Percentage":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.sortPercentage).ThenBy(m => m.setsWon).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.sortPercentage).ThenByDescending(m => m.setsWon).ToList();
                        break;

                    case "Basisrating":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.baseRating).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.baseRating).ToList();
                        break;

                    case "Rating":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.rating).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.rating).ToList();
                        break;

                    case "Licentie":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.license).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.license).ToList();
                        break;
                        
                    case "TeamNaam":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.clubName).ThenBy(m => m.teamNumber).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.clubName).ThenByDescending(m => m.teamNumber).ToList();
                        break;
                        
                    case "TeamRating":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.averageRating).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.averageRating).ToList();
                        break;

                    default:
                        break;
                }
            }

            return View(model);
        }

    }
}
