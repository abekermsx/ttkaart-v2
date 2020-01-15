using System.Collections.Generic;
using System.Linq;
using ttkaart.ViewModels;
using ttkaart.DAL;
using System.Web.Mvc;

namespace ttkaart.Service
{
    public class PouleService
    {
        public List<SelectListItem> GetSeasonsSelect()
        {
            List<SelectListItem> seasonSelectList = new List<SelectListItem>();

            List<Season> seasons = Database.Instance.seasonRepository.GetSeasons();

            foreach (Season season in seasons)
            {
                seasonSelectList.Add(new SelectListItem()
                                        {
                                            Value = season.id.ToString(),
                                            Text = (season.period == 1 ? "Voorjaar" : "Najaar") + " " + season.year.ToString()
                                        }
                                    );
            }

            return seasonSelectList;
        }

        public List<SelectListItem> GetCategorySelect(int seasonId)
        {
            List<SelectListItem> categorySelectList = new List<SelectListItem>();

            List<int> categories = Database.Instance.seasonRepository.GetCategoriesBySeason(seasonId);

            foreach (int category in categories)
            {
                categorySelectList.Add(new SelectListItem() 
                                            {
                                                Value = category.ToString(), 
                                                Text = (category == 1 ? "Senioren" : "Junioren") 
                                            }
                                      );
            }
            
            return categorySelectList;
        }

        public List<SelectListItem> GetRegionSelect(int seasonId, int categoryId)
        {
            List<SelectListItem> regionSelectList = new List<SelectListItem>();

            List<Region> regions = Database.Instance.regionRepository.GetRegionsBySeasonAndCategory(seasonId, categoryId);

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

        public List<PouleListViewModel> GetPoules(int seasonId, int categoryId, int regionId)
        {
            return Database.Instance.pouleRepository.GetPoules(seasonId, categoryId, regionId);
        }


        public PouleViewModel GetPouleById(int id)
        {
            PouleViewModel poule = new PouleViewModel();

            poule.playerResults = Database.Instance.resultRepository.GetResultsByPoule(id);

            if (poule.playerResults.Count == 0)
                return null;

            poule.teamResults = Database.Instance.teamRepository.GetTeamsByPoule(id);
            
            poule.teamResults = poule.teamResults.OrderByDescending(m => m.averageRating).ToList();

            return poule;
        }

    }
}