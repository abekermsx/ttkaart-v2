using System;
using System.Collections.Generic;
using ttkaart.ViewModels;

namespace ttkaart.Service
{
    public class RatingService
    {
        public static void CalculateTeamRatings(List<TeamListViewModel> teams)
        {
            foreach (var team in teams)
            {
                float teamRatingTotals = 0.5F;
                float totalSetsPlayed = 0;

                foreach (var member in team.teamMembers)
                {
                    teamRatingTotals += (member.rating * member.setsPlayed);
                    totalSetsPlayed += member.setsPlayed;
                }

                if (teamRatingTotals < -9999)
                    team.averageRating = -9999;
                else
                    team.averageRating = (int)Math.Round((teamRatingTotals / totalSetsPlayed), 0);
            }
        }

    }
}