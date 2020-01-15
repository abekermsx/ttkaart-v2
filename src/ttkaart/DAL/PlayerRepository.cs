using System;
using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;
using ttkaart.Helpers;

namespace ttkaart.DAL
{
    public class PlayerRepository
    {
        private OleDbConnection db;

        public PlayerRepository(OleDbConnection _db)
        {
            db = _db;
        }



        public List<PlayerViewModel> GetAllPlayers()
        {
            var players = new List<PlayerViewModel>();

            using (OleDbCommand cmd = new OleDbCommand("SELECT list_name, player_nr FROM Player", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _playerName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");

                while (dbReader.Read())
                {
                    var player = new PlayerViewModel();

                    player.playerName = dbReader.GetString(_playerName);
                    player.playerNumber = dbReader.GetString(_playerNumber);

                    players.Add(player);
                }
            }

            return players;
        }


        public PlayerViewModel GetPlayerByNumber(string text)
        {
            PlayerViewModel player = null;

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, wiki_url, wiki_active, wiki_text, " +
                    " license_youth_current, license_youth_previous, " +
                    " license_senior_current, license_senior_previous, " +
                    " category" + 
                    " FROM Player" +
                    " WHERE player_nr='" + text + "'", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _playerName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");
                int _wikiUrl = dbReader.GetOrdinal("wiki_url");
                int _wikiActive = dbReader.GetOrdinal("wiki_active");
                int _wikiText = dbReader.GetOrdinal("wiki_text");
                int _licenseYouthCurrent = dbReader.GetOrdinal("license_youth_current");
                int _licenseYouthPrevious = dbReader.GetOrdinal("license_youth_previous");
                int _licenseSeniorCurrent = dbReader.GetOrdinal("license_senior_current");
                int _licenseSeniorPrevious = dbReader.GetOrdinal("license_senior_previous");
                int _category = dbReader.GetOrdinal("category");

                while (dbReader.Read())
                {
                    player = new PlayerViewModel();

                    player.playerName = dbReader.GetString(_playerName);
                    player.playerNumber = dbReader.GetString(_playerNumber);

                    player.wiki = new WikiViewModel();
                    player.wiki.wikiUrl = dbReader.GetString(_wikiUrl);
                    player.wiki.wikiActive = dbReader.GetBoolean(_wikiActive);
                    player.wiki.wikiText = dbReader.GetString(_wikiText);

                    player.licenseYouthCurrent = dbReader.GetString(_licenseYouthCurrent);
                    player.licenseYouthPrevious = dbReader.GetString(_licenseYouthPrevious);
                    player.licenseSeniorCurrent = dbReader.GetString(_licenseSeniorCurrent);
                    player.licenseSeniorPrevious = dbReader.GetString(_licenseSeniorPrevious);

                    player.category = dbReader.GetString(_category);
                }
            }

            return player;
        }



        private List<PlayerListViewModel> GetPlayersFromCmd(OleDbCommand cmd)
        {
            List<PlayerListViewModel> players = new List<PlayerListViewModel>();

            PlayerListViewModel player = null;
            
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _playerName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");
                int _wikiUrl = dbReader.GetOrdinal("wiki_url");
                int _wikiActive = dbReader.GetOrdinal("wiki_active");
                int _clubId = dbReader.GetOrdinal("id");
                int _clubName = dbReader.GetOrdinal("club_name");

                while (dbReader.Read())
                {
                    string playerNr = dbReader.GetString(_playerNumber);

                    if (player == null || !player.playerNr.Equals(playerNr))
                    {
                        player = new PlayerListViewModel();

                        player.playerName = dbReader.GetString(_playerName);
                        player.playerNr = playerNr;
                        player.wikiUrl = dbReader.GetString(_wikiUrl);
                        player.wikiActive = dbReader.GetBoolean(_wikiActive);

                        players.Add(player);
                    }

                    ClubListViewModel club = new ClubListViewModel();

                    club.id = dbReader.GetInt32(_clubId);
                    club.clubName = dbReader.GetString(_clubName);

                    player.clubs.Add(club);
                }
            }

            return players;
        }


        public List<PlayerListViewModel> GetPlayersByText(string text)
        {
            text = text.ToUrlFriendlyString(' ');


            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, Player.wiki_url, Player.wiki_active, Club.id, club_name " +
                    " FROM Player, PlayerResult, Team, Club" +
                    " WHERE Player.search_name LIKE '%" + text + "%'" +
                    " AND Player.id=PlayerResult.player_id" +
                    " AND PlayerResult.team_id=Team.id" +
                    " AND Team.club_id=Club.id" +
                    " GROUP BY list_name, player_nr, Player.wiki_url, Player.wiki_active, club_name, Club.id" +
                    " ORDER BY list_name ASC, player_nr ASC", db))
                return GetPlayersFromCmd(cmd);
        }



        public List<PlayerListViewModel> GetPlayersStartingWithCharacter(char c)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr, Player.wiki_url, Player.wiki_active, Club.id, club_name " +
                    " FROM Player, PlayerClub, Club" +
                    " WHERE list_name LIKE '" + c + "%'" +
                    " AND Player.id=PlayerClub.player_id" +
                    " AND PlayerClub.club_id=Club.id" +
                    " GROUP BY list_name, player_nr, Player.wiki_url, Player.wiki_active, club_name, Club.id" +
                    " ORDER BY list_name ASC, player_nr ASC", db))
                return GetPlayersFromCmd(cmd);
        }


        public List<PlayerHistoryViewModel> GetPlayersFromClubs(List<int> clubIds)
        {
            var result = new List<PlayerHistoryViewModel>();

            PlayerHistoryViewModel player = null;

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT DISTINCT PlayerResult.player_id, player_nr, list_name, Season.season_year, Season.season_period" +
                    " FROM Player,PlayerResult, Team, Poule, Season" +
                    " WHERE PlayerResult.team_id=Team.id AND Team.poule_id=Poule.id AND Poule.season_id=Season.id AND PlayerResult.player_id=Player.id" +
                    " AND Team.club_id IN (" + String.Join(",", clubIds) + ")" +
                    " ORDER BY list_name ASC, player_nr ASC, season_year ASC, season_period ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _playerName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");

                while (dbReader.Read())
                {
                    string playerNr = dbReader.GetString(_playerNumber);

                    if (player == null || !player.playerNumber.Equals(playerNr))
                    {
                        player = new PlayerHistoryViewModel();
                        player.playerName = dbReader.GetString(_playerName);
                        player.playerNumber = dbReader.GetString(_playerNumber);
                        player.seasons = new List<SeasonViewModel>();

                        result.Add(player);
                    }

                    var season = new SeasonViewModel();

                    season.seasonYear = dbReader.GetInt32(_seasonYear);
                    season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);

                    player.seasons.Add(season);
                }

            }

            return result;
        }


    }
}