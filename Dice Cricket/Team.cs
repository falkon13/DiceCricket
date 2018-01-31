//-----------------------------------------------------------------------
// <copyright file="Team.cs" company="Falkon">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Dice_Cricket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Creation of the team and details
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Populates the details of the team selected
        /// </summary>
        /// <param name="teamSelected">The team selected</param>
        /// <returns>Details of the team</returns>
        public TeamDetails[] PopulateTeamPlayers(int teamSelected)
        {
            Team teamName = new Team();
            TeamDetails[] team = new Team.TeamDetails[11];
            for (int i = 0; i < 11; i++)
            {
                if (i == 1)
                {
                    team[i].BattingStatus = "Facing";
                }

                if (i == 2)
                {
                    team[i].BattingStatus = "At Bat";
                }

                if (i > 2)
                {
                    team[i].BattingStatus = "Not Yet Batted";
                }

                team[i].BattingOrder = i + 1;
                team[i].Score = 0;
                if (i < 5)
                {
                    team[i].IsBowler = false;
                }
                else
                {
                    team[i].IsBowler = true;
                }
            }

            switch (teamSelected)
            {
                case 1:
                    team[0].TeamName = "Afghanistan";
                    team[0].PlayerName = "Usman Ghani";
                    team[1].PlayerName = "Gulbadin Naib";
                    team[2].PlayerName = "Asghar Stanikzai";
                    team[3].PlayerName = "Samiullah Shenwari";
                    team[4].PlayerName = "Mohammad Nabi";
                    team[5].PlayerName = "Karim Janat";
                    team[6].PlayerName = "Shafiqullah";
                    team[7].PlayerName = "Najibullah Zadran";
                    team[8].PlayerName = "Rashid Khan";
                    team[9].PlayerName = "Amir Hamza";
                    team[10].PlayerName = "Shapoor Zadran";
                    break;
                case 2:
                    team[0].TeamName = "Australia";
                    team[0].PlayerName = "David Warner";
                    team[1].PlayerName = "Matt Renshaw";
                    team[2].PlayerName = "Usman Khawaja";
                    team[3].PlayerName = "Nathan Lyon";
                    team[4].PlayerName = "Steven Smith";
                    team[5].PlayerName = "Peter Handscomb";
                    team[6].PlayerName = "Glenn Maxwell";
                    team[7].PlayerName = "Matthew Wade";
                    team[8].PlayerName = "Ashton Agar";
                    team[9].PlayerName = "Patrick Cummins";
                    team[10].PlayerName = "Josh Hazlewood";
                    break;
                case 3:
                    team[0].TeamName = "Bangladesh";
                    team[0].PlayerName = "Imrul Kayes";
                    team[1].PlayerName = "Soumya Sarkar";
                    team[2].PlayerName = "Mominul Haque";
                    team[3].PlayerName = "Mushfiqur Rahim";
                    team[4].PlayerName = "Mohammad Mahmudullah";
                    team[5].PlayerName = "Liton Das";
                    team[6].PlayerName = "Sabbit Rahman";
                    team[7].PlayerName = "Taijul Islam";
                    team[8].PlayerName = "Rubel Hossain";
                    team[9].PlayerName = "Subashis Roy";
                    team[10].PlayerName = "Mustafizur Rahman";
            }

            return team;
        }

        /// <summary>
        /// Details of the team
        /// </summary>
        public struct TeamDetails
        {
            /// <summary>
            /// Name of the player
            /// </summary>
            public string PlayerName;

            /// <summary>
            /// Batting order of the player
            /// </summary>
            public int BattingOrder;

            /// <summary>
            /// Name of the cricket team
            /// </summary>
            public string TeamName;

            /// <summary>
            /// Status of the batting player
            /// </summary>
            public string BattingStatus;

            /// <summary>
            /// Score of the individual player
            /// </summary>
            public int Score;

            /// <summary>
            /// Boolean to see if player is a bowler
            /// </summary>
            public bool IsBowler;

            /// <summary>
            /// ToString for testing purposes
            /// </summary>
            /// <returns>String of team</returns>
            public override string ToString()
            {
                return string.Format("PlayerName={0}, order={1}, status={2}", this.PlayerName, this.BattingOrder, this.BattingStatus);
            }
        }
    }
}
