using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ttkaart.Controllers
{
    public class TtkaartController : Controller
    {
        public string MenuItem { get; set; }


        public ActionResult NotFound()
        {
            Response.Status = "404 Not Found";
            Response.TrySkipIisCustomErrors = true;

            return View("~/Views/Error/Index.cshtml");
        }

    }
}
