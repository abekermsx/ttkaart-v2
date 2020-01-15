using System;
using System.Linq;
using System.Web.Mvc;
using ttkaart.Helpers;
using ttkaart.ViewModels;

namespace ttkaart.Controllers
{
    public class SearchController : TtkaartController
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            string text = Request.QueryString["text"];

            if (!String.IsNullOrEmpty(text))
                text = text.Trim();
            else
                text = "";


            if (text.ToUrlFriendlyString(' ').Length <= 1)
            {
                SearchViewModel empty = new SearchViewModel();
                empty.SearchTerm = text;
                empty.clubs = null;
                empty.players = null;

                return View(empty);
            }

            var s = new Service.SearchService();

            SearchViewModel model = s.Search(text);
            
            if (model.players.players.Count() == 1 && model.clubs.Count() == 0)
                return RedirectToRoute("SpelersDetails", new { id = model.players.players[0].playerNr, name = model.players.players[0].playerName.ToUrlFriendlyString() });

            if (model.players.players.Count() == 0 && model.clubs.Count() == 1)
                return RedirectToRoute("VerenigingenDetails", new { id = model.clubs[0].id, name = model.clubs[0].clubName.ToUrlFriendlyString() });

            RouteData.Values.Add("text", text);


            string sortField = Request.QueryString["SorteerOp"];
            string sortDir = Request.QueryString["Richting"];

            if (!Int32.TryParse(Request.QueryString["page"], out model.players.page))
                model.players.page = 1;


            if (!String.IsNullOrEmpty(sortField))
            {
                if (String.IsNullOrEmpty(sortDir))
                    sortDir = "Oplopend";

                switch (sortField)
                {
                    case "Vereniging":
                        if (sortDir.Equals("Oplopend")) model.clubs = model.clubs.OrderBy(m => m.clubName).ToList();
                        else model.clubs = model.clubs.OrderByDescending(m => m.clubName).ToList();
                        break;

                    case "Website":
                        if (sortDir.Equals("Oplopend")) model.clubs = model.clubs.OrderBy(m => m.website).ToList();
                        else model.clubs = model.clubs.OrderByDescending(m => m.website).ToList();
                        break;

                    case "Bondsnummer":
                        if (sortDir.Equals("Oplopend")) model.players.players = model.players.players.OrderBy(m => m.playerNr).ToList();
                        else model.players.players = model.players.players.OrderByDescending(m => m.playerNr).ToList();
                        break;

                    case "Speler":
                        if (sortDir.Equals("Oplopend")) model.players.players = model.players.players.OrderBy(m => m.playerName).ToList();
                        else model.players.players = model.players.players.OrderByDescending(m => m.playerName).ToList();
                        break;

                    case "Wiki":
                        if (sortDir.Equals("Oplopend")) model.players.players = model.players.players.OrderBy(m => m.wikiUrl).ToList();
                        else model.players.players = model.players.players.OrderByDescending(m => m.wikiUrl).ToList();
                        break;

                    default:
                        break;
                }
                
                if ( sortField.Equals("Bondsnummer") || sortField.Equals("Speler") || sortField.Equals("Wiki") )
                {
                    model.players.sortDir = sortDir;
                    model.players.sortField = sortField;
                }
            }


            return View(model);
        }

    }
}
