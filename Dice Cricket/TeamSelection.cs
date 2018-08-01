//-----------------------------------------------------------------------
// <copyright file="TeamSelection.cs" company="Falkon13">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Dice_Cricket
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class handling the selection of teams
    /// </summary>
    public static class TeamSelection
    {
        /// <summary>
        /// Number of teams in a tournament
        /// </summary>
        private const int NumberOfTeams = 16;

        /// <summary>
        /// Method handling the selection of a team by a user
        /// </summary>
        /// <returns>Integer representing a users team</returns>
        public static int UserSelectingTeam()
        {
            Console.WriteLine("Please select your team: ");
            Console.WriteLine("1 : Afghanistan");
            Console.WriteLine("2 : Australia");
            Console.WriteLine("3 : Bangladesh");
            Console.WriteLine("4 : England");
            Console.WriteLine("5 : Guernsey");
            Console.WriteLine("6 : India");
            Console.WriteLine("7 : Ireland");
            Console.WriteLine("8 : Jersey");
            Console.WriteLine("9 : Netherlands");
            Console.WriteLine("10 : New Zealand");
            Console.WriteLine("11 : Pakistan");
            Console.WriteLine("12 : South Africa");
            Console.WriteLine("13 : Sri Lanka");
            Console.WriteLine("14 : West Indies");
            Console.WriteLine("15 : Zimbabwe");
            Console.WriteLine("16 : Scotland");

            int team;
            while (!int.TryParse(Console.ReadLine(), out team))
            {
                Console.WriteLine("Invalid selection");
                Console.WriteLine("Please input a number between 1 and 16");
            }

            switch (team)
            {
                case 1:
                    Console.WriteLine("You have selected Afghanistan");
                    return 1;

                case 2:
                    Console.WriteLine("You have selected Australia");
                    return 2;

                case 3:
                    Console.WriteLine("You have selected Bangladesh");
                    return 3;

                case 4:
                    Console.WriteLine("You have selected England");
                    return 4;

                case 5:
                    Console.WriteLine("You have selected Guernsey");
                    return 5;

                case 6:
                    Console.WriteLine("You have selected India");
                    return 6;

                case 7:
                    Console.WriteLine("You have selected Ireland");
                    return 7;

                case 8:
                    Console.WriteLine("You have selected Jersey");
                    return 8;

                case 9:
                    Console.WriteLine("You have selected Netherlands");
                    return 9;

                case 10:
                    Console.WriteLine("You have selected New Zealand");
                    return 10;

                case 11:
                    Console.WriteLine("You have selected Pakistan");
                    return 11;

                case 12:
                    Console.WriteLine("You have selected South Africa");
                    return 12;

                case 13:
                    Console.WriteLine("You have selected Sri Lanka");
                    return 13;

                case 14:
                    Console.WriteLine("You have selected West Indies");
                    return 14;

                case 15:
                    Console.WriteLine("You have selected Zimbabwe");
                    return 15;

                case 16:
                    Console.WriteLine("You have selected Scotland");
                    return 16;

                default:
                    Console.WriteLine("Invalid team");
                    return 0;
            }
        }

        /// <summary>
        /// Computer selects a team as the opponent
        /// </summary>
        /// <param name="userTeam">The user team</param>
        /// <param name="availableTeams">All non user teams in the competition</param>
        /// <returns>The team to face</returns>
        public static int ComputerSelectingTeam(int userTeam, IList<int> availableTeams)
        {
            Random teamSelect = new Random();
            int team = teamSelect.Next(1, NumberOfTeams);

            // Also need to check previous teams
            while (!availableTeams.Contains(team))
            {
                team = teamSelect.Next(1, NumberOfTeams);
            }

            return team;
        }
    }
}