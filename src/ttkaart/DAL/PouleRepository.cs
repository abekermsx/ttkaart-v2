using System.Collections.Generic;
using System.Data.OleDb;
using ttkaart.ViewModels;

namespace ttkaart.DAL
{
    public class PouleRepository
    {
        
        private OleDbConnection db;

        public PouleRepository(OleDbConnection _db)
        {
            db = _db;
        }



        public List<PouleListViewModel> GetPoules(int seasonId, int categoryId, int regionId)
        {
            List<PouleListViewModel> poules = new List<PouleListViewModel>();

            PouleListViewModel poule = new PouleListViewModel();
            poule.id = -1;

            using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT Poule.id, poule_name, class_name, strength, Team.id AS team_id, club_name, team_number, class_level" +
                    " FROM Poule, Class, Team, Club" +
                    " WHERE Poule.class_id=Class.id" +
                    " AND Poule.id=Team.poule_id" +
                    " AND Team.club_id=Club.id" +  
                    " AND season_id=" + seasonId + 
                    " AND poule_category=" + categoryId + 
                    " AND region_id=" + regionId +
                    " ORDER BY class_level ASC, class_name ASC, poule_name ASC, club_name ASC, team_number ASC", db))
            using (OleDbDataReader dbReader = cmd.ExecuteReader())
            {
                int _pouleId = dbReader.GetOrdinal("id");
                int _pouleName = dbReader.GetOrdinal("poule_name");
                int _className = dbReader.GetOrdinal("class_name");
                int _strength = dbReader.GetOrdinal("strength");
                int _teamId = dbReader.GetOrdinal("team_id");
                int _clubName = dbReader.GetOrdinal("club_name");
                int _teamNumber = dbReader.GetOrdinal("team_number");
                int _classLevel = dbReader.GetOrdinal("class_level");

                while (dbReader.Read())
                {
                    int pouleId = dbReader.GetInt32(_pouleId);

                    if (poule.id != pouleId)
                    {
                        poule = new PouleListViewModel();

                        poule.id = pouleId;
                        poule.pouleName = dbReader.GetString(_pouleName);
                        poule.className = dbReader.GetString(_className);
                        poule.strength = dbReader.GetInt32(_strength);
                        poule.classLevel = dbReader.GetInt32(_classLevel);
                        poule.teams = new List<TeamListViewModel>();

                        poules.Add(poule);
                    }

                    var team = new TeamListViewModel();
                    team.teamId = dbReader.GetInt32(_teamId);
                    team.clubName = dbReader.GetString(_clubName);
                    team.teamNumber = dbReader.GetInt32(_teamNumber);

                    poule.teams.Add(team);
                }
            }

            return poules;
        }
    }
}