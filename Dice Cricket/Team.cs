//-----------------------------------------------------------------------
// <copyright file="Team.cs" company="Falkon13">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Dice_Cricket
{
    /// <summary>
    /// Creation of the team and details
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Enum listing batting milestones in their innings
        /// </summary>
        public enum Milestone
        {
            /// <summary>
            /// Batter has not reached any milestones
            /// </summary>
            None,

            /// <summary>
            /// Batter has previously hit a half century
            /// </summary>
            HalfCentury,

            /// <summary>
            /// Batter has previously hit a century
            /// </summary>
            Century,

            /// <summary>
            /// Batter has previously hit 150 runs
            /// </summary>
            OneAndAHalfCentury,

            /// <summary>
            /// Batter has previously hit a double century
            /// </summary>
            DoubleCentury,

            /// <summary>
            /// Batter has previously hit 250 runs
            /// </summary>
            DoubleAndAHalfCentury,

            /// <summary>
            /// Batter has previously hit a triple century
            /// </summary>
            TripleCentury
        }

        /// <summary>
        /// Populates the details of the team selected
        /// </summary>
        /// <param name="teamSelected">The team selected</param>
        /// <returns>Details of the team</returns>
        public TeamDetails[] PopulateTeamPlayers(int teamSelected)
        {
            Team teamName = new Team();
            TeamDetails[] team = new TeamDetails[11];

            team = this.GenerateTeamStatus(team);

            team = this.PopulatePlayers(teamSelected, team);

            return team;
        }

        /// <summary>
        /// Generates the status of each player
        /// </summary>
        /// <param name="team">The team to get status for</param>
        /// <returns>Relevant team details</returns>
        private TeamDetails[] GenerateTeamStatus(TeamDetails[] team)
        {
            for (int i = 0; i < 11; i++)
            {
                team[i].LastMileStone = Milestone.None;
                team[i].IsKeeper = false;
                if (i == 0)
                {
                    team[i].BattingStatus = "Facing";
                }

                if (i == 1)
                {
                    team[i].BattingStatus = "At Bat";
                }

                if (i > 1)
                {
                    team[i].BattingStatus = "Not Yet Batted";
                }

                team[i].BattingOrder = i + 1;
                team[i].Score = 0;
                team[i].BowlingWickets = 0;
                team[i].FieldingWickets = 0;
                if (i < 7)
                {
                    team[i].IsBowler = false;
                }
                else
                {
                    team[i].IsBowler = true;
                }
            }

            return team;
        }

        /// <summary>
        /// Populates teams with player details
        /// </summary>
        /// <param name="teamSelected">The team selected</param>
        /// <param name="team">Details of the team selected</param>
        /// <returns>Details of the team selected with player names</returns>
        private TeamDetails[] PopulatePlayers(int teamSelected, TeamDetails[] team)
        {
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

                    team[5].IsKeeper = true;
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

                    team[7].IsKeeper = true;
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

                    team[5].IsKeeper = true;
                    break;

                case 4:
                    team[0].TeamName = "England";
                    team[0].PlayerName = "Alastair Cook";
                    team[1].PlayerName = "Mark Stoneman";
                    team[2].PlayerName = "James Vince";
                    team[3].PlayerName = "Joe Root";
                    team[4].PlayerName = "Dawid Malan";
                    team[5].PlayerName = "Ben Stokes";
                    team[6].PlayerName = "Jonny Bairstow";
                    team[7].PlayerName = "Stuart Broad";
                    team[8].PlayerName = "Mark Wood";
                    team[9].PlayerName = "Jack Leach";
                    team[10].PlayerName = "James Anderson";

                    team[5].IsKeeper = true;
                    break;

                case 5:
                    team[0].TeamName = "Guernsey";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 6:
                    team[0].TeamName = "India";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 7:
                    team[0].TeamName = "Ireland";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 8:
                    team[0].TeamName = "Jersey";
                    team[0].PlayerName = "Peter Gough";
                    team[1].PlayerName = "Nathaniel Watkins";
                    team[2].PlayerName = "Ben Stevens";
                    team[3].PlayerName = "Jonty Jenner";
                    team[4].PlayerName = "Corey Bisson";
                    team[5].PlayerName = "Harrison Carlyon";
                    team[6].PlayerName = "Anthony Hawkins-Kay";
                    team[7].PlayerName = "Corne Bodenstein";
                    team[8].PlayerName = "Jake Dunford";
                    team[9].PlayerName = "Charles Perchard";
                    team[10].PlayerName = "Ben Kynman";

                    team[5].IsKeeper = true;
                    break;

                case 9:
                    team[0].TeamName = "Netherlands";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 10:
                    team[0].TeamName = "New Zealand";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 11:
                    team[0].TeamName = "Pakistan";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 12:
                    team[0].TeamName = "South Africa";
                    team[0].PlayerName = "Aiden Markram";
                    team[1].PlayerName = "Dean Elgar";
                    team[2].PlayerName = "Hashim Amla";
                    team[3].PlayerName = "AB de Villiers";
                    team[4].PlayerName = "Faf du Plessis";
                    team[5].PlayerName = "Quinton de Kock";
                    team[6].PlayerName = "Temba Bavumba";
                    team[7].PlayerName = "Vernon Philander";
                    team[8].PlayerName = "Khagiso Rabada";
                    team[9].PlayerName = "Dale Steyn";
                    team[10].PlayerName = "Morne Morkel";

                    team[5].IsKeeper = true;
                    break;

                case 13:
                    team[0].TeamName = "Sri Lanka";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;

                    team[5].IsKeeper = true;
                    break;

                case 14:
                    team[0].TeamName = "West Indies";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;
                    team[5].IsKeeper = true;
                    break;

                case 15:
                    team[0].TeamName = "Zimbabwe";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;
                    team[5].IsKeeper = true;
                    break;

                case 16:
                    team[0].TeamName = "Scotland";
                    team[0].PlayerName = string.Empty;
                    team[1].PlayerName = string.Empty;
                    team[2].PlayerName = "KEEPS";
                    team[3].PlayerName = string.Empty;
                    team[4].PlayerName = string.Empty;
                    team[5].PlayerName = string.Empty;
                    team[6].PlayerName = string.Empty;
                    team[7].PlayerName = string.Empty;
                    team[8].PlayerName = string.Empty;
                    team[9].PlayerName = string.Empty;
                    team[10].PlayerName = string.Empty;
                    team[5].IsKeeper = true;
                    break;
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
            /// Bowling wickets taken by the player
            /// </summary>
            public int BowlingWickets;

            /// <summary>
            /// Fielding wickets taken by the player
            /// </summary>
            public int FieldingWickets;

            /// <summary>
            /// The last batting milestone a batter has reached
            /// </summary>
            public Milestone LastMileStone;

            /// <summary>
            /// Boolean to see if player is a bowler
            /// </summary>
            public bool IsBowler;

            /// <summary>
            /// Boolean to see if player is wicketkeeper
            /// </summary>
            public bool IsKeeper;

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