using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Cricket
{
    public static class TeamSelection
    {

        public static int UserSelectingTeam()
        {
           var teamDetails = new TeamDetails();
            Console.WriteLine("Please select your team: ");
            Console.WriteLine("1 : Afghanistan");
            Console.WriteLine("2 : Australia");
            Console.WriteLine("3 : Bangladesh");
            int team = Convert.ToInt32(Console.ReadLine());

            switch(team)
            {
                case 1:
                    Console.WriteLine("You have selected Afghanistan");
                    teamDetails.UserTeamDetails(1);
                    return 1;
                case 2:
                    Console.WriteLine("You have selected Australia");
                    teamDetails.UserTeamDetails(2);
                    return 2;
                case 3:
                    Console.WriteLine("You have selected Bangladesh");
                    teamDetails.UserTeamDetails(3);
                    return 3;                  
                default:
                    Console.WriteLine("Invalid team");
                    return 0;
            }

        }
    }
}
