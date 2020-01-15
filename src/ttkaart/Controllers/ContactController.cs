using System.Web.Mvc;
using ttkaart.ViewModels;
using System.Net.Mail;
using ttkaart.Filters;

namespace ttkaart.Controllers
{
    [MenuItem(MenuItem="Contact")]
    public class ContactController : TtkaartController
    {
        //
        // GET: /Contact/

        [HttpGet]
        public ActionResult Index()
        {
            return View( new ContactViewModel() );
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if ( !Utils.CrawlerDetection.IsCrawler(Request.UserAgent) )
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("info@ttkaart.nl");
                mail.To.Add(new MailAddress("info@ttkaart.nl"));

                mail.Subject = "Contactformulier op ttkaart.nl ingevuld";

                mail.IsBodyHtml = false;
                mail.Body = "Iemand heeft een contactformulier op ttkaart.nl ingevuld.\n\n" +
                            "Naam: " + model.Name + "\n" +
                            "Email: " + model.Email + "\n" +
                            "IP: " + Request.ServerVariables["REMOTE_ADDR"] + "\n" +
                            "Onderwerp: " + model.Subject + "\n" +
                            "Bericht: " + model.Message + "\n\n";


                SmtpClient SmtpServer = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = "smtp.ttkaart.nl",
                    Port = 25
                };
                SmtpServer.Send(mail);

                return RedirectToRoute("Bedankt");
            }

            return View();
        }


        public ActionResult Thanks()
        {
            return View();
        }
    }
}
