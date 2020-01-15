using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;

namespace ttkaart.DAL
{
    public class SeasonRepository
    {
        private OleDbConnection db;

        public SeasonRepository(OleDbConnection _db)
        {
            db = _db;
        }



        public List<Season> GetSeasons()
        {
            List<Season> seasons = new List<Season>();

            using (OleDbCommand cmd = new OleDbCommand("SELECT id, season_year, season_period FROM Season ORDER BY season_year DESC, season_period DESC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonId = dbReader.GetOrdinal("id");
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");

                while (dbReader.Read())
                {
                    var season = new Season();

                    season.id = dbReader.GetInt32(_seasonId);
                    season.year = dbReader.GetInt32(_seasonYear);
                    season.period = dbReader.GetInt32(_seasonPeriod);

                    seasons.Add(season);
                }
            }

            return seasons;
        }


        public List<int> GetCategoriesBySeason(int seasonId)
        {
            List<int> categories = new List<int>();

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT DISTINCT poule_category" + 
                    " FROM Poule, Season" + 
                    " WHERE Poule.season_id=Season.id" + 
                    " AND Season.id=" + seasonId, db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _pouleCategory = dbReader.GetOrdinal("poule_category");

                while (dbReader.Read())
                    categories.Add(dbReader.GetInt32(_pouleCategory));
            }

            return categories;
        }
    }
}