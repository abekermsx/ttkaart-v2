using System;
using System.Linq;
using System.Web.Mvc;
using ttkaart.DAL;

namespace ttkaart.Controllers
{
    public class TeamController : TtkaartController
    {


        public ActionResult Details(int id)
        {
            var model = Database.Instance.resultRepository.GetResultsByTeam(id);

            if (model.Count == 0)
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
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.playerNr).ToList();
                        else model = model.OrderByDescending(m => m.playerNr).ToList();
                        break;

                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.playerName).ToList();
                        else model = model.OrderByDescending(m => m.playerName).ToList();
                        break;

                    case "Gespeeld":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.setsPlayed).ToList();
                        else model = model.OrderByDescending(m => m.setsPlayed).ToList();
                        break;

                    case "Gewonnen":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.setsWon).ToList();
                        else model = model.OrderByDescending(m => m.setsWon).ToList();
                        break;

                    case "Percentage":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.sortPercentage).ThenBy(m => m.setsWon).ToList();
                        else model = model.OrderByDescending(m => m.sortPercentage).ThenByDescending(m => m.setsWon).ToList();
                        break;

                    case "Basisrating":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.baseRating).ToList();
                        else model = model.OrderByDescending(m => m.baseRating).ToList();
                        break;

                    case "Rating":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.rating).ToList();
                        else model = model.OrderByDescending(m => m.rating).ToList();
                        break;

                    case "Licentie":
                        if (sortDir.Equals("Oplopend")) model = model.OrderBy(m => m.license).ToList();
                        else model = model.OrderByDescending(m => m.license).ToList();
                        break;

                    default:
                        break;
                }
            }

            return View(model);
        }

    }
}
