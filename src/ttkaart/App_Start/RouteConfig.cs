using System.Web.Mvc;
using System.Web.Routing;

namespace ttkaart
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();

            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.asp/{*pathInfo}");
            routes.IgnoreRoute("{resource}.txt/{*pathInfo}");
            routes.IgnoreRoute("{resource}.html/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("movies/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Links",
                url: "Links",
                defaults: new { controller = "Links", action = "Index" }
            );

            routes.MapRoute(
                name: "Nieuws",
                url: "Nieuws",
                defaults: new { controller = "News", action = "Index" }
            );

            routes.MapRoute(
                name: "Cookies",
                url: "Cookies",
                defaults: new { controller = "Cookie", action = "Index" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "Contact",
                defaults: new { controller = "Contact", action = "Index" }
            );

            routes.MapRoute(
                name: "Bedankt",
                url: "Bedankt",
                defaults: new { controller = "Contact", action = "Thanks" }
            );

            routes.MapRoute(
                name: "Wiki",
                url: "Wiki",
                defaults: new { controller = "Wiki", action = "Index" }
            );

            routes.MapRoute(
                name: "Zoeken",
                url: "Zoeken",
                defaults: new { controller = "Search", action = "Index" }
            );

            routes.MapRoute(
                name: "Spelers",
                url: "Spelers",
                defaults: new { controller = "Player", action = "Index" }
            );

            routes.MapRoute(
                name: "SpelersOverzicht",
                url: "Spelers/Overzicht/{c}/{page}",
                defaults: new { controller = "Player", action = "Overview", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SpelersDetails",
                url: "Spelers/{id}/{name}",
                defaults: new { controller = "Player", action = "Details", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Verenigingen",
                url: "Verenigingen",
                defaults: new { controller = "Club", action = "Index" }
            );

            routes.MapRoute(
                name: "VerenigingenOverzicht",
                url: "Verenigingen/Overzicht/{c}",
                defaults: new { controller = "Club", action = "Overview" }
            );

            routes.MapRoute(
                name: "VerenigingenDetails",
                url: "Verenigingen/{id}/{name}",
                defaults: new { controller = "Club", action = "Details", name = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "VerenigingenSpelers",
                url: "Verenigingen/{id}/{clubName}/Resultaten/{season}/{seasonName}",
                defaults: new { controller = "Club", action = "Players" }
            );

            routes.MapRoute(
                name: "VerenigingenTeams",
                url: "Verenigingen/{id}/{clubName}/Teams/{season}/{seasonName}",
                defaults: new { controller = "Club", action = "Teams" }
            );

            routes.MapRoute(
                name: "VerenigingenHistorie",
                url: "Verenigingen/{id}/{clubName}/Historie",
                defaults: new { controller = "Club", action = "PlayerHistory" }
            );

            routes.MapRoute(
                name: "TeamDetails",
                url: "Team/{id}/{name}",
                defaults: new { controller = "Team", action = "Details", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PouleDetails",
                url: "Poule/{id}/{name}",
                defaults: new { controller = "Poule", action = "Details", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Competities",
                url: "Competities",
                defaults: new { controller = "Poule", action = "Index" }
            );

            routes.MapRoute(
                name: "CompetitiesOverzicht",
                url: "Competities/{season}/{category}/{region}/{seasonName}/{categoryName}/{regionName}",
                defaults: new { controller = "Poule", action = "Index" }
            );

            routes.MapRoute(
                name: "StatistiekenSpelers",
                url: "Statistieken/Spelers",
                defaults: new { controller = "Statistics", action = "Players" }
            );

            routes.MapRoute(
                name: "StatistiekenSpelersJunioren",
                url: "Statistieken/Spelers/Junioren",
                defaults: new { controller = "Statistics", action = "PlayersYouth" }
            );

            routes.MapRoute(
                name: "StatistiekenSpelersSenioren",
                url: "Statistieken/Spelers/Senioren",
                defaults: new { controller = "Statistics", action = "PlayersAdult" }
            );

            routes.MapRoute(
                name: "StatistiekenVerenigingen",
                url: "Statistieken/Verenigingen",
                defaults: new { controller = "Statistics", action = "Clubs" }
            );

            routes.MapRoute(
                name: "RatingsUitleg",
                url: "Ratings/Uitleg",
                defaults: new { controller = "Rating", action = "Explanation" }
            );

            routes.MapRoute(
                name: "RatingsPerRegioStart",
                url: "Ratings/Afdeling",
                defaults: new { controller = "Rating", action = "Region" }
            );

            routes.MapRoute(
                name: "RatingsPerRegio",
                url: "Ratings/Afdeling/{region}/{regionName}/{category}",
                defaults: new { controller = "Rating", action = "Region", region = UrlParameter.Optional, regionName = UrlParameter.Optional, category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RatingsPerSeizoenStart",
                url: "Ratings/Seizoen",
                defaults: new { controller = "Rating", action = "Season" }
            );

            routes.MapRoute(
                name: "RatingsPerSeizoen",
                url: "Ratings/Seizoen/{season}/{seasonName}/{category}",
                defaults: new { controller = "Rating", action = "Season", season = UrlParameter.Optional, seasonName = UrlParameter.Optional, category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GegevensNietGevonden",
                url: "Error",
                defaults: new { controller = "Error", action = "Index" }
            );

            routes.MapRoute(
                name: "Keepalive",
                url: "Keepalive",
                defaults: new { controller = "Keepalive", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}