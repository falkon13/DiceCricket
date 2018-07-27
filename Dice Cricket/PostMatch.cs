//-----------------------------------------------------------------------
// <copyright file="PostMatch.cs" company="Jonathan le Grange">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Dice_Cricket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Handles all actions after a match has finished
    /// </summary>
    public class PostMatch
    {
        /// <summary>
        /// Removes teams from the list the computer can pick a team from
        /// </summary>
        /// <param name="teamsLeft">The number of computer teams left for the round</param>
        /// <param name="teams">List of currently available teams</param>
        /// <returns>Available teams</returns>
        public IList<int> KnockoutTeams(int teamsLeft, IList<int> teams)
        {
            Random random = new Random();
            while (teams.Count > teamsLeft)
            {
                int randomTeam = random.Next(1, 16);
                if (teams.Contains(randomTeam))
                {
                    teams.Remove(randomTeam);
                }
            }

            return teams;
        }

        public void CalculateBestPlayer(Team.TeamDetails[] computerTeamDetails, Team.TeamDetails[] userTeamDetails)
        {
            int[] computerScore = new int[11];
            int[] playerScore = new int[11];
            for (int i = 0; i < computerTeamDetails.Length; i++)
            {
                computerScore[i] += computerTeamDetails[i].Score;
                computerScore[i] += (computerTeamDetails[i].BowlingWickets * 50);
                computerScore[i] += (computerTeamDetails[i].FieldingWickets * 25);
            }
            for (int i = 0; i < userTeamDetails.Length; i++)
            {
                playerScore[i] += userTeamDetails[i].Score;
                playerScore[i] += (userTeamDetails[i].BowlingWickets * 50);
                playerScore[i] += (userTeamDetails[i].FieldingWickets * 25);
            }
            int highestComputerScore = computerScore.ToList().IndexOf(computerScore.Max());
            int highestPlayerScore = playerScore.ToList().IndexOf(playerScore.Max());
            if (computerScore.Max() > playerScore.Max())
            {
                Console.WriteLine($"Man of the Match : {computerTeamDetails[highestComputerScore].PlayerName}({computerScore.Max()})");
            }
            else if (playerScore.Max() > computerScore.Max())
            {
                Console.WriteLine($"Man of the Match : {userTeamDetails[highestPlayerScore].PlayerName}({playerScore.Max()})");
            }
            else if (playerScore.Max() == computerScore.Max())
            {
                Console.WriteLine($"Man of the Match : {userTeamDetails[highestPlayerScore].PlayerName}({playerScore.Max()}) & {computerTeamDetails[highestComputerScore].PlayerName}({computerScore.Max()}) ");
            }
        }
    }
}