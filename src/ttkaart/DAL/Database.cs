using System;
using System.Data.OleDb;
using System.Data;
using System.Configuration;


namespace ttkaart.DAL
{
    public class Database
    {
        private static Database database;
        
        private OleDbConnection db = null;


        public TeamRepository teamRepository { get; private set; }
        public PlayerRepository playerRepository { get; private set; }
        public ClubRepository clubRepository { get; private set; }
        public ResultRepository resultRepository { get; private set; }
        public WikiRepository wikiRepository { get; private set; }
        public SeasonRepository seasonRepository { get; private set; }
        public PouleRepository pouleRepository { get; private set; }
        public RegionRepository regionRepository { get; private set; }
        public StatisticsRepository statisticsRepository { get; private set; }
        public RatingRepository ratingRepository { get; private set; }


        public static Database Instance
        {
            get
            {
                if (database == null || database.db.State == ConnectionState.Broken || database.db.State == ConnectionState.Closed)
                {
                    if (database != null)
                    {
                        try
                        {
                            database.db.Close();
                        }
                        catch (Exception e)
                        {
                            // just trying to close it just in case..
                        }
                    }

                    database = new Database();
                }

                return database;
            }
        }


        public Database()
        {
            db = new OleDbConnection( ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString );
            db.Open();

            teamRepository = new TeamRepository(db);
            playerRepository = new PlayerRepository(db);
            clubRepository = new ClubRepository(db);
            resultRepository = new ResultRepository(db);
            wikiRepository = new WikiRepository(db);
            seasonRepository = new SeasonRepository(db);
            pouleRepository = new PouleRepository(db);
            regionRepository = new RegionRepository(db);
            statisticsRepository = new StatisticsRepository(db);
            ratingRepository = new RatingRepository(db);
        }
        
        public void Close()
        {
            db.Close();
        }


    }
}