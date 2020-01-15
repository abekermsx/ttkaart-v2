using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;

namespace ttkaart.DAL
{
    public class RatingRepository
    {
        
        private OleDbConnection db;

        public RatingRepository(OleDbConnection _db)
        {
            db = _db;
        }
        

        private List<RatingViewModel> GetRatingsFromCmd(OleDbCommand cmd)
        {
            var ratings = new List<RatingViewModel>();

            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _seasonYear = dbReader.GetOrdinal("season_year");
                int _seasonPeriod = dbReader.GetOrdinal("season_period");
                int _regionName = dbReader.GetOrdinal("region_name");
                int _className = dbReader.GetOrdinal("class_name");
                int _classLevel = dbReader.GetOrdinal("class_level");
                int _category = dbReader.GetOrdinal("category");
                int _rating = dbReader.GetOrdinal("rating");
                int _formula = dbReader.GetOrdinal("formula");

                while (dbReader.Read())
                {
                    var rating = new RatingViewModel();

                    rating.seasonYear = dbReader.GetInt32(_seasonYear);
                    rating.seasonPeriod = dbReader.GetInt32(_seasonPeriod);
                    rating.regionName = dbReader.GetString(_regionName);
                    rating.className = dbReader.GetString(_className);
                    rating.classLevel = dbReader.GetInt32(_classLevel);
                    rating.category = dbReader.GetInt32(_category);
                    rating.rating = dbReader.GetInt32(_rating);
                    rating.formula = dbReader.GetInt32(_formula);

                    ratings.Add(rating);
                }
            }
            
            return ratings;
        }

        public List<RatingViewModel> GetRatingsBySeason(int seasonId)
        {
            using(OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, region_name, class_name, class_level, category, rating, formula" +
                    " FROM Rating, Region, Season, Class" +
                    " WHERE Rating.season_id=Season.id AND Rating.region_id=Region.id AND Rating.class_id=Class.id" +
                    " AND season_id=" + seasonId +
                    " ORDER BY category ASC, region_name ASC, rating DESC", db))
                return GetRatingsFromCmd(cmd);
        }

        public List<RatingViewModel> GetRatingsByRegion(int regionId)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT season_year, season_period, region_name, class_name, class_level, category, rating, formula" +
                    " FROM Rating, Region, Season, Class" +
                    " WHERE Rating.season_id=Season.id AND Rating.region_id=Region.id AND Rating.class_id=Class.id" +
                    " AND region_id=" + regionId +
                    " ORDER BY season_year ASC, season_period ASC, category ASC, class_level ASC", db))
                return GetRatingsFromCmd(cmd);
        }

    }
}