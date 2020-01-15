using System.Collections.Generic;
using System.Linq;
using ttkaart.DAL;
using ttkaart.Models;
using ttkaart.ViewModels;

namespace ttkaart.Service
{
    public class StatisticsService
    {
        public PlayerStatisticsViewModel GetTotalPlayersStatistics()
        {
            var model = new PlayerStatisticsViewModel();

            model.playersPerRegionPerSeason = Database.Instance.statisticsRepository.GetPlayersPerRegionPerSeason();
            model.playersPerSeason = Database.Instance.statisticsRepository.GetPlayersPerSeason();

            return GetPlayerStatistics(model);
        }

        private PlayerStatisticsViewModel GetPlayerStatistics(PlayerStatisticsViewModel model)
        {
            model.regions = model.playersPerRegionPerSeason.Select(r => r.regionName).Distinct().OrderBy(regionName => regionName).ToList();

            model.headings = new List<SortOptions>();
            model.headings.Add(new SortOptions("Afdeling", "Afdeling", _defaultSortOrder: "Aflopend"));

            int seasonYear = model.playersPerSeason[0].seasonYear;
            int seasonYearEnd = model.playersPerSeason[model.playersPerSeason.Count - 1].seasonYear;

            while (seasonYear <= seasonYearEnd)
            {
                model.headings.Add(new SortOptions("vj", "vj" + seasonYear, _defaultSortOrder: "Aflopend", _attributes: "class='voorjaar'"));
                model.headings.Add(new SortOptions("nj", "nj" + seasonYear, _defaultSortOrder: "Aflopend", _attributes: "class='najaar'"));

                seasonYear++;
            }

            model.headings.Add(new SortOptions("Afdeling", "Afdeling", _defaultSortOrder: "Aflopend"));

            return model;
        }


        public PlayerStatisticsViewModel GetYouthPlayersStatistics()
        {
            var model = new PlayerStatisticsViewModel();

            model.playersPerRegionPerSeason = Database.Instance.statisticsRepository.GetPlayersPerRegionPerSeasonPerCategory(2);
            model.playersPerSeason = Database.Instance.statisticsRepository.GetPlayersPerSeasonPerCategory(2);

            return GetPlayerStatistics(model);
        }


        public PlayerStatisticsViewModel GetAdultPlayersStatistics()
        {
            var model = new PlayerStatisticsViewModel();

            model.playersPerRegionPerSeason = Database.Instance.statisticsRepository.GetPlayersPerRegionPerSeasonPerCategory(1);
            model.playersPerSeason = Database.Instance.statisticsRepository.GetPlayersPerSeasonPerCategory(1);

            return GetPlayerStatistics(model);
        }




        public ClubStatisticsViewModel GetClubStatistics()
        {
            var model = new ClubStatisticsViewModel();

            model.clubsPerRegionPerSeason = Database.Instance.statisticsRepository.GetClubsPerRegionPerSeason();
            model.clubsPerSeason = Database.Instance.statisticsRepository.GetClubsPerSeason();

            model.regions = model.clubsPerRegionPerSeason.Select(r => r.regionName).Distinct().OrderBy(regionName => regionName).ToList();

            model.headings = new List<SortOptions>();
            model.headings.Add(new SortOptions("Afdeling", "Afdeling", _defaultSortOrder: "Aflopend"));

            int seasonYear = model.clubsPerSeason[0].seasonYear;
            int seasonYearEnd = model.clubsPerSeason[model.clubsPerSeason.Count - 1].seasonYear;

            while (seasonYear <= seasonYearEnd)
            {
                model.headings.Add(new SortOptions("vj", "vj" + seasonYear, _defaultSortOrder: "Aflopend", _attributes: "class='voorjaar'"));
                model.headings.Add(new SortOptions("nj", "nj" + seasonYear, _defaultSortOrder: "Aflopend", _attributes: "class='najaar'"));

                seasonYear++;
            }

            model.headings.Add(new SortOptions("Afdeling", "Afdeling", _defaultSortOrder: "Aflopend"));

            return model;
        }

    }
}