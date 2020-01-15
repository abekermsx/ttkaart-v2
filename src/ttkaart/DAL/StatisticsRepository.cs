using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;

namespace ttkaart.DAL
{
    public class StatisticsRepository
    {
        private OleDbConnection db;

        public StatisticsRepository(OleDbConnection _db)
        {
            db = _db;
        }
        
        
        public List<PlayersPerSeason> GetPlayersPerSeason()
        {
            var result = new List<PlayersPerSeason>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, number_of_players, poule_category" +
                    " FROM PlayersPerSeason, Season" +
                    " WHERE PlayersPerSeason.season_id=Season.id" +
                    " ORDER BY season_year ASC, season_period ASC", db))            
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _playerCount = dbReader.GetOrdinal("number_of_players");

                PlayersPerSeason season = null;

                while (dbReader.Read())
                {
                    if (season == null || season.seasonYear != dbReader.GetInt32(_seasonYear) || season.seasonPeriod != dbReader.GetInt32(_seasonPeriod))
                    {
                        season = new PlayersPerSeason();
                        season.seasonYear = dbReader.GetInt32(_seasonYear);
                        season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                        season.playerCount = 0;

                        result.Add(season);
                    }

                    season.playerCount += dbReader.GetInt32(_playerCount);
                }
            }

            return result;
        }
        

        public List<PlayersPerSeason> GetPlayersPerSeasonPerCategory(int categoryId)
        {
            var result = new List<PlayersPerSeason>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, number_of_players, poule_category" +
                    " FROM PlayersPerSeason, Season" +
                    " WHERE PlayersPerSeason.season_id=Season.id AND poule_category=" + categoryId +
                    " ORDER BY season_year ASC, season_period ASC", db))            
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _playerCount = dbReader.GetOrdinal("number_of_players");

                while (dbReader.Read())
                {
                    var season = new PlayersPerSeason();
                    season.seasonYear = dbReader.GetInt32(_seasonYear);
                    season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                    season.playerCount = dbReader.GetInt32(_playerCount);

                    result.Add(season);
                }
            }

            return result;
        }


        public List<PlayersPerRegionPerSeason> GetPlayersPerRegionPerSeason()
        {
            var result = new List<PlayersPerRegionPerSeason>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, region_name, number_of_players, poule_category" +
                    " FROM PlayersPerRegionPerSeason, Season, Region" +
                    " WHERE PlayersPerRegionPerSeason.season_id=Season.id AND PlayersPerRegionPerSeason.region_id=Region.id" +
                    " ORDER BY season_year ASC, season_period ASC, region_name ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _regionName = dbReader.GetOrdinal("region_name");
                int _playerCount = dbReader.GetOrdinal("number_of_players");

                PlayersPerRegionPerSeason season = null;

                while (dbReader.Read())
                {
                    if (season == null || season.regionName != dbReader.GetString(_regionName) || season.seasonYear != dbReader.GetInt32(_seasonYear) || season.seasonPeriod != dbReader.GetInt32(_seasonPeriod))
                    {
                        season = new PlayersPerRegionPerSeason();
                        season.seasonYear = dbReader.GetInt32(_seasonYear);
                        season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                        season.regionName = dbReader.GetString(_regionName);
                        season.playerCount = 0;

                        result.Add(season);
                    }

                    season.playerCount += dbReader.GetInt32(_playerCount);
                }
            }

            return result;
        }


        public List<PlayersPerRegionPerSeason> GetPlayersPerRegionPerSeasonPerCategory(int categoryId)
        {
            var result = new List<PlayersPerRegionPerSeason>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, region_name, number_of_players, poule_category" +
                    " FROM PlayersPerRegionPerSeason, Season, Region" +
                    " WHERE PlayersPerRegionPerSeason.season_id=Season.id AND PlayersPerRegionPerSeason.region_id=Region.id" +
                    " AND poule_category=" + categoryId +
                    " ORDER BY season_year ASC, season_period ASC, region_name ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _regionName = dbReader.GetOrdinal("region_name");
                int _playerCount = dbReader.GetOrdinal("number_of_players");

                while (dbReader.Read())
                {
                    var season = new PlayersPerRegionPerSeason();

                    season.seasonYear = dbReader.GetInt32(_seasonYear);
                    season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                    season.regionName = dbReader.GetString(_regionName);
                    season.playerCount = dbReader.GetInt32(_playerCount);

                    result.Add(season);
                }
            }

            return result;
        }



        public List<ClubsPerSeason> GetClubsPerSeason()
        {
            var result = new List<ClubsPerSeason>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, number_of_clubs" +
                    " FROM ClubsPerSeason, Season" +
                    " WHERE ClubsPerSeason.season_id=Season.id" +
                    " ORDER BY season_year ASC, season_period ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _clubCount = dbReader.GetOrdinal("number_of_clubs");

                while (dbReader.Read())
                {
                    var season = new ClubsPerSeason();

                    season.seasonYear = dbReader.GetInt32(_seasonYear);
                    season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                    season.clubCount = dbReader.GetInt32(_clubCount);

                    result.Add(season);
                }
            }

            return result;
        }


        public List<ClubsPerRegionPerSeason> GetClubsPerRegionPerSeason()
        {
            var result = new List<ClubsPerRegionPerSeason>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, region_name, number_of_clubs" +
                    " FROM ClubsPerRegionPerSeason, Season, Region" +
                    " WHERE ClubsPerRegionPerSeason.season_id=Season.id AND ClubsPerRegionPerSeason.region_id=Region.id" +
                    " ORDER BY season_year ASC, season_period ASC, region_name ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _regionName = dbReader.GetOrdinal("region_name");
                int _clubCount = dbReader.GetOrdinal("number_of_clubs");

                while (dbReader.Read())
                {
                    var season = new ClubsPerRegionPerSeason();

                    season.seasonYear = dbReader.GetInt32(_seasonYear);
                    season.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                    season.regionName = dbReader.GetString(_regionName);
                    season.clubCount = dbReader.GetInt32(_clubCount);

                    result.Add(season);
                }
            }

            return result;
        }
    }
}