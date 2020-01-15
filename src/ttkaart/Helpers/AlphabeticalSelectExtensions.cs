using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace ttkaart.Helpers
{
    public static class AlphabeticalSelectExtensions
    {
        public static MvcHtmlString AlphabeticalSelect(this HtmlHelper helper, string routeName, char selected)
        {
            StringBuilder sb = new StringBuilder();
            
            for (var i = 0; i <= 25; i++)
            {
                var ch = (char)(i + 'A');

                if (selected == ch)
                    sb.Append("<b>");

                sb.Append(System.Web.Mvc.Html.LinkExtensions.RouteLink(helper, ch.ToString(), routeName, new RouteValueDictionary{ {"c", ch}}));
                    
                if (selected == ch)
                    sb.Append("</b>");

                sb.Append(" ");
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}