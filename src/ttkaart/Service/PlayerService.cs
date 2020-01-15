using System;
using System.Collections.Generic;
using System.Linq;
using ttkaart.ViewModels;
using ttkaart.DAL;
using ttkaart.Models;

namespace ttkaart.Service
{
    public class PlayerService
    {
        private List<ChartItemViewModel> GetTeamChartData(List<TeamListViewModel> teams)
        {
            var chartItems = new List<ChartItemViewModel>();

            foreach (var team in teams)
            {
                if (team.averageRating != -9999)
                {
                    ChartItemViewModel chartItem = new ChartItemViewModel();

                    chartItem.baseRating = team.averageRating;
                    chartItem.rating = team.averageRating;
                    chartItem.seasonYear = team.seasonYear;
                    chartItem.seasonPeriod = team.seasonPeriod;

                    if (team.pouleCategory == 1)
                    {
                        if (team.regionName.Equals("Landelijk Dames"))
                            chartItem.color = "magenta";
                        else
                            chartItem.color = "blue";
                    }
                    else
                    {
                        if (team.regionName.Equals("Landelijk Meisjes"))
                            chartItem.color = "red";
                        else
                            chartItem.color = "green";
                    }

                    chartItems.Add(chartItem);
                }
            }

            return chartItems.OrderBy(c => c.seasonYear).ThenBy(c => c.seasonPeriod).ThenByDescending(c => c.pouleCategory).ToList();
        }


        private int GetPrognosis(List<ChartItemViewModel> chartItems)
        {
            float lngSumXY = 0;
            float lngSumX = 0;
            float lngSumY = 0;
            float lngSumXSquared = 0;
            int i = 0;
            float t = 0;

            var prognosisItems = chartItems.GroupBy(c => new { c.seasonYear, c.seasonPeriod }).Select(g => new { Average = g.Average(p => p.rating) }).ToList();

            for (int j = prognosisItems.Count() - 3; j < prognosisItems.Count(); j++)
            {
                i++;

                for (var p = i; p >= 0; p--)
                {
                    t++;

                    lngSumXY = lngSumXY + t * (float)prognosisItems[j].Average;
                    lngSumX = lngSumX + t;
                    lngSumY = lngSumY + (float)prognosisItems[j].Average;
                    lngSumXSquared = lngSumXSquared + t * t;
                }
            }

            float slope = (t * lngSumXY - lngSumX * lngSumY) / (t * lngSumXSquared - lngSumX * lngSumX);
            float intercept = (lngSumY - slope * lngSumX) / t;
            float prognose = (slope * (t + 1) + intercept);

            return (int)Math.Round(prognose + 0.5F, 0);
        }

        private List<ChartItemViewModel> GetPlayerChartData(List<ResultListViewModel> results)
        {
            var chartItems = new List<ChartItemViewModel>();

            foreach (var result in results)
            {
                if (result.rating != -9999)
                {
                    ChartItemViewModel chartItem = new ChartItemViewModel();

                    chartItem.baseRating = result.baseRating;
                    chartItem.rating = result.rating;
                    chartItem.seasonYear = result.seasonYear;
                    chartItem.seasonPeriod = result.seasonPeriod;
                    chartItem.pouleCategory = result.pouleCategory;
                    chartItem.regionId = result.regionId;
                    
                    if (result.pouleCategory == 1)
                    {
                        if (result.regionName.Equals("Landelijk Dames"))
                            chartItem.color = "magenta";
                        else
                            chartItem.color = "blue";
                    }
                    else
                    {
                        if (result.regionName.Equals("Landelijk Meisjes"))
                            chartItem.color = "red";
                        else
                            chartItem.color = "green";
                    }
                    
                    chartItems.Add(chartItem);
                }
            }

            List<Season> seasons = Database.Instance.seasonRepository.GetSeasons();

            chartItems = chartItems.OrderBy(c => c.seasonYear).ThenBy(c => c.seasonPeriod).ThenByDescending(c => c.pouleCategory).ThenBy(c => c.regionId).ToList();

            var addPrognosis = true;


            if ((chartItems.Count(s => s.seasonYear == seasons[0].year && s.seasonPeriod == seasons[0].period) == 0) &&
                 (chartItems.Count(s => s.seasonYear == seasons[1].year && s.seasonPeriod == seasons[1].period) == 0))
            {
                addPrognosis = false;   // No recent results
            }
            else
            {
                var prognosisItems = chartItems.GroupBy(c => new { c.seasonYear, c.seasonPeriod }).Select(g => new { Average = g.Average(p => p.rating) }).ToList();

                if (prognosisItems.Count() <= 2)
                    addPrognosis = false;   // Not enough results to calculate prognosis
            }

            if (addPrognosis )
            {
                int prognose = GetPrognosis(chartItems);

                ChartItemViewModel chartItem = new ChartItemViewModel();

                chartItem.baseRating = prognose;
                chartItem.rating = prognose;
                chartItem.seasonYear = 1;
                chartItem.seasonPeriod = 1;
                chartItem.color = "black";

                chartItems.Add(chartItem);
            }

            return chartItems;
        }


