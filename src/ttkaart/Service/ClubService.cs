using System;
using System.Collections.Generic;
using System.Linq;
using ttkaart.ViewModels;
using ttkaart.DAL;
using ttkaart.Models;

namespace ttkaart.Service
{
    public class ClubService
    {
        
        private List<int> GetClubIds(ClubViewModel club)
        {
            List<int> clubIds = new List<int>();

            if (!String.IsNullOrEmpty(club.website))
            {
                var clubs = Database.Instance.clubRepository.GetClubsWithWebsite(club.website);

                foreach (var c in clubs)
                    clubIds.Add(c.id);
            }
            else
                clubIds.Add(club.id);

            return clubIds;
        }


        public ClubPlayerHistoryViewModel GetClubPlayerHistory(int clubId)
        {
            var result = new ClubPlayerHistoryViewModel();

            result.club = Database.Instance.clubRepository.GetClubById(clubId);

            if (result.club == null)
                return null;

            List<int> clubIds = GetClubIds(result.club);

            result.players = Database.Instance.playerRepository.GetPlayersFromClubs(clubIds);
            result.seasons = Database.Instance.clubRepository.GetClubsSeasons(clubIds);

            result.headings = new List<SortOptions>();
            result.headings.Add(new SortOptions("Bondsnr", "Bondsnummer", _attributes: "class='nr'"));
            result.headings.Add(new SortOptions(_displayField: "Speler", _sortField: "Speler", _isDefaultSortColumn: true, _attributes: "class='name'"));
            result.headings.Add(new SortOptions("Seizoenen", "Seizoenen", _defaultSortOrder: "Aflopend"));

            int seasonYear = result.seasons[result.seasons.Count() - 1].seasonYear;
            int seasonYearEnd = result.seasons[0].seasonYear;

            while (seasonYear <= seasonYearEnd)
            {
                result.headings.Add(new SortOptions("vj", "vj" + seasonYear, _attributes: "class='season'"));
                result.headings.Add(new SortOptions("nj", "nj" + seasonYear, _attributes: "class='season'"));

                seasonYear++;
            }

            return result;
        }


        public ClubSeasonPlayerViewModel GetClubSeasonPlayerResults(int clubId, int seasonId)
        {
            ClubSeasonPlayerViewModel result = new ClubSeasonPlayerViewModel();

            result.club = Database.Instance.clubRepository.GetClubById(clubId);

            if (result.club == null)
                return null;

            List<int> clubIds = GetClubIds(result.club);

            result.playerResults = Database.Instance.resultRepository.GetResultsByClubAndSeason(clubIds, seasonId);

            if (result.playerResults.Count == 0)
                return null;

            return result;
        }


        public ClubSeasonTeamViewModel GetClubSeasonTeamResults(int clubId, int seasonId)
        {
            ClubSeasonTeamViewModel result = new ClubSeasonTeamViewModel();

            result.club = Database.Instance.clubRepository.GetClubById(clubId);

            if (result.club == null)
                return null;

            List<int> clubIds = GetClubIds(result.club);
            
            List<int> seasonIds = new List<int>();
            seasonIds.Add(seasonId);

            result.teamResults = Database.Instance.teamRepository.GetTeamsByClubAndSeason(clubIds, seasonIds);

            if (result.teamResults.Count == 0)
                return null;

            RatingService.CalculateTeamRatings(result.teamResults);

            return result;
        }

        public ClubViewModel GetClubViewById(int id)
        {
            var clubView = Database.Instance.clubRepository.GetClubById(id);

            if (clubView == null)
                return null;

            clubView.clubNames = new List<string>();

            List<int> clubIds = new List<int>();

            if (!String.IsNullOrEmpty(clubView.website))
            {
                var clubs = Database.Instance.clubRepository.GetClubsWithWebsite(clubView.website);

                foreach (var club in clubs)
                {
                    clubView.clubNames.Add(club.clubName);
                    clubIds.Add(club.id);
                }
            }
            else
                clubIds.Add(id);

            clubView.seasons = Database.Instance.clubRepository.GetClubsSeasons(clubIds);

            List<int> seasonIds = clubView.seasons.Select(c => c.seasonId).ToList();
                
            var teams = Database.Instance.teamRepository.GetTeamsByClubAndSeason(clubIds, seasonIds);

            TeamListViewModel previousTeam = null;

            foreach (var team in teams)
            {
                if (previousTeam == null
                        || previousTeam.seasonId != team.seasonId
                        || !(previousTeam.teamNumber == team.teamNumber
                                && previousTeam.pouleCategory == team.pouleCategory
                                && previousTeam.regionName.Equals(team.regionName)
                                && previousTeam.className.Equals(team.className)))
                {
                    previousTeam = team;
                    clubView.seasons.First(s => s.seasonId == team.seasonId).teams++;
                }
            }

            var players = Database.Instance.playerRepository.GetPlayersFromClubs(clubIds);

            clubView.playerCount = players.Count;

            return clubView;
        }
    }
}