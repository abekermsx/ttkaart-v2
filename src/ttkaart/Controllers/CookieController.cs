using System;
using System.Web;
using System.Web.Mvc;

namespace ttkaart.Controllers
{
    public class CookieController : TtkaartController
    {
        //
        // GET: /Cookie/

        public ActionResult Index()
        {
            if ("yes".Equals(Request.QueryString["allow"]))
            {
                HttpCookie cookie = new HttpCookie("allowcookies");

                cookie.Value = "yes";
                cookie.Domain = ".ttkaart.nl";
                cookie.Expires = DateTime.Now.AddYears(20);

                Response.Cookies.Add(cookie);

                return RedirectToRoute("Home");
            }

            if ("no".Equals(Request.QueryString["allow"]))
            {
                HttpCookie cookie = new HttpCookie("allowcookies");

                cookie.Value = "no";
                cookie.Domain = ".ttkaart.nl";
                cookie.Expires = DateTime.Now.AddYears(20);

                Response.Cookies.Add(cookie);

                return RedirectToRoute("Home");
            }

            return View();
        }

    }
}
