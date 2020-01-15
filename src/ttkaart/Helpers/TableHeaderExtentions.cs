using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ttkaart.Models;
using System.Web.Routing;
using System.Text;

namespace ttkaart.Helpers
{
    public static class TableHeaderExtentions
    {

        public static MvcHtmlString GenerateTableHeader(this HtmlHelper helper, List<SortOptions> headings, string sortField, string currentSortDir)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<thead>");
            html.Append("<tr>");

            RouteValueDictionary routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            string routeLink = helper.ViewContext.RouteData.Route.Name();
            
            foreach (var heading in headings)
            {
                if (String.IsNullOrEmpty(heading.Attributes))
                    html.Append("<th>");
                else
                    html.Append("<th " + heading.Attributes + ">");
                
                if (String.IsNullOrEmpty(heading.SortField))
                {
                    html.Append(heading.DisplayField.ToString());
                }
                else
                {
                    string sortDir = heading.DefaultSortOrder;

                    if ( (heading.SortField.Equals(sortField) && sortDir.Equals(currentSortDir)) || (String.IsNullOrEmpty(currentSortDir) && heading.IsDefaultSortColumn) )
                    {
                        sortDir = (sortDir.Equals("Oplopend") ? "Aflopend" : "Oplopend");
                    }

                    routeValues.Remove("SorteerOp");
                    routeValues.Remove("Richting");

                    routeValues.Add("SorteerOp", heading.SortField.ToString());
                    routeValues.Add("Richting", sortDir);

                    html.Append(System.Web.Mvc.Html.LinkExtensions.RouteLink(helper, heading.DisplayField.ToString(), routeLink, routeValues));

                    if (heading.IsDefaultSortColumn && String.IsNullOrEmpty(sortField))
                    {
                        if ("Oplopend".Equals(sortDir) )
                        {
                            html.Append("<span> v</span>");
                        }
                        else
                        {
                            html.Append("<span> ^</span>");
                        }
                    }
                    else
                    {
                        if (heading.SortField.Equals(sortField))
                        {
                            if ("Oplopend".Equals(currentSortDir))
                            {
                                html.Append("<span> ^</span>");
                            }
                            else
                            {
                                html.Append("<span> v</span>");
                            }
                        }
                    }
                }

                html.Append("</th>");
            }

            html.Append("</tr>");
            html.Append("</thead>");

            return MvcHtmlString.Create(html.ToString());
        }
    }
}