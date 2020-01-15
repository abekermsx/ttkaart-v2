using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace ttkaart.Helpers
{
    public static class NumericalSelectExtensions
    {
        public static MvcHtmlString NumericalSelect(this HtmlHelper helper, string routeName, int count, int page, RouteValueDictionary routeValues)
        {
            StringBuilder sb = new StringBuilder();

            if (page == 1)
                sb.Append("&lt;&lt; ");
            else
            {
                sb.Append("<b>");

                routeValues.Remove("page");
                routeValues.Add("page", page - 1);

                sb.Append(System.Web.Mvc.Html.LinkExtensions.RouteLink(helper, "<<", routeName, routeValues));

                sb.Append("</b> ");
            }

            for (var i = 1; i <= count; i++)
            {
                if (page == i)
                    sb.Append("<b>");

                routeValues.Remove("page");
                routeValues.Add("page", i);

                sb.Append(System.Web.Mvc.Html.LinkExtensions.RouteLink(helper, i.ToString(), routeName, routeValues ));

                if (page == i)
                    sb.Append("</b>");

                sb.Append(" ");
            }

            if (page == count)
                sb.Append("&gt;&gt; ");
            else
            {
                sb.Append("<b>");

                routeValues.Remove("page");
                routeValues.Add("page", page + 1);

                sb.Append(System.Web.Mvc.Html.LinkExtensions.RouteLink(helper, ">>", routeName, routeValues));

                sb.Append("</b> ");
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}