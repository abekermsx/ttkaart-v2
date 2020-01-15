using System.Web.Mvc;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Home")]
    public class HomeController : TtkaartController
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View();
        }


    }
}
