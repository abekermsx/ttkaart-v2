using System.Web.Mvc;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Links")]
    public class LinksController : TtkaartController
    {
        //
        // GET: /Links/

        public ActionResult Index()
        {
            return View();
        }

    }
}
