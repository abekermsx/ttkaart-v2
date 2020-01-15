using System.Web.Mvc;
using ttkaart.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace ttkaart.Helpers
{
    public static class ChartExtensions
    {
        public static MvcHtmlString Chart(this HtmlHelper helper, List<ChartItemViewModel> chartItems)
        {
            int minimumRating = 1000;
            int maximumRating = -1000;

            foreach (var item in chartItems)
            {
                if (item.baseRating < minimumRating)
                    minimumRating = item.baseRating;

                if (item.rating > maximumRating)
                    maximumRating = item.rating;
            }
            
            StringBuilder html = new StringBuilder();

            html.Append("<table class='chart'>");
            
            html.Append("<tr>");

            foreach (var item in chartItems)
            {
                int baseRatingHeight, ratingHeight;

                if (maximumRating == minimumRating)
                {
                    baseRatingHeight = 240;
                    ratingHeight = 0;
                }
                else
                {
                    baseRatingHeight = ((item.baseRating - minimumRating) * 240 / (maximumRating - minimumRating));
                    ratingHeight = ((item.rating - minimumRating) * 240 / (maximumRating - minimumRating)) - baseRatingHeight;
                }

                string ratingColor = item.color;
                string baseRatingColor = "dark" + item.color;

                if (ratingColor.Equals("black"))
                    baseRatingColor = "black";

                baseRatingHeight += 24;

                html.Append("<td>");
                
                html.Append("<br />");
                html.Append(item.rating);

                html.Append("<table class='chartrow'><tr><td>");

                // Note: &nbsp; has to be outputted inside chart elements for Safari...

                if (ratingHeight > 0)
                {
                    html.Append("<table>" +
                                "<tr><td bgcolor='" + ratingColor + "' height='" + ratingHeight + "'>&nbsp;</td></tr>" +
                                "</table>");
                }

                html.Append("<table>" +
                            "<tr><td bgcolor='"+baseRatingColor+"' height='"+baseRatingHeight+"'>&nbsp;</td></tr>" +
                            "</table>");

                html.Append("</td></tr></table>");

                html.Append("<b>");
                if (!item.color.Equals("black"))
                    html.Append(item.seasonYear + " " + (item.seasonPeriod == 1 ? "vj" : "nj") );
                else
                    html.Append("prognose");
                html.Append("</b>");

                html.Append("</td>");
            }

            html.Append("</tr>");
            
            html.Append("</table>");
            
            return MvcHtmlString.Create(html.ToString());
        }
    }
}