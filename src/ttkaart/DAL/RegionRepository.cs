using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;

namespace ttkaart.DAL
{
    public class RegionRepository
    {
        private OleDbConnection db;

        public RegionRepository(OleDbConnection _db)
        {
            db = _db;
        }


        private List<Region> GetRegionsFromCmd(OleDbCommand cmd)
        {
            List<Region> regions = new List<Region>();

            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _regionId = dbReader.GetOrdinal("id");
                int _regionName = dbReader.GetOrdinal("region_name");

                while (dbReader.Read())
                {
                    var region = new Region();

                    region.id = dbReader.GetInt32(_regionId);
                    region.name = dbReader.GetString(_regionName);

                    regions.Add(region);
                }
            }

            return regions;
        }


        public List<Region> GetRegions()
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT Region.id, region_name FROM Region" +
                     " ORDER BY region_name ASC", db))
                return GetRegionsFromCmd(cmd);
        }


        public List<Region> GetRegionsBySeasonAndCategory(int seasonId, int categoryid)
        {
            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT DISTINCT Region.id, region_name FROM Region, Poule, Season" +
                    " WHERE Region.id=Poule.region_id" +
                    " AND Poule.season_id=Season.id" +
                    " AND Poule.season_id=" + seasonId + 
                    " AND Poule.poule_category=" + categoryid + 
                    " ORDER BY region_name ASC", db))
                return GetRegionsFromCmd(cmd);
         }

    }
}