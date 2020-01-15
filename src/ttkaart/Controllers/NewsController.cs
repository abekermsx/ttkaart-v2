using System.Web.Mvc;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Nieuws")]
    public class NewsController : TtkaartController
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stop()
        {
            return View();
        }
    }
}
