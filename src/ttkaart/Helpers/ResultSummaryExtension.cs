using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ttkaart.ViewModels;

namespace ttkaart.Helpers
{
    public static class ResultSummaryExtension
    {
        public static MvcHtmlString ResultSummary(this HtmlHelper helper, string label, IEnumerable<ResultListViewModel> result)
        {
            StringBuilder sb = new StringBuilder();

            if (result.Count() > 0)
            {                
                var best = result.OrderByDescending(t => t.rating).First();

                if (best.rating == -9999)
                    best = result.OrderByDescending(t => t.percentage).First();

                if ("Totaal".Equals(label))
                    sb.Append("<tfoot>");

                sb.Append("<tr>");
                sb.Append("<td>"+label+"</td>");
                sb.Append("<td>"+result.Sum(t => t.setsPlayed)+"</td>");
                sb.Append("<td>"+result.Sum(t => t.setsWon)+"</td>");
                sb.Append("<td>"+(result.Sum(t => t.setsWon) * 100 / result.Sum(t => t.setsPlayed))+"</td>");

                if ( best.rating != -9999 )
                    sb.Append("<td>"+best.rating+" ("+(best.seasonPeriod == 1 ? "voorjaar" : "najaar")+" "+best.seasonYear+")</td>");
                else
                    sb.Append("<td>- (" + (best.seasonPeriod == 1 ? "voorjaar" : "najaar") + " " + best.seasonYear + ")</td>");

                sb.Append("</tr>");

                if ("Totaal".Equals(label))
                    sb.Append("</tfoot>");
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}