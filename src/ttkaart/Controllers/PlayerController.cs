using System;
using System.Linq;
using System.Web.Mvc;
using ttkaart.ViewModels;
using ttkaart.DAL;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Spelers")]
    public class PlayerController : TtkaartController
    {
        //
        // GET: /Player/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview(string c, int? page)
        {
            if (c.Length != 1)
                return NotFound();

            PlayerOverviewModel model = new PlayerOverviewModel();

            model.players = Database.Instance.playerRepository.GetPlayersStartingWithCharacter(c[0]);

            if (model.players.Count == 0)
                return NotFound();

            if (page == null)
                page = 1;

            model.c = c[0];
            model.page = (int)page;
            
            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];
            
            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Bondsnummer":
                        if (sortDir.Equals("Oplopend")) model.players = model.players.OrderBy(m => m.playerNr).ToList();
                        else model.players = model.players.OrderByDescending(m => m.playerNr).ToList();
                        break;

                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model.players = model.players.OrderBy(m => m.playerName).ToList();
                        else model.players = model.players.OrderByDescending(m => m.playerName).ToList();
                        break;

                    case "Wiki":
                        if (sortDir.Equals("Oplopend")) model.players = model.players.OrderBy(m => m.wikiUrl).ToList();
                        else model.players = model.players.OrderByDescending(m => m.wikiUrl).ToList();
                        break;
                        
                    default:
                        break;
                }
            }

            model.sortField = sortField;
            model.sortDir = sortDir;

            return View(model);
        }



        public ActionResult Details(string id, string name)
        {
            var s = new Service.PlayerService();

            PlayerDetailsViewModel model = s.GetPlayerDetails(id);
            
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
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.clubName).ThenBy(m => m.teamNumber).ToList();
                        else model.results = model.results.OrderByDescending(m => m.clubName).ThenByDescending(m => m.teamNumber).ToList();
                        break;

                    case "Seizoen":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.seasonYear).ThenBy(m => m.seasonPeriod).ToList();
                        else model.results = model.results.OrderByDescending(m => m.seasonYear).ThenByDescending(m => m.seasonPeriod).ToList();
                        break;

                    case "Categorie":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.pouleCategory).ToList();
                        else model.results = model.results.OrderByDescending(m => m.pouleCategory).ToList();
                        break;

                    case "Regio":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.regionName).ToList();
                        else model.results = model.results.OrderByDescending(m => m.regionName).ToList();
                        break;

                    case "Klasse":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.classLevel).ThenBy(m => m.className).ToList();
                        else model.results = model.results.OrderByDescending(m => m.classLevel).ThenByDescending(m => m.className).ToList();
                        break;

                    case "Gespeeld":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.setsPlayed).ToList();
                        else model.results = model.results.OrderByDescending(m => m.setsPlayed).ToList();
                        break;

                    case "Gewonnen":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.setsWon).ToList();
                        else model.results = model.results.OrderByDescending(m => m.setsWon).ToList();
                        break;

                    case "Percentage":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.sortPercentage).ThenBy(m => m.setsWon).ToList();
                        else model.results = model.results.OrderByDescending(m => m.sortPercentage).ThenByDescending(m => m.setsWon).ToList();
                        break;

                    case "Basisrating":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.baseRating).ToList();
                        else model.results = model.results.OrderByDescending(m => m.baseRating).ToList();
                        break;

                    case "Rating":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.rating).ToList();
                        else model.results = model.results.OrderByDescending(m => m.rating).ToList();
                        break;

                    case "Licentie":
                        if (sortDir.Equals("Oplopend")) model.results = model.results.OrderBy(m => m.license).ToList();
                        else model.results = model.results.OrderByDescending(m => m.license).ToList();
                        break;

                    case "TeamNaam":
                        if (sortDir.Equals("Oplopend")) model.teams = model.teams.OrderBy(m => m.clubName).ThenBy(m => m.teamNumber).ToList();
                        else model.teams = model.teams.OrderByDescending(m => m.clubName).ThenByDescending(m => m.teamNumber).ToList();
                        break;

                    case "TeamSeizoen":
                        if (sortDir.Equals("Oplopend")) model.teams = model.teams.OrderBy(m => m.seasonYear).ThenBy(m => m.seasonPeriod).ToList();
                        else model.teams = model.teams.OrderByDescending(m => m.seasonYear).ThenByDescending(m => m.seasonPeriod).ToList();
                        break;

                    case "TeamRating":
                        if (sortDir.Equals("Oplopend")) model.teams = model.teams.OrderBy(m => m.averageRating).ToList();
                        else model.teams = model.teams.OrderByDescending(m => m.averageRating).ToList();
                        break;

                    case "Bondsnummer":
                        if (sortDir.Equals("Oplopend")) model.teamMemberHistory.players = model.teamMemberHistory.players.OrderBy(m => m.playerNumber).ToList();
                        else model.teamMemberHistory.players = model.teamMemberHistory.players.OrderByDescending(m => m.playerNumber).ToList();
                        break;

                    case "Seizoenen":
                        if (sortDir.Equals("Oplopend")) model.teamMemberHistory.players = model.teamMemberHistory.players.OrderBy(m => m.seasons.Count).ToList();
                        else model.teamMemberHistory.players = model.teamMemberHistory.players.OrderByDescending(m => m.seasons.Count).ToList();
                        break;

                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model.teamMemberHistory.players = model.teamMemberHistory.players.OrderBy(m => m.playerName).ToList();
                        else model.teamMemberHistory.players = model.teamMemberHistory.players.OrderByDescending(m => m.playerName).ToList();
                        break;

                    default:
                        break;
                }
            }

            return View(model);
        }


    }

}
