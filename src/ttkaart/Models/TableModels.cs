using System.Collections.Generic;

namespace ttkaart.Models
{
    public static class TableModels
    {
            public static readonly List<SortOptions> ClubHeadings = new List<SortOptions>() {
                new SortOptions("Vereniging", "Vereniging", "Oplopend", true),
                new SortOptions(""),
                new SortOptions("Website", "Website", "Aflopend"),
                new SortOptions("Social", _attributes:"colspan='3'"),
            };


            public static readonly List<SortOptions> ClubSeasonHeadings = new List<SortOptions>() { 
                new SortOptions("Seizoen", "Seizoen", "Aflopend", true, _attributes:"style='width:90px;'"),
                new SortOptions("Teams", "Teams", "Aflopend", _attributes:"style='width:65px;'"),
                new SortOptions("Spelers", "Spelers", "Aflopend",  _attributes:"style='width:70px;'"),
                new SortOptions("Speelnamen")
            };

            public static readonly List<SortOptions> ClubPlayerHeadings = new List<SortOptions>() {
                new SortOptions("Bondsnr", "Bondsnummer", _attributes:"style='width:70px;'"),
                new SortOptions(_displayField:"Speler", _sortField:"Speler", _isDefaultSortColumn:true),
                new SortOptions("Team", "Team"),
                new SortOptions("Categorie", "Categorie", "Aflopend"),
                new SortOptions("Regio", "Regio"),
                new SortOptions("Klasse", "Klasse"),
                new SortOptions("Gespeeld", "Gespeeld", "Aflopend"),
                new SortOptions("Gewonnen", "Gewonnen", "Aflopend"),
                new SortOptions("Percentage", "Percentage", "Aflopend"),
                new SortOptions("Basisrating*", "Basisrating", "Aflopend"),
                new SortOptions("Rating**", "Rating", "Aflopend"),
                new SortOptions("Licentie***", "Licentie", "Oplopend")
            };

            public static readonly List<SortOptions> ClubTeamHeadings = new List<SortOptions>() {
                new SortOptions(_displayField:"Team", _sortField:"Team",_isDefaultSortColumn:true),
                new SortOptions("Teamrating*", "Teamrating", "Aflopend"),
                new SortOptions("Categorie", "Categorie", "Aflopend"),
                new SortOptions("Regio", "Regio", "Aflopend"),
                new SortOptions("Klasse", "Klasse", "Aflopend"),
                new SortOptions("Teamgenoten")
            };



            public static readonly List<SortOptions> PlayerHeadings = new List<SortOptions>() {
                new SortOptions("Bondsnr", "Bondsnummer", _attributes:"style='width:70px;'"),
                new SortOptions("Speler", "Speler", "Oplopend", true),
                new SortOptions("Verenigingen", ""),
                new SortOptions("Wiki", "Wiki", _attributes:"style='width:50px;'")
            };

            public static readonly List<SortOptions> PlayerResultHeadings = new List<SortOptions>() {
                new SortOptions("Team", "Team"),
                new SortOptions("Seizoen", "Seizoen", "Aflopend", true),
                new SortOptions("Categorie", "Categorie", "Aflopend"),
                new SortOptions("Regio", "Regio"),
                new SortOptions("Klasse", "Klasse"),
                new SortOptions("Gespeeld", "Gespeeld", "Aflopend"),
                new SortOptions("Gewonnen", "Gewonnen", "Aflopend"),
                new SortOptions("Percentage", "Percentage", "Aflopend"),
                new SortOptions("Basisrating*", "Basisrating", "Aflopend"),
                new SortOptions("Rating**", "Rating", "Aflopend"),
                new SortOptions("Licentie***", "Licentie", "Oplopend")
            };


            public static readonly List<SortOptions> PlayerTeamHeadings = new List<SortOptions>() {
                new SortOptions("Team", "TeamNaam"),
                new SortOptions("Seizoen", "TeamSeizoen", "Aflopend", true),
                new SortOptions("Teamrating*", "TeamRating", "Aflopend"),
                new SortOptions("Spelers + Ratings")
            };



            public static readonly List<SortOptions> PoulePlayerHeadings = new List<SortOptions>() {
                new SortOptions("Bondsnr", "Bondsnummer", _attributes:"style='width:70px;'"),
                new SortOptions("Speler", "Speler"),
                new SortOptions("Team", "Team"),
                new SortOptions("Gespeeld", "Gespeeld", "Aflopend"),
                new SortOptions("Gewonnen", "Gewonnen", "Aflopend"),
                new SortOptions("Percentage", "Percentage", "Aflopend",true),
                new SortOptions("Basisrating*", "Basisrating", "Aflopend"),
                new SortOptions("Rating**", "Rating", "Aflopend"),
                new SortOptions("Licentie***", "Licentie", "Oplopend")
            };


            public static readonly List<SortOptions> PouleTeamHeadings = new List<SortOptions>() {
                new SortOptions("Team", "TeamNaam"),
                new SortOptions("Teamrating*", "TeamRating", "Aflopend", true),
                new SortOptions("Spelers")
            };
        

            public static readonly List<SortOptions> TeamPlayerHeadings = new List<SortOptions>() {
                new SortOptions("Bondsnr", "Bondsnummer", _attributes:"style='width:70px;'"),
                new SortOptions(_displayField:"Speler", _sortField:"Speler", _isDefaultSortColumn:true),
                new SortOptions("Gespeeld", "Gespeeld", "Aflopend"),
                new SortOptions("Gewonnen", "Gewonnen", "Aflopend"),
                new SortOptions("Percentage", "Percentage", "Aflopend"),
                new SortOptions("Basisrating*", "Basisrating", "Aflopend"),
                new SortOptions("Rating**", "Rating", "Aflopend"),
                new SortOptions("Licentie***", "Licentie", "Oplopend")
            };


            public static readonly List<SortOptions> CompetitionHeadings = new List<SortOptions>() {
                new SortOptions("Klasse", "Klasse", "Oplopend", true),
                new SortOptions("Sterkte*", "Sterkte", "Aflopend"),
                new SortOptions("Teams")
            };

    }
}