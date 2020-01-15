using System;
using System.Linq;
using System.Web.Mvc;
using ttkaart.Filters;
using ttkaart.ViewModels;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem = "Statistieken")]
    public class StatisticsController : TtkaartController
    {
        //
        // GET: /Statistics/

        private PlayerStatisticsViewModel SortData(PlayerStatisticsViewModel model)
        {
            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                if (sortField.Equals("Afdeling"))
                {
                    if (sortDir.Equals("Oplopend")) model.regions = model.regions.OrderBy(id => id).ToList();
                    else model.regions = model.regions.OrderByDescending(id => id).ToList();
                }

                if (sortField.StartsWith("vj") || sortField.StartsWith("nj"))
                {
                    int seasonPeriod = 1;
                    if (sortField.StartsWith("nj"))
                        seasonPeriod = 2;

                    int seasonYear = Convert.ToInt32(sortField.Substring(2));

                    var seasonResults = model.playersPerRegionPerSeason.Where(s => s.seasonYear == seasonYear && s.seasonPeriod == seasonPeriod);

                    if (sortDir.Equals("Oplopend")) model.regions = seasonResults.OrderBy(s => s.playerCount).Select(r => r.regionName).ToList();
                    else model.regions = seasonResults.OrderByDescending(s => s.playerCount).Select(r => r.regionName).ToList();

                    var regions = model.playersPerRegionPerSeason.Select(r => r.regionName).Distinct().OrderBy(regionName => regionName).ToList();

                    foreach (var region in regions)
                    {
                        if (!model.regions.Contains(region))
                            model.regions.Add(region);
                    }
                }
            }

            return model;
        }



        public ActionResult Players()
        {
            var service = new Service.StatisticsService();

            var model = service.GetTotalPlayersStatistics();

            model = SortData(model);

            return View(model);
        }

        public ActionResult PlayersYouth()
        {
            var service = new Service.StatisticsService();

            var model = service.GetYouthPlayersStatistics();

            model = SortData(model);

            return View(model);
        }


        public ActionResult PlayersAdult()
        {
            var service = new Service.StatisticsService();

            var model = service.GetAdultPlayersStatistics();

            model = SortData(model);

            return View(model);
        }


        public ActionResult Clubs()
        {
            var service = new Service.StatisticsService();

            var model = service.GetClubStatistics();

            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                if (sortField.Equals("Afdeling"))
                {
                    if (sortDir.Equals("Oplopend")) model.regions = model.regions.OrderBy(id => id).ToList();
                    else model.regions = model.regions.OrderByDescending(id => id).ToList();
                }

                if (sortField.StartsWith("vj") || sortField.StartsWith("nj"))
                {
                    int seasonPeriod = 1;
                    if (sortField.StartsWith("nj"))
                        seasonPeriod = 2;

                    int seasonYear = Convert.ToInt32(sortField.Substring(2));

                    var seasonResults = model.clubsPerRegionPerSeason.Where(s => s.seasonYear == seasonYear && s.seasonPeriod == seasonPeriod);

                    if (sortDir.Equals("Oplopend")) model.regions = seasonResults.OrderBy(s => s.clubCount).Select(r => r.regionName).ToList();
                    else model.regions = seasonResults.OrderByDescending(s => s.clubCount).Select(r => r.regionName).ToList();

                    var regions = model.clubsPerRegionPerSeason.Select(r => r.regionName).Distinct().OrderBy(regionName => regionName).ToList();

                    foreach (var region in regions)
                    {
                        if (!model.regions.Contains(region))
                            model.regions.Add(region);
                    }
                }
            }

            return View(model);
        }
    }
}
