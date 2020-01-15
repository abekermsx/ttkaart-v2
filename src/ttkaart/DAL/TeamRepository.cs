using System;
using System.Collections.Generic;
using ttkaart.ViewModels;
using System.Data.OleDb;
using ttkaart.Service;

namespace ttkaart.DAL
{
    public class TeamRepository
    {
        private OleDbConnection db;

        public TeamRepository(OleDbConnection _db)
        {
            db = _db;
        }



        private List<TeamListViewModel> GetTeamsFromCmd(OleDbCommand cmd)
        {
            List<TeamListViewModel> teams = new List<TeamListViewModel>();

            TeamListViewModel team = null;
            
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _playerName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");
                int _setsPlayed = dbReader.GetOrdinal("sets_played");
                int _rating = dbReader.GetOrdinal("rating");
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _teamId = dbReader.GetOrdinal("team_id");
                int _teamNumber = dbReader.GetOrdinal("team_number");
                int _clubName = dbReader.GetOrdinal("club_name");
                int _pouleCategory = dbReader.GetOrdinal("poule_category");
                int _regionName = dbReader.GetOrdinal("region_name");
                int _className = dbReader.GetOrdinal("class_name");
                int _pouleName = dbReader.GetOrdinal("poule_name");
                int _pouleId = dbReader.GetOrdinal("PouleId");
                int _seasonId = dbReader.GetOrdinal("SeasonId");
                int _classLevel = dbReader.GetOrdinal("class_level");

                while (dbReader.Read())
                {
                    if (team == null || !team.teamId.Equals(dbReader.GetInt32(_teamId)) )
                    {
                        team = new TeamListViewModel();

                        team.teamId = dbReader.GetInt32(_teamId);
                        team.teamNumber = dbReader.GetInt32(_teamNumber);
                        team.clubName = dbReader.GetString(_clubName);

                        team.seasonId = dbReader.GetInt32(_seasonId);
                        team.seasonYear = dbReader.GetInt32(_seasonYear);
                        team.seasonPeriod = dbReader.GetInt32(_seasonPeriod);

                        team.pouleId = dbReader.GetInt32(_pouleId);
                        team.pouleName = dbReader.GetString(_pouleName);
                        team.pouleCategory = dbReader.GetInt32(_pouleCategory);

                        team.regionName = dbReader.GetString(_regionName);

                        team.className = dbReader.GetString(_className);
                        team.classLevel = dbReader.GetInt32(_classLevel);

                        team.teamMembers = new List<TeamMemberListViewModel>();

                        teams.Add(team);
                    }

                    TeamMemberListViewModel teamMember = new TeamMemberListViewModel();

                    teamMember.playerName = dbReader.GetString(_playerName);
                    teamMember.playerNumber = dbReader.GetString(_playerNumber);
                    teamMember.setsPlayed = dbReader.GetInt32(_setsPlayed);
                    teamMember.rating = dbReader.GetInt32(_rating);

                    team.teamMembers.Add(teamMember);
                }

            }

            RatingService.CalculateTeamRatings(teams);

            return teams;
        }

        public List<TeamListViewModel> GetTeamsByPoule(int pouleId)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, rating, " +
                    "       season_year, season_period, " +
                    "       team_id, team_number, club_name, " +
                    "       poule_category, region_name, class_name, poule_name, Poule.id AS PouleId, Season.id AS SeasonId, class_level" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Region, Season, Class" +
                    " WHERE Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND Poule.id=" + pouleId +
                    " ORDER BY team_id ASC, list_name ASC", db))
                return GetTeamsFromCmd(cmd);
        }


        public List<TeamListViewModel> GetTeamsByPlayer(string playerNumber)
        {
            string teamIds = "";

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT Team.id" +
                    " FROM Team, PlayerResult, Player" +
                    " WHERE Team.id=PlayerResult.team_id" +
                    " AND PlayerResult.player_id=Player.id" +
                    " AND player_nr='" + playerNumber + "'" +
                    " ORDER BY Team.id ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _teamId = dbReader.GetOrdinal("id");

                while (dbReader.Read())
                {
                    if (!String.IsNullOrEmpty(teamIds))
                        teamIds += ",";

                    teamIds += dbReader.GetInt32(_teamId);
                }
            }
            
            using(var cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, rating, " +
                    "       season_year, season_period, " +
                    "       team_id, team_number, club_name, " +
                    "       poule_category, region_name, class_name, poule_name, Poule.id AS PouleId, Season.id AS SeasonId, class_level" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Region, Season, Class" +
                    " WHERE Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND team_id IN (" + teamIds + ")" +
                    " ORDER BY season_year DESC, season_period DESC, team_id ASC, player_name ASC", db))
                return GetTeamsFromCmd(cmd);
        }


        public List<TeamListViewModel> GetTeamsByClubAndSeason(List<int> clubIds, List<int> seasonId)
        {
            if (seasonId.Count == 0)
                return new List<TeamListViewModel>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, sets_played, rating, " +
                    "       season_year, season_period, " +
                    "       team_id, team_number, club_name, " +
                    "       poule_category, region_name, class_name, poule_name, Poule.id As PouleId, Season.id AS SeasonId, class_level" +
                    " FROM Player, PlayerResult, Team, Club, Poule, Region, Season, Class" +
                    " WHERE Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.region_id=Region.id" +
                    " AND Poule.class_id=Class.id" +
                    " AND Team.club_id IN (" + String.Join(",", clubIds) + ")" +
                    " AND Season.id IN (" + String.Join(",", seasonId) + ")" +
                    " ORDER BY season_year DESC, season_period DESC, team_number ASC, poule_category ASC, region_name ASC, team_id ASC, list_name ASC", db))
                return GetTeamsFromCmd(cmd);
        }

    }
}