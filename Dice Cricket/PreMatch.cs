//-----------------------------------------------------------------------
// <copyright file="PreMatch.cs" company="Jonathan le Grange">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Dice_Cricket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Handles all things before a match
    /// </summary>
    public class PreMatch
    {
        /// <summary>
        /// Number of players on a team
        /// </summary>
        private const int TeamSize = 11;

        /// <summary>
        /// List of all available teams
        /// </summary>
        public IList<int> AvailableTeams { get; set; } = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        /// <summary>
        /// Setups a match
        /// </summary>
        /// <param name="userSelectedTeam">The user's team</param>
        /// <param name="currentRound">The current tournament round</param>
        /// <returns>All details for a match </returns>
        public Tuple<Team, Team, GameEngine, Team.TeamDetails[], Team.TeamDetails[], int> MatchSetup(int userSelectedTeam, string currentRound)
        {
            var gameEngine = new GameEngine();
            Team userTeam = new Team();
            Team computerTeam = new Team();
            int teamSelected;

            if (userSelectedTeam == 0)
            {
                teamSelected = TeamSelection.UserSelectingTeam();
                this.AvailableTeams = this.AvailableTeams.Where(teams => teams != teamSelected).ToList();
            }
            else
            {
                teamSelected = userSelectedTeam;
            }

            int computerTeamSelected = TeamSelection.ComputerSelectingTeam(teamSelected, this.AvailableTeams);

            this.AvailableTeams = this.AvailableTeams.Where(teams => teams != computerTeamSelected).ToList();

            var userTeamDetails = userTeam.PopulateTeamPlayers(teamSelected);
            var computerTeamDetails = computerTeam.PopulateTeamPlayers(computerTeamSelected);

            string userTeamName = userTeamDetails[0].TeamName;

            string computerTeamName = computerTeamDetails[0].TeamName;

            if (currentRound != "Final")
            {
                Console.WriteLine($"Welcome to today's match, it's a {currentRound} match between  {computerTeamDetails[0].TeamName} and {userTeamDetails[0].TeamName}");
            }
            else
            {
                Console.WriteLine($"Welcome to today's match, it's the one we've all been waiting for, it's the final between  {computerTeamDetails[0].TeamName} and {userTeamDetails[0].TeamName}");
            }

            this.CoinToss();

            Console.WriteLine($"Here is the line up for {userTeamDetails[0].TeamName}");
            this.DisplayTeam(teamSelected);
            Console.WriteLine($"And here is the line up for {computerTeamDetails[0].TeamName}");
            this.DisplayTeam(computerTeamSelected);

            Console.WriteLine("The game is ready to play. Press any key to start");
            Console.ReadKey();
            gameEngine.ScoreBoard(userTeamName, computerTeamName, userTeamDetails);

            return Tuple.Create(userTeam, computerTeam, gameEngine, userTeamDetails, computerTeamDetails, teamSelected);
        }

        /// <summary>
        /// Simulation of a coin toss to decide who bats first.
        /// </summary>
        /// <returns> A boolean value stating if the user bats first.</returns>
        private bool CoinToss()
        {
            Console.WriteLine("Select Heads(1) or Tails(2)");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid selection");
                Console.WriteLine("Select Heads(1) or Tails(2)");
            }

            Random randomCoinToss = new Random();
            int actual = randomCoinToss.Next(1, 2);
            if (choice == actual)
            {
                Console.WriteLine("You have won the coin toss, and have chosen to bat");
                return true;
            }
            else
            {
                Console.WriteLine("You have lost the coin toss, and have been chosen to bat first");
                return false;
            }
        }

        /// <summary>
        /// Displays the team and their lineup.
        /// </summary>
        /// <param name="teamSelected">The team selected</param>
        private void DisplayTeam(int teamSelected)
        {
            var teamToDisplay = new Team();
            var teamToDisplayDetails = teamToDisplay.PopulateTeamPlayers(teamSelected);
            for (int i = 0; i < TeamSize; i++)
            {
                Console.WriteLine($"{i + 1} {teamToDisplayDetails[i].PlayerName}");
            }
        }
    }
}