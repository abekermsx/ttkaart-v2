using System.Web.Mvc;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Wiki")]
    public class WikiController : TtkaartController
    {
        //
        // GET: /Wiki/

        public ActionResult Index()
        {
            var s = new Service.WikiService();

            var model = s.GetWiki();

            return View(model);
        }

    }
}
