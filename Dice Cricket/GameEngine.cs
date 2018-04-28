//-----------------------------------------------------------------------
// <copyright file="GameEngine.cs" company="Jonathan le Grange">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Dice_Cricket
{
    using System;

    /// <summary>
    /// The simulation of the game itself, holds all game logic.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// Number of faces on the die being rolled
        /// </summary>
        private const int dieSize = 6;

        /// <summary>
        /// Number of players on a team
        /// </summary>
        private const int teamSize = 11;

        /// <summary>
        /// The score of the current batting team.
        /// </summary>
        private int score;

        /// <summary>
        /// The amount of wickets the batting team has lost.
        /// </summary>
        private int wickets;

        /// <summary>
        /// The current tournament round of the user.
        /// </summary>
        private string currentRound = "Round of 16";

        /// <summary>
        /// Method that holds the game engine logic.
        /// </summary>
        public void Engine(int userTeamNumb)
        {
            var setup = MatchSetup(userTeamNumb);

            var userTeam = setup.Item1;
            var computerTeam = setup.Item2;
            var gameEngine = setup.Item3;
            var userTeamDetails = setup.Item4;
            var computerTeamDetails = setup.Item5;
            userTeamNumb = setup.Item6;

            while (gameEngine.wickets < 10)
            {
                userTeamDetails = gameEngine.BowlBalled(userTeamDetails);

                Console.ReadKey();
            }

            Console.WriteLine($"Team is out for {gameEngine.score}");
            this.DisplayInningsScoreboard(userTeamDetails);
            int userScore = gameEngine.score;

            gameEngine.wickets = 0;
            gameEngine.score = 0;

            Console.WriteLine($"{computerTeamDetails[0].TeamName} are ready to bat, they need to chase a score of {userScore}");

            while (gameEngine.wickets < 10 && gameEngine.score <= userScore)
            {
                computerTeamDetails = gameEngine.BowlBalled(computerTeamDetails);

                Console.ReadKey();
            }
            int computerScore = gameEngine.score;

            if (userScore > computerScore)
            {
                Console.WriteLine("USER WINS");
                Console.WriteLine("You have progressed to the next round!");
                currentRound = "Quarter Final";
                NewGame(userTeamNumb);
            }
            if (computerScore > userScore)
            {
                Console.WriteLine("COMPUTER WINS");
                Console.WriteLine($"You have been knocked out at the {currentRound}");
            }
            if (computerScore == userScore)
            {
                Console.WriteLine("DRAW");
            }

            this.DisplayInningsScoreboard(computerTeamDetails);
        }

        private void NewGame(int userTeam)
        {
            Engine(userTeam);
        }

        /// <summary>
        /// Displays the team and their lineup.
        /// </summary>
        /// <param name="teamSelected">The team selected</param>
        private void DisplayTeam(int teamSelected)
        {
            var teamToDisplay = new Team();
            var teamToDisplayDetails = teamToDisplay.PopulateTeamPlayers(teamSelected);
            for (int i = 0; i < teamSize; i++)
            {
                Console.WriteLine($"{i + 1} {teamToDisplayDetails[i].PlayerName}");
            }
        }

        private Tuple<Team, Team, GameEngine, Team.TeamDetails[], Team.TeamDetails[], int> MatchSetup(int userSelectedTeam)
        {
            var gameEngine = new GameEngine();
            Team userTeam = new Team();
            Team computerTeam = new Team();
            int teamSelected;

            if (userSelectedTeam == 0)
            {
                teamSelected = TeamSelection.UserSelectingTeam();
            }
            else
            {
                teamSelected = userSelectedTeam;
            }

            int computerTeamSelected = TeamSelection.ComputerSelectingTeam(teamSelected, null);

            var userTeamDetails = userTeam.PopulateTeamPlayers(teamSelected);
            var computerTeamDetails = computerTeam.PopulateTeamPlayers(computerTeamSelected); //computerTeamSelected
            string userTeamName = userTeamDetails[0].TeamName;

            string computerTeamName = computerTeamDetails[0].TeamName;

            Console.WriteLine($"Welcome to today's match, it's {computerTeamDetails[0].TeamName} vs {userTeamDetails[0].TeamName}");

            gameEngine.CoinToss();

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
            int choice = Convert.ToInt32(Console.ReadLine());
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
        /// The score board keeping track of the score and wickets
        /// </summary>
        /// <param name="batters">The team who is batting</param>
        /// <param name="bowlingTeam">The team who is bowling</param>
        /// <param name="team">Details of the batting team</param>
        /// <returns>Returns details of the batting team</returns>
        private Team.TeamDetails[] ScoreBoard(string batters, string bowlingTeam, Team.TeamDetails[] team)
        {
            Console.WriteLine(string.Empty);
            for (int i = 0; i < teamSize; i++)
            {
                if (team[i].BattingStatus == "Facing")
                {
                    Console.Write($"* {team[i].PlayerName} ({team[i].Score}) ");
                }

                if (team[i].BattingStatus == "At Bat")
                {
                    Console.Write($" {team[i].PlayerName} ({team[i].Score}) ");
                }
            }

            Console.WriteLine($" Score : {score} / {wickets} ");
            Console.WriteLine(string.Empty);
            return team;
        }

        /// <summary>
        /// Simulation of a ball being bowled
        /// </summary>
        /// <param name="team">The batting team</param>
        /// <returns>Details of the batting team</returns>
        private Team.TeamDetails[] BowlBalled(Team.TeamDetails[] team)
        {
            Random ballOutcome = new Random();
            int actual = ballOutcome.Next(1, dieSize);
            Random isOut = new Random(Guid.NewGuid().GetHashCode());
            int outStatus = isOut.Next(1, dieSize);
            if (actual == 5)
            {
                if (outStatus == 3)
                {
                    this.wickets++;
                    this.BallCommentary(actual);
                    for (int i = 0; i < teamSize; i++)
                    {
                        if (team[i].BattingStatus == "Facing")
                        {
                            team[i].BattingStatus = "Out";
                            Console.WriteLine(Environment.NewLine + $" OUT : {team[i].PlayerName} ({team[i].Score})");
                        }

                        if (team[i].BattingStatus == "Not Yet Batted")
                        {
                            team[i].BattingStatus = "Facing";
                            break;
                        }
                    }
                }

                if (outStatus != 3)
                {
                    Console.WriteLine("It's a dot ball");
                }

                this.ScoreBoard("SA", "Eng", team);

                return team;
            }
            else
            {
                this.BallCommentary(actual);
                this.score += actual;
                for (int i = 0; i < teamSize; i++)
                {
                    if (team[i].BattingStatus == "Facing")
                    {
                        team[i].Score += actual;
                    }
                }

                if (actual == 3 || actual == 1)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < teamSize; i++)
                    {
                        if (team[i].BattingStatus == "Facing")
                        {
                            x = i;
                        }

                        if (team[i].BattingStatus == "At Bat")
                        {
                            y = i;
                        }
                    }

                    team[x].BattingStatus = "At Bat";
                    team[y].BattingStatus = "Facing";
                }
                MilestoneCheck(team);
                this.ScoreBoard("SA", "Eng", team);
                return team;
            }
        }

        /// <summary>
        /// Checks the batsmen to see if they have reached a milestone and mention it in the commentary
        /// </summary>
        /// <param name="team">The batting team</param>
        private void MilestoneCheck(Team.TeamDetails[] team)
        {
            for (int i = 0; i < teamSize; i++)
            {
                if (team[i].LastMileStone == Team.Milestone.None && team[i].Score >= 50)
                {
                    Console.WriteLine(Environment.NewLine + $"Decent knock by {team[i].PlayerName} he's put up the fifty.");
                    team[i].LastMileStone = Team.Milestone.HalfCentury;
                }

                if (team[i].LastMileStone == Team.Milestone.HalfCentury && team[i].Score >= 100)
                {
                    Console.WriteLine(Environment.NewLine + $"A good knock by the lad {team[i].PlayerName} he's got the ton!");
                    team[i].LastMileStone = Team.Milestone.Century;
                }

                if (team[i].LastMileStone == Team.Milestone.Century && team[i].Score >= 150)
                {
                    Console.WriteLine(Environment.NewLine + $"The crowd are on their feet, a fantastic innings {team[i].PlayerName} he's got one fifty!");
                    team[i].LastMileStone = Team.Milestone.OneAndAHalfCentury;
                }

                if (team[i].LastMileStone == Team.Milestone.OneAndAHalfCentury && team[i].Score >= 200)
                {
                    Console.WriteLine(Environment.NewLine + $"Even the bowlers are impressed, a beautiful innings {team[i].PlayerName} he's got the double century!");
                    team[i].LastMileStone = Team.Milestone.DoubleCentury;
                }

                if (team[i].LastMileStone == Team.Milestone.DoubleCentury && team[i].Score >= 250)
                {
                    Console.WriteLine(Environment.NewLine + $"He's not put a foot wrong all innings, a beautiful innings {team[i].PlayerName} he's hit two hundred and fifty runs!");
                    team[i].LastMileStone = Team.Milestone.DoubleAndAHalfCentury;
                }
            }
        }

        /// <summary>
        /// Commentary to be displayed for every ball.
        /// </summary>
        /// <param name="roll">The score of the roll</param>
        private void BallCommentary(int roll)
        {
            Random randomComm = new Random(Guid.NewGuid().GetHashCode());
            int commOutput = randomComm.Next(1, 5);
            switch (roll)
            {
                case 1:
                    switch (commOutput)
                    {
                        case 1:
                            Console.WriteLine($"Nicked for {roll}");
                            break;

                        case 2:
                            Console.WriteLine($"Edged pass the slips for {roll}");
                            break;

                        case 3:
                            Console.WriteLine($"Pushed away for {roll}");
                            break;

                        case 4:
                            Console.WriteLine($"It's in the air, but drops safely for {roll}");
                            break;

                        case 5:
                            Console.WriteLine($"Cut down legside for {roll}");
                            break;
                    }

                    break;

                case 2:
                    switch (commOutput)
                    {
                        case 1:
                            Console.WriteLine($"It's a high ball and its been pulled for {roll}");
                            break;

                        case 2:
                            Console.WriteLine($"Solid cover drive for {roll}");
                            break;

                        case 3:
                            Console.WriteLine($"That's some quick running and the batting team picks up {roll}");
                            break;

                        case 4:
                            Console.WriteLine($"Great fielding, stopping the boundary and the batsmen pick up {roll}");
                            break;

                        case 5:
                            Console.WriteLine($"That's an overthrow on the single and the batsmen run for {roll}");
                            break;
                    }

                    break;

                case 3:
                    switch (commOutput)
                    {
                        case 1:
                            Console.WriteLine($"It's a high ball and its been pulled for {roll}");
                            break;

                        case 2:
                            Console.WriteLine($"Solid cover drive for {roll}");
                            break;

                        case 3:
                            Console.WriteLine($"That's some quick running and the batting team picks up {roll}");
                            break;

                        case 4:
                            Console.WriteLine($"Great fielding, stopping the boundary and the batsmen pick up {roll}");
                            break;

                        case 5:
                            Console.WriteLine($"That's an overthrow on the single and the batsmen run for {roll}");
                            break;
                    }

                    break;

                case 4:
                    switch (commOutput)
                    {
                        case 1:
                            Console.WriteLine($"Expertly cut away for {roll}");
                            break;

                        case 2:
                            Console.WriteLine($"The fast outfield carries the drive for {roll}");
                            break;

                        case 3:
                            Console.WriteLine($"The fielder gets to it but his foot touches the ropes it's a  {roll}");
                            break;

                        case 4:
                            Console.WriteLine($"A powerful slog taking one bounce and that's {roll}");
                            break;

                        case 5:
                            Console.WriteLine($"Edged between the slips and it races away for {roll}");
                            break;
                    }

                    break;

                case 5:

                    switch (commOutput)
                    {
                        case 1:
                            Console.WriteLine("Poor defence and the bails are off, bowled clean");
                            break;

                        case 2:
                            Console.WriteLine("He's nicked it and that's a great catch by the keeper. He's gone");
                            break;

                        case 3:
                            Console.WriteLine("It hits the pad, the bowler appeals and the finger is up, he's been given LBW");
                            break;

                        case 4:
                            Console.WriteLine($"The batsman advances down the wicket he misses it and the keeper whips off the bails. He's out");
                            break;

                        case 5:
                            Console.WriteLine("A big slog and that's caught, holed out to mid off");
                            break;
                    }

                    break;

                case 6:
                    switch (commOutput)
                    {
                        case 1:
                            Console.WriteLine($"That's a terrible delivery and it's been punished for {roll}");
                            break;

                        case 2:
                            Console.WriteLine($"That's a big hit and it sails for {roll}");
                            break;

                        case 3:
                            Console.WriteLine($"Smashed right into the sunscreen that's a big {roll}");
                            break;

                        case 4:
                            Console.WriteLine($"It's going out of the car park, he's absolutely demolished that ball for {roll}");
                            break;

                        case 5:
                            Console.WriteLine($"A beautiful reverse sweep and he gets his rewards that's a great {roll}");
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// Displays the final scoreboard at the end of the inning
        /// </summary>
        /// <param name="team">The team who batted</param>
        private void DisplayInningsScoreboard(Team.TeamDetails[] team)
        {
            for (int i = 0; i < teamSize; i++)
            {
                Console.WriteLine($" {team[i].PlayerName} ({team[i].Score})");
            }
        }
    }
}