using System;
using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;
using ttkaart.Service;

namespace ttkaart.DAL
{
    public class ResultRepository
    {
               
        
        private OleDbConnection db;

        public ResultRepository(OleDbConnection _db)
        {
            db = _db;
        }
        


        private List<ResultListViewModel> GetResultsFromCmd(OleDbCommand cmd)
        {
            List<ResultListViewModel> results = new List<ResultListViewModel>();
            
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _playerName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");
                int _setsPlayed = dbReader.GetOrdinal("sets_played");
                int _setsWon = dbReader.GetOrdinal("sets_won");
                int _percentage = dbReader.GetOrdinal("percentage");
                int _rating = dbReader.GetOrdinal("rating");
                int _baseRating = dbReader.GetOrdinal("base_rating");
                int _teamId = dbReader.GetOrdinal("team_id");
                int _clubId = dbReader.GetOrdinal("club_id");
                int _clubName = dbReader.GetOrdinal("club_name");
                int _teamNumber = dbReader.GetOrdinal("team_number");
                int _seasonId = dbReader.GetOrdinal("season_id");
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _pouleId = dbReader.GetOrdinal("poule_id");
                int _className = dbReader.GetOrdinal("class_name");
                int _classLevel = dbReader.GetOrdinal("class_level");
                int _pouleName = dbReader.GetOrdinal("poule_name");
                int _pouleCategory = dbReader.GetOrdinal("poule_category");
                int _regionName = dbReader.GetOrdinal("region_name");
                int _regionId = dbReader.GetOrdinal("region_id");

                while (dbReader.Read())
                {
                    var result = new ResultListViewModel();

                    result.playerName = dbReader.GetString(_playerName);
                    result.playerNr = dbReader.GetString(_playerNumber);

                    result.setsPlayed = dbReader.GetInt32(_setsPlayed);
                    result.setsWon = dbReader.GetInt32(_setsWon);

                    result.percentage = dbReader.GetInt32(_percentage);

                    result.sortPercentage = 0;
                    if (_setsPlayed > 0) result.sortPercentage = (double)result.setsWon / (double)result.setsPlayed;

                    result.rating = dbReader.GetInt32(_rating);
                    result.baseRating = dbReader.GetInt32(_baseRating);

                    result.clubId = dbReader.GetInt32(_clubId);
                    result.clubName = dbReader.GetString(_clubName);
                    result.teamId = dbReader.GetInt32(_teamId);
                    result.teamNumber = dbReader.GetInt32(_teamNumber);

                    result.seasonId = dbReader.GetInt32(_seasonId);
                    result.seasonYear = dbReader.GetInt32(_seasonYear);
                    result.seasonPeriod = dbReader.GetInt32(_seasonPeriod);

                    result.pouleId = dbReader.GetInt32(_pouleId);
                    result.pouleName = dbReader.GetString(_pouleName);
                    result.pouleCategory = dbReader.GetInt32(_pouleCategory);
                    result.className = dbReader.GetString(_className);
                    result.classLevel = dbReader.GetInt32(_classLevel);

                    result.regionId = dbReader.GetInt32(_regionId);
                    result.regionName = dbReader.GetString(_regionName);
                    
                    result.license = LicenseService.GetLicense(result.rating, result.regionName, result.pouleCategory);

                    results.Add(result);
                }
            }

            return results;
        }

        public List<ResultListViewModel> GetResultsByPlayer(string playerNumber)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, sets_won, percentage, PlayerResult.rating, base_rating," +
                    "      team_id, club_id, club_name, team_number, " +
                    "      Poule.season_id, season_year, season_period," +
                    "      Team.poule_id, class_name, class_level, poule_name, poule_category, region_name, region_id" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Class, Season, Region" +
                    " WHERE Player.id=PlayerResult.player_id" + 
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Player.player_nr='" + playerNumber + "'" +
                    " ORDER BY season_year DESC, season_period DESC, region_id DESC, poule_category ASC", db))
                return GetResultsFromCmd(cmd);
        }


        public List<ResultListViewModel> GetResultsByTeam(int teamId)
        {
            using(OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, sets_won, percentage, rating, base_rating, " +
                    " 		team_id, club_id, club_name, team_number, " +
                    "		season_id, season_year, season_period, " +
                    "       Team.poule_id, class_name, class_level, poule_name, poule_category, region_name, region_id" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Class, Season, Region" +
                    " WHERE Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Team.id=" + teamId +
                    " ORDER BY player_name ASC", db))
                return GetResultsFromCmd(cmd);
        }

        public List<ResultListViewModel> GetResultsByPoule(int pouleId)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, sets_won, percentage, rating, base_rating, " +
                    "      team_id, club_id, club_name, team_number, " +
                    "      season_id, season_year, season_period," +
                    "      Team.poule_id, class_name, class_level, poule_name, poule_category, region_name, region_id" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Class, Season, Region" +
                    " WHERE Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Team.poule_id=" + pouleId +
                    " ORDER BY percentage DESC", db))   
                return GetResultsFromCmd(cmd);
        }

        public List<ResultListViewModel> GetResultsByClubAndSeason(List<int> clubIds, int seasonId)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, sets_won, percentage, rating, base_rating, " +
                    "      team_id, club_id, club_name, team_number, " +
                    "      season_id, season_year, season_period," +
                    "      Team.poule_id, class_name, class_level, poule_name, poule_category, region_name, region_id" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Class, Season, Region" +
                    " WHERE Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Club.id IN (" + String.Join(",", clubIds) + ")" +
                    " AND Season.id=" + seasonId +
                    " ORDER BY list_name ASC", db))
                return GetResultsFromCmd(cmd);
        }


    }
}