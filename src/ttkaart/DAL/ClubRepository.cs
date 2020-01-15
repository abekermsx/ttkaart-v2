using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using ttkaart.ViewModels;
using ttkaart.Helpers;


namespace ttkaart.DAL
{
    public class ClubRepository
    {
        private OleDbConnection db;

        public ClubRepository(OleDbConnection _db)
        {
            db = _db;
        }



        public List<SeasonListViewModel> GetClubsSeasons(List<int> clubIds)
        {
            List<SeasonListViewModel> seasons = new List<SeasonListViewModel>();
            
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT Club.id As ClubId, club_name, season_year, season_period, Season.id As SeasonId" +
                    " FROM Club, Team, Poule, Season" +
                    " WHERE Club.id IN (" + String.Join(",", clubIds) + ")" +
                    " AND Club.id=Team.club_id" +
                    " AND Team.poule_id=Poule.id" +
                    " AND Poule.season_id=Season.id" +
                    " GROUP BY season_year, season_period, Season.id, Club.id, club_name" +
                    " ORDER BY season_year DESC, season_period DESC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                SeasonListViewModel season = null;

                int _clubId = dbReader.GetOrdinal("ClubId");
                int _clubName = dbReader.GetOrdinal("club_name");
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _seasonId = dbReader.GetOrdinal("SeasonId");

                while (dbReader.Read())
                {
                    int seasonId = dbReader.GetInt32(_seasonId);

                    if (season == null || season.seasonId != seasonId)
                    {
                        season = new SeasonListViewModel();

                        season.seasonId = seasonId;
                        season.seasonYear = dbReader.GetInt32(_seasonYear);
                        season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                        season.clubId = dbReader.GetInt32(_clubId);
                        season.teams = 0;   // number of teams has to be set correct manually
                        season.clubNames = new List<string>();

                        seasons.Add(season);
                    }

                    season.clubNames.Add( dbReader.GetString(_clubName) );
                }
            }


            
            // count number of players in specified clubs per season
            using (var cmd = new OleDbCommand(
                    "SELECT COUNT(*), season_year, season_period" +
                    " FROM" +
                    " (" +
                    " SELECT DISTINCT PlayerResult.player_id, Season.season_year, Season.season_period" +
                    " FROM PlayerResult, Team, Poule, Season" +
                    " WHERE PlayerResult.team_id=Team.id AND Team.poule_id=Poule.id AND Poule.season_id=Season.id" +
                    " AND Team.club_id IN (" + String.Join(",", clubIds) + ")" +
                    " )" +
                    " GROUP BY Season.season_year, Season.season_period" +
                    " ORDER BY Season.season_year DESC, Season.season_period DESC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");

                while (dbReader.Read())
                {
                    int seasonYear = dbReader.GetInt32(_seasonYear);
                    int seasonPeriod = dbReader.GetInt32(_seasonPeriod);

                    seasons.First(x => x.seasonYear == seasonYear && x.seasonPeriod == seasonPeriod).players = dbReader.GetInt32(0);
                }
            }

            return seasons;
        }


        public ClubViewModel GetClubById(int id)
        {
            ClubViewModel club = null;

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT id, club_name, website, twitter, facebook, wiki_url, wiki_text, favicon" +
                    " FROM Club" +
                    " WHERE id=" + id, db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _clubId = dbReader.GetOrdinal("id");
                int _clubName = dbReader.GetOrdinal("club_name");
                int _website = dbReader.GetOrdinal("website");
                int _twitter = dbReader.GetOrdinal("twitter");
                int _facebook = dbReader.GetOrdinal("facebook");
                int _wikiUrl = dbReader.GetOrdinal("wiki_url");
                int _wikiText = dbReader.GetOrdinal("wiki_text");
                int _favicon = dbReader.GetOrdinal("favicon");

                if (dbReader.Read())
                {
                    club = new ClubViewModel();

                    club.id = dbReader.GetInt32(_clubId);
                    club.clubName = dbReader.GetString(_clubName);
                    club.website = dbReader.GetString(_website);
                    club.twitter = dbReader.GetString(_twitter);
                    club.facebook = dbReader.GetString(_facebook);

                    club.wiki = new WikiViewModel();

                    club.wiki.wikiUrl = dbReader.GetString(_wikiUrl);
                    club.wiki.wikiText = dbReader.GetString(_wikiText);
                    club.wiki.wikiActive = !String.IsNullOrEmpty(club.wiki.wikiUrl);

                    club.favicon = dbReader.GetString(_favicon);
                }
            }

            return club;
        }


        private List<ClubListViewModel> GetClubsFromCmd(OleDbCommand cmd)
        {
            List<ClubListViewModel> clubs = new List<ClubListViewModel>();

            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _clubId = dbReader.GetOrdinal("id");
                int _clubName = dbReader.GetOrdinal("club_name");
                int _website = dbReader.GetOrdinal("website");
                int _twitter = dbReader.GetOrdinal("twitter");
                int _facebook = dbReader.GetOrdinal("facebook");
                int _wikiUrl = dbReader.GetOrdinal("wiki_url");
                int _favicon = dbReader.GetOrdinal("favicon");

                while (dbReader.Read())
                {
                    ClubListViewModel club = new ClubListViewModel();

                    club.id = dbReader.GetInt32(_clubId);
                    club.clubName = dbReader.GetString(_clubName);
                    club.website = dbReader.GetString(_website);
                    club.twitter = dbReader.GetString(_twitter);
                    club.facebook = dbReader.GetString(_facebook);
                    club.wikiUrl = dbReader.GetString(_wikiUrl);
                    club.favicon = dbReader.GetString(_favicon);

                    clubs.Add(club);
                }
            }

            return clubs;
        }

        public List<ClubListViewModel> GetClubsWithWebsite(string website)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT id, club_name, website, twitter, facebook, wiki_url, favicon" +
                    " FROM Club" +
                    " WHERE website='" + website + "'" +
                    " ORDER BY club_name ASC", db))
                return GetClubsFromCmd(cmd);
        }


        public List<ClubListViewModel> GetClubsByText(string text)
        {
            text = text.ToUrlFriendlyString(' ');

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT id, club_name, website, twitter, facebook, wiki_url, favicon" +
                    " FROM Club" +
                    " WHERE search_name LIKE '%" + text + "%'" +
                    " ORDER BY club_name ASC", db))
                return GetClubsFromCmd(cmd);
        }

        public List<ClubListViewModel> GetClubsStartingWithCharacter(char c)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT id, club_name, website, twitter, facebook, wiki_url, favicon" +
                    " FROM Club" +
                    " WHERE club_name LIKE '" + c + "%'" +
                    " ORDER BY club_name ASC", db))
                return GetClubsFromCmd(cmd);
        }


    }
}