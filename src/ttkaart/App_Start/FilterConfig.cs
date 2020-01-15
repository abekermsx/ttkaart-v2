using System.Web.Mvc;
using ttkaart.Filters;

namespace ttkaart
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CompressContentAttribute());
        }
    }
}