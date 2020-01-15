using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DataAnnotationsExtensions;
using System.Web.Mvc;
using ttkaart.Models;

namespace ttkaart.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Voer je naam in")]
        [DisplayName("Naam:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Voer je e-mailadres in")]
        [Email(ErrorMessage = "Voer een geldig e-mailadres in")]
        [DisplayName("E-mailadres:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Voer een onderwerp in")]
        [DisplayName("Onderwerp:")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Voer een bericht in")]
        [DisplayName("Bericht:")]
        public string Message { get; set; }
    }

    public class ChartItemViewModel
    {
        public int seasonYear;
        public int seasonPeriod;

        public int pouleCategory;

        public int regionId;

        public int baseRating;
        public int rating;

        public string color;
    }


    public class WikiListViewModel
    {
        public List<PlayerWikiListViewModel> playerWiki;
        public List<ClubWikiListViewModel> clubWiki;
    }

    public class PlayerWikiListViewModel
    {
        public string playerName;
        public string playerNr;
    }

    public class ClubWikiListViewModel
    {
        public int id;
        public string clubName;
        public string wikiUrl;
    }


    public class PouleViewModel
    {
        public List<ResultListViewModel> playerResults;
        public List<TeamListViewModel> teamResults;
    }


    public class ClubSeasonPlayerViewModel
    {
        public ClubViewModel club;
        public List<ResultListViewModel> playerResults;
    }

    public class ClubSeasonTeamViewModel
    {
        public ClubViewModel club;
        public List<TeamListViewModel> teamResults;
    }

    public class WikiViewModel
    {
        public string wikiUrl;
        public bool wikiActive;
        public string wikiText;
    }

    public class PlayerViewModel
    {
        public string playerName;
        public string playerNumber;

        public WikiViewModel wiki;

        public string licenseYouthCurrent;
        public string licenseYouthPrevious;
        public string licenseSeniorCurrent;
        public string licenseSeniorPrevious;

        public string category;
    }


    public class SeasonViewModel
    {
        public int seasonYear;
        public int seasonPeriod;
    }

    public class ClubPlayerHistoryViewModel
    {
        public ClubViewModel club;
        public List<PlayerHistoryViewModel> players;
        public List<SeasonListViewModel> seasons;
        public List<SortOptions> headings;
    }

    public class PlayerHistoryViewModel
    {
        public string playerName;
        public string playerNumber;

        public WikiViewModel wiki;

        public List<SeasonViewModel> seasons;
    }

    public class PlayerDetailsViewModel
    {
        public PlayerViewModel details;
        public List<ResultListViewModel> results;
        public List<EloRatingListViewModel> eloRatings;
        public List<TeamListViewModel> teams;
        public List<ChartItemViewModel> playerChart;
        public List<ChartItemViewModel> teamChart;
        public ClubPlayerHistoryViewModel teamMemberHistory;
    }

    public class EloRatingListViewModel
    {
        public int seasonId;
        public int seasonYear;
        public int seasonPeriod;
        public int rating;
    }

    public class TeamMemberListViewModel
    {
        public string playerName;
        public string playerNumber;
        public int setsPlayed;
        public int rating;
    }

    public class TeamListViewModel
    {
        public int teamId;
        public string clubName;
        public int teamNumber;

        public int seasonId;
        public int seasonYear;
        public int seasonPeriod;

        public string regionName;

        public int pouleCategory;

        public int pouleId;
        public string pouleName;
        public string className;
        public int classLevel;

        public int averageRating;

        public List<TeamMemberListViewModel> teamMembers;
    }


    public class ResultListViewModel
    {
        public string playerName;
        public string playerNr;

        public int setsPlayed;
        public int setsWon;

        public int percentage;
        public double sortPercentage;

        public int rating;
        public int baseRating;

        public string license;

        public int clubId;
        public string clubName;
        public int teamId;
        public int teamNumber;

        public int seasonId;
        public int seasonYear;
        public int seasonPeriod;

        public int pouleId;
        public string pouleName;
        public int pouleCategory;

        public string className;
        public int classLevel;

        public int regionId;
        public string regionName;
    }
    

    public class SeasonListViewModel
    {
        public int seasonId;
        public int seasonYear;
        public int seasonPeriod;

        public int teams;
        public int players;

        public int clubId;

        public List<string> clubNames;
    }

    public class ClubViewModel
    {
        public int id;
        public string clubName;

        public string website;
        public string twitter;
        public string facebook;

        public string favicon;

        public WikiViewModel wiki;

        public List<string> clubNames;
        public List<SeasonListViewModel> seasons;

        public int playerCount;
    }


    public class SearchViewModel
    {
        public string SearchTerm;
        public List<ClubListViewModel> clubs;
        public PlayerOverviewModel players;

        public bool IsBondsnummer;
        public bool IsValidBondsnummer;
    }


    public class ClubListViewModel
    {
        public int id;
        public string clubName;

        public string website;
        public string twitter;
        public string facebook;

        public string favicon;

        public string wikiUrl;
    }

    public class PlayerListViewModel
    {
        public string playerName;
        public string playerNr;
        public string wikiUrl;
        public bool wikiActive;
        public List<ClubListViewModel> clubs;


        public PlayerListViewModel()
        {
            clubs = new List<ClubListViewModel>();
        }
    }


    public class PlayerOverviewModel
    {
        public List<PlayerListViewModel> players;
        
        public char c;
        public int page;
        public string sortField;
        public string sortDir;

        public PlayerOverviewModel()
        {
            players = new List<PlayerListViewModel>();
        }
    }


    public class Region
    {
        public int id;
        public string name;
    }
    
    public class Season
    {
        public int id;
        public int year;
        public int period;
    }

    public class PouleListViewModel
    {
        public int id;
        public string pouleName;
        public string className;

        public int classLevel;
        public int strength;

        public List<TeamListViewModel> teams;
    }

    public class PoulesViewModel
    {
        public string name;
        public List<SelectListItem> seasons;
        public List<SelectListItem> categories;
        public List<SelectListItem> regions;
        public List<PouleListViewModel> poules;
    }


    public class PlayersPerSeason
    {
        public int playerCount;
        public int seasonYear;
        public int seasonPeriod;
    }

    public class PlayersPerRegionPerSeason
    {
        public int playerCount;
        public string regionName;
        public int seasonYear;
        public int seasonPeriod;
    }

    public class ClubsPerSeason
    {
        public int clubCount;
        public int seasonYear;
        public int seasonPeriod;
    }

    public class ClubsPerRegionPerSeason
    {
        public int clubCount;
        public string regionName;
        public int seasonYear;
        public int seasonPeriod;
    }

    public class PlayerStatisticsViewModel
    {
        public List<PlayersPerRegionPerSeason> playersPerRegionPerSeason;
        public List<PlayersPerSeason> playersPerSeason;
        public List<SortOptions> headings;
        public List<string> regions;
    }

    public class ClubStatisticsViewModel
    {
        public List<ClubsPerRegionPerSeason> clubsPerRegionPerSeason;
        public List<ClubsPerSeason> clubsPerSeason;
        public List<SortOptions> headings;
        public List<string> regions;
    }


    public class RatingViewModel
    {
        public int seasonYear;
        public int seasonPeriod;
        public string regionName;
        public string className;
        public int classLevel;
        public int category;
        public int rating;
        public int formula;
    }


    public class RegionRatingViewModel
    {
        public string regionName;
        public List<SelectListItem> regions;
        public List<SelectListItem> categories;
        public string selectedCategory;
        public List<RatingViewModel> ratings;
        public List<RatingViewModel> ratingsAdults;
        public List<RatingViewModel> ratingsAdultsDuo;
        public List<RatingViewModel> ratingsYouth;
    }


    public class SeasonRatingViewModel
    {
        public string seasonName;
        public List<SelectListItem> seasons;
        public List<SelectListItem> categories;
        public List<RatingViewModel> ratings;
    }
}