using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ttkaart.DAL;
using System.IO.Compression;

namespace ttkaart
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            MvcHandler.DisableMvcResponseHeader = true;
        }
        
        protected void Application_End()
        {
            try
            {
                Database.Instance.Close();
            }
            catch (Exception ex)
            {
                // Might fail, but not needed to do anything here
            }
        }
        
        protected void Application_BeginRequest()
        {
        }

        protected void Application_EndRequest()
        {
        }

        protected void Application_PreSendRequestHeaders()
        {
            // Ensure that if GZip/Deflate Encoding is applied that headers are set
            // Also works when error occurs if filters are still active

            // Note: won't work if multiple filters are present and the GZipStream / DeflateStream isn't in response.Filter
            // Note: won't work if OutputCache is active

            HttpResponse response = HttpContext.Current.Response;

            if (response.Filter is GZipStream && response.Headers["Content-encoding"] != "gzip")
                response.AppendHeader("Content-encoding", "gzip");
            else if (response.Filter is DeflateStream && response.Headers["Content-encoding"] != "deflate")
                response.AppendHeader("Content-encoding", "deflate");
        }
    }
}