using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Reflection;

namespace ttkaart.Helpers
{
    public static class RouteExtensions
    {
        public static string Name(this RouteBase original)
        {
            var routes = System.Web.Routing.RouteTable.Routes;

            if (routes.Contains(original))
            {
                var namedMapField = routes.GetType().GetField("_namedMap", BindingFlags.NonPublic | BindingFlags.Instance);
                var namedMap = namedMapField.GetValue(routes) as Dictionary<string, RouteBase>;

                var query =
                    from pair in namedMap
                    where pair.Value == original
                    select pair.Key;

                return query.Single();
            }

            return string.Empty;
        }
    }
}