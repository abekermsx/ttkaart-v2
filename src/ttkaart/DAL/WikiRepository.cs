using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using ttkaart.ViewModels;

namespace ttkaart.DAL
{
    public class WikiRepository
    {
        
        private OleDbConnection db;

        public WikiRepository(OleDbConnection _db)
        {
            db = _db;
        }


        public List<PlayerWikiListViewModel> GetPlayersWithWiki()
        {
            List<PlayerWikiListViewModel> players = new List<PlayerWikiListViewModel>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT list_name, player_nr" +
                    " FROM Player" +
                    " WHERE wiki_url<>'' AND wiki_active=true" +
                    " ORDER BY list_name ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _listName = dbReader.GetOrdinal("list_name");
                int _playerNumber = dbReader.GetOrdinal("player_nr");

                while (dbReader.Read())
                {
                    var player = new PlayerWikiListViewModel();

                    player.playerName = dbReader.GetString(_listName);
                    player.playerNr = dbReader.GetString(_playerNumber);

                    players.Add(player);
                }
            }

            return players;
        }


        public List<ClubWikiListViewModel> GetClubsWithWiki()
        {
            List<ClubWikiListViewModel> clubs = new List<ClubWikiListViewModel>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT MIN(id) AS ClubId, MIN(club_name) AS ClubName, wiki_url" +
                    " FROM Club" +
                    " WHERE wiki_url<>''" +
                    " GROUP BY wiki_url", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _clubId = dbReader.GetOrdinal("ClubId");
                int _clubName = dbReader.GetOrdinal("ClubName");
                int _wikiUrl = dbReader.GetOrdinal("wiki_url");

                while (dbReader.Read())
                {
                    var club = new ClubWikiListViewModel();

                    club.id = dbReader.GetInt32(_clubId);
                    club.clubName = dbReader.GetString(_clubName);
                    club.wikiUrl = dbReader.GetString(_wikiUrl).Replace("_", " ");

                    clubs.Add(club);
                }
            }

            clubs = clubs.OrderBy(c => c.wikiUrl).ToList();

            return clubs;
        }

    }
}