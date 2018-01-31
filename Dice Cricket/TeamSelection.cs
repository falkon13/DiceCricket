//-----------------------------------------------------------------------
// <copyright file="TeamSelection.cs" company="Falkon">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Dice_Cricket
{
    using System;

    /// <summary>
    /// Class handling the selection of teams
    /// </summary>
    public static class TeamSelection
    {
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
            int team = Convert.ToInt32(Console.ReadLine());

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

                default:
                    Console.WriteLine("Invalid team");
                    return 0;
            }
        }
    }
}