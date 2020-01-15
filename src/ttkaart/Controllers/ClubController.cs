using System;
using System.Linq;
using System.Web.Mvc;
using ttkaart.ViewModels;
using ttkaart.DAL;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Verenigingen")]
    public class ClubController : TtkaartController 
    {
        //
        // GET: /Club/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Details(int id, string name)
        {
            var s = new Service.ClubService();
            
            ClubViewModel club = s.GetClubViewById(id);

            if (club == null)
                return NotFound();

            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Seizoen":
                        if (sortDir.Equals("Oplopend")) club.seasons = club.seasons.OrderBy(m => m.seasonYear).ThenBy(m => m.seasonPeriod).ToList();
                        else club.seasons = club.seasons.OrderByDescending(m => m.seasonYear).ThenByDescending(m => m.seasonPeriod).ToList();
                        break;
                    case "Teams":
                        if (sortDir.Equals("Oplopend")) club.seasons = club.seasons.OrderBy(m => m.teams).ToList();
                        else club.seasons = club.seasons.OrderByDescending(m => m.teams).ToList();
                        break;
                    case "Spelers":
                        if (sortDir.Equals("Oplopend")) club.seasons = club.seasons.OrderBy(m => m.players).ToList();
                        else club.seasons = club.seasons.OrderByDescending(m => m.players).ToList();
                        break;
                }
            }

            return View(club);
        }



        public ActionResult Overview(string c)
        {
            if (c.Length != 1)
                return NotFound();
                        
            var model = Database.Instance.clubRepository.GetClubsStartingWithCharacter( c[0] );

            if (model.Count == 0)
                return NotFound();


            ViewBag.c = c[0];

            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Vereniging":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.clubName).ToList();
                        else model = model.OrderByDescending(m => m.clubName).ToList();
                        break;
                        
                    case "Website":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.website).ToList();
                        else model = model.OrderByDescending(m => m.website).ToList();
                        break;

                    default:
                        break;
                }
            }

            return View(model);
        }


        public ActionResult Players(int id, int season)
        {
            var s = new Service.ClubService();
            
            var model = s.GetClubSeasonPlayerResults(id, season);

            if (model == null)
                return NotFound();

            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Bondsnummer":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.playerNr).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.playerNr).ToList();
                        break;

                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.playerName).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.playerName).ToList();
                        break;

                    case "Team":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.clubName).ThenBy(m => m.teamNumber).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.clubName).ThenByDescending(m => m.teamNumber).ToList();
                        break;

                    case "Seizoen":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.seasonYear).ThenBy(m => m.seasonPeriod).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.seasonYear).ThenByDescending(m => m.seasonPeriod).ToList();
                        break;

                    case "Categorie":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.pouleCategory).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.pouleCategory).ToList();
                        break;

                    case "Regio":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.regionName).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.regionName).ToList();
                        break;

                    case "Klasse":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.classLevel).ThenBy(m => m.className).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.classLevel).ThenByDescending(m => m.className).ToList();
                        break;

                    case "Gespeeld":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.setsPlayed).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.setsPlayed).ToList();
                        break;

                    case "Gewonnen":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.setsWon).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.setsWon).ToList();
                        break;

                    case "Percentage":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.sortPercentage).ThenBy(m => m.setsWon).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.sortPercentage).ThenByDescending(m => m.setsWon).ToList();
                        break;

                    case "Basisrating":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.baseRating).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.baseRating).ToList();
                        break;

                    case "Rating":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.rating).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.rating).ToList();
                        break;

                    case "Licentie":
                        if (sortDir.Equals("Oplopend")) model.playerResults = model.playerResults.OrderBy(m => m.license).ToList();
                        else model.playerResults = model.playerResults.OrderByDescending(m => m.license).ToList();
                        break;

                    default:
                        break;
                }
            }

            return View(model);
        }


        public ActionResult PlayerHistory(int id)
        {
            var s = new Service.ClubService();

            var model = s.GetClubPlayerHistory(id);

            if (model == null)
                return NotFound();
            
            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Bondsnummer":
                        if (sortDir.Equals("Oplopend")) model.players = model.players.OrderBy(m => m.playerNumber).ToList();
                        else model.players = model.players.OrderByDescending(m => m.playerNumber).ToList();
                        break;

                    case "Seizoenen":
                        if (sortDir.Equals("Oplopend")) model.players = model.players.OrderBy(m => m.seasons.Count).ToList();
                        else model.players = model.players.OrderByDescending(m => m.seasons.Count).ToList();
                        break;
                        
                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model.players = model.players.OrderBy(m => m.playerName).ToList();
                        else model.players = model.players.OrderByDescending(m => m.playerName).ToList();
                        break;
                }
            }

            return View(model);
        }

        public ActionResult Teams(int id, int season)
        {
            var s = new Service.ClubService();

            var model = s.GetClubSeasonTeamResults(id, season);

            if (model == null)
                return NotFound();
            
            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Team":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.clubName).ThenBy(m => m.teamNumber).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.clubName).ThenByDescending(m => m.teamNumber).ToList();
                        break;

                    case "Teamrating":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.averageRating).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.averageRating).ToList();
                        break;

                    case "Categorie":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.pouleCategory).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.pouleCategory).ToList();
                        break;

                    case "Regio":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.regionName).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.regionName).ToList();
                        break;

                    case "Klasse":
                        if (sortDir.Equals("Oplopend")) model.teamResults = model.teamResults.OrderBy(m => m.classLevel).ThenBy(m => m.className).ToList();
                        else model.teamResults = model.teamResults.OrderByDescending(m => m.classLevel).ThenByDescending(m => m.className).ToList();
                        break;

                    default:
                        break;
                }
            }
            
            return View(model);
        }
    }
}
