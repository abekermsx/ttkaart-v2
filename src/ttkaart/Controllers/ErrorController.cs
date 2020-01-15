using System.Web.Mvc;

namespace ttkaart.Controllers
{
    public class ErrorController : TtkaartController
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return NotFound();
        }

    }
}