        public void RemoveSelfFromTeams(List<TeamListViewModel> teams, string playerNumber)
        {
            foreach (var team in teams)
            {
                foreach (var member in team.teamMembers)
                {
                    if (playerNumber.Equals(member.playerNumber))
                    {
                        team.teamMembers.Remove(member);
                        break;
                    }
                }
            }
        }


        private ClubPlayerHistoryViewModel GetTeamMemberHistory(List<TeamListViewModel> teams, string playerNumber)
        {
            var history = new ClubPlayerHistoryViewModel();
                
            history.players = new List<PlayerHistoryViewModel>();
            history.seasons = new List<SeasonListViewModel>();

            foreach( var team in teams)
            {
                foreach( var player in team.teamMembers)
                {
                    if (player.playerNumber.Equals(playerNumber))
                        continue;

                    var playerHistory = history.players.SingleOrDefault(p => p.playerNumber.Equals(player.playerNumber));

                    if (playerHistory == null)
                    {
                        playerHistory = new PlayerHistoryViewModel();
                        playerHistory.playerName = player.playerName;
                        playerHistory.playerNumber = player.playerNumber;
                        playerHistory.seasons = new List<SeasonViewModel>();

                        history.players.Add(playerHistory);
                    }

                    if (playerHistory.seasons.Count(s => s.seasonPeriod == team.seasonPeriod && s.seasonYear == team.seasonYear) == 0)
                        playerHistory.seasons.Add(new SeasonViewModel() { seasonPeriod = team.seasonPeriod, seasonYear = team.seasonYear });

                    if (history.seasons.Count(s => s.seasonPeriod == team.seasonPeriod && s.seasonYear == team.seasonYear) == 0)
                        history.seasons.Add(new SeasonListViewModel() { seasonPeriod = team.seasonPeriod, seasonYear = team.seasonYear });
                }
            }

            history.headings = new List<SortOptions>();
            history.headings.Add(new SortOptions("Bondsnr", "Bondsnummer", _attributes: "class='nr'"));
            history.headings.Add(new SortOptions("Speler", "Speler", _attributes: "class='name'"));
            history.headings.Add(new SortOptions("Seizoenen", "Seizoenen", _defaultSortOrder: "Aflopend"));

            if (history.seasons.Count() > 0)
            {
                int seasonYear = history.seasons[history.seasons.Count() - 1].seasonYear;
                int seasonYearEnd = history.seasons[0].seasonYear;

                while (seasonYear <= seasonYearEnd)
                {
                    history.headings.Add(new SortOptions("vj", "vj" + seasonYear, _attributes: "class='season'"));
                    history.headings.Add(new SortOptions("nj", "nj" + seasonYear, _attributes: "class='season'"));

                    seasonYear++;
                }
            }

            return history;
        }


        public PlayerDetailsViewModel GetPlayerDetails(string playerNumber)
        {
            PlayerDetailsViewModel model = new PlayerDetailsViewModel();
            
            model.details = Database.Instance.playerRepository.GetPlayerByNumber(playerNumber);

            if (model.details == null)
                return null;

            model.results = Database.Instance.resultRepository.GetResultsByPlayer(playerNumber);

            if (model.results.Count() > 0)
            {
                model.teams = Database.Instance.teamRepository.GetTeamsByPlayer(playerNumber);
                model.teamMemberHistory = GetTeamMemberHistory(model.teams, playerNumber);
                
                model.playerChart = GetPlayerChartData(model.results);
                model.teamChart = GetTeamChartData(model.teams);
            }

            return model;
        }
    }
}