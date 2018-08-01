//-----------------------------------------------------------------------
// <copyright file="GameEngine.cs" company="Falkon13">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Dice_Cricket
{
    using System;
    using System.Linq;

    /// <summary>
    /// The simulation of the game itself, holds all game logic.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// Number of faces on the die being rolled
        /// </summary>
        private const int DieSize = 6;

        /// <summary>
        /// Number of players on a team
        /// </summary>
        private const int TeamSize = 11;

        /// <summary>
        /// Class that handles post match stuff
        /// </summary>
        private readonly PostMatch postMatch = new PostMatch();

        /// <summary>
        /// Class that handles pre match stuff
        /// </summary>
        private readonly PreMatch preMatch = new PreMatch();

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
        /// Entry point of the game engine
        /// </summary>
        /// <param name="userTeamNumb">The user's team</param>
        public void Engine(int userTeamNumb)
        {
            var setup = this.preMatch.MatchSetup(userTeamNumb, this.currentRound);

            var userTeam = setup.Item1;
            var computerTeam = setup.Item2;
            var gameEngine = setup.Item3;
            var userTeamDetails = setup.Item4;
            var computerTeamDetails = setup.Item5;
            userTeamNumb = setup.Item6;

            while (gameEngine.wickets < 10)
            {
                userTeamDetails = gameEngine.BowlBalled(userTeamDetails, computerTeamDetails);

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
                computerTeamDetails = gameEngine.BowlBalled(computerTeamDetails, userTeamDetails);

                Console.ReadKey();
            }

            int computerScore = gameEngine.score;
            this.postMatch.CalculateBestPlayer(computerTeamDetails, userTeamDetails);
            if (userScore > computerScore)
            {
                Console.WriteLine("USER WINS");
                Console.WriteLine("You have progressed to the next round!");

                switch (this.currentRound)
                {
                    case "Round of 16":
                        this.currentRound = "Quarter Final";
                        this.preMatch.AvailableTeams = this.postMatch.KnockoutTeams(7, this.preMatch.AvailableTeams);
                        this.NewGame(userTeamNumb);

                        break;

                    case "Quarter Final":
                        this.currentRound = "Semi Final";
                        this.preMatch.AvailableTeams = this.postMatch.KnockoutTeams(3, this.preMatch.AvailableTeams);
                        this.NewGame(userTeamNumb);

                        break;

                    case "Semi Final":
                        this.currentRound = "Final";
                        this.preMatch.AvailableTeams = this.postMatch.KnockoutTeams(1, this.preMatch.AvailableTeams);
                        this.NewGame(userTeamNumb);
                        break;

                    case "Final":
                        Console.WriteLine("You did it! YOU WON!!!!!!!!!!!!!!!!!!!!!!!!!");
                        break;
                }
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

        /// <summary>
        /// The score board keeping track of the score and wickets
        /// </summary>
        /// <param name="batters">The team who is batting</param>
        /// <param name="bowlingTeam">The team who is bowling</param>
        /// <param name="team">Details of the batting team</param>
        /// <returns>Returns details of the batting team</returns>
        public Team.TeamDetails[] ScoreBoard(string batters, string bowlingTeam, Team.TeamDetails[] team)
        {
            Console.WriteLine(string.Empty);
            for (int i = 0; i < TeamSize; i++)
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
        /// Starts a new match
        /// </summary>
        /// <param name="userTeam">The user's team</param>
        private void NewGame(int userTeam)
        {
            this.Engine(userTeam);
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
        /// Simulation of a ball being bowled
        /// </summary>
        /// <param name="battingTeam">The batting team</param>
        /// <param name="bowlingTeam">The bowling team</param>
        /// <returns>Details of the batting team</returns>
        private Team.TeamDetails[] BowlBalled(Team.TeamDetails[] battingTeam, Team.TeamDetails[] bowlingTeam)
        {
            Random ballOutcome = new Random();
            int actual = ballOutcome.Next(1, DieSize);
            Random isOut = new Random(Guid.NewGuid().GetHashCode());
            int outStatus = isOut.Next(1, DieSize);
            if (actual == 5)
            {
                if (outStatus == 3 || outStatus == 6)
                {
                    this.wickets++;
                    int method = this.BallCommentary(actual);
                    for (int i = 0; i < TeamSize; i++)
                    {
                        if (battingTeam[i].BattingStatus == "Facing")
                        {
                            battingTeam[i].BattingStatus = "Out";
                            this.DetermineWicket(isOut, battingTeam, bowlingTeam, i, method);
                        }

                        if (battingTeam[i].BattingStatus == "Not Yet Batted")
                        {
                            battingTeam[i].BattingStatus = "Facing";
                            break;
                        }
                    }
                }

                if (outStatus != 3 && outStatus != 6)
                {
                    Console.WriteLine("It's a dot ball");
                }

                this.ScoreBoard(null, null, battingTeam);

                return battingTeam;
            }
            else
            {
                this.BallCommentary(actual);
                this.score += actual;
                for (int i = 0; i < TeamSize; i++)
                {
                    if (battingTeam[i].BattingStatus == "Facing")
                    {
                        battingTeam[i].Score += actual;
                    }
                }

                if (actual == 3 || actual == 1)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < TeamSize; i++)
                    {
                        if (battingTeam[i].BattingStatus == "Facing")
                        {
                            x = i;
                        }

                        if (battingTeam[i].BattingStatus == "At Bat")
                        {
                            y = i;
                        }
                    }

                    battingTeam[x].BattingStatus = "At Bat";
                    battingTeam[y].BattingStatus = "Facing";
                }

                this.MilestoneCheck(battingTeam);
                this.ScoreBoard(null, null, battingTeam);
                return battingTeam;
            }
        }

        /// <summary>
        /// Determines how a wicket has fallen
        /// </summary>
        /// <param name="isOut">RNG for who is the bowler responsible</param>
        /// <param name="battingTeam">The batting team</param>
        /// <param name="bowlingTeam">The bowling team</param>
        /// <param name="batter">The batter affected</param>
        /// <param name="method">How the batsman was out</param>
        private void DetermineWicket(Random isOut, Team.TeamDetails[] battingTeam, Team.TeamDetails[] bowlingTeam, int batter, int method)
        {
            int bowler = isOut.Next(0, 4);
            bowler = 10 - bowler;
            var keeper = bowlingTeam.Where(m => m.IsKeeper == true).Select(m => m.BattingOrder).First() - 1;
            int fielder = isOut.Next(0, 10);
            while (fielder == keeper || fielder == bowler)
            {
                fielder = isOut.Next(0, 10);
            }

            switch (method)
            {
                case 1:
                    Console.WriteLine(Environment.NewLine + $" OUT : {battingTeam[batter].PlayerName} ({battingTeam[batter].Score}) b {bowlingTeam[bowler].PlayerName}");

                    break;

                case 2:
                    Console.WriteLine(Environment.NewLine + $" OUT : {battingTeam[batter].PlayerName} ({battingTeam[batter].Score}) c †{bowlingTeam[keeper].PlayerName} b {bowlingTeam[bowler].PlayerName}");
                    bowlingTeam[keeper].FieldingWickets += 1;
                    break;

                case 3:
                    Console.WriteLine(Environment.NewLine + $" OUT : {battingTeam[batter].PlayerName} ({battingTeam[batter].Score}) lbw b {bowlingTeam[bowler].PlayerName}");

                    break;

                case 4:
                    Console.WriteLine(Environment.NewLine + $" OUT : {battingTeam[batter].PlayerName} ({battingTeam[batter].Score}) st †{bowlingTeam[keeper].PlayerName} b {bowlingTeam[bowler].PlayerName}");
                    bowlingTeam[keeper].FieldingWickets += 1;
                    break;

                case 5:
                    Console.WriteLine(Environment.NewLine + $" OUT : {battingTeam[batter].PlayerName} ({battingTeam[batter].Score}) c {bowlingTeam[fielder].PlayerName} b {bowlingTeam[bowler].PlayerName}");
                    bowlingTeam[fielder].FieldingWickets += 1;
                    break;
            }

            bowlingTeam[bowler].BowlingWickets += 1;
        }

        /// <summary>
        /// Checks the batsmen to see if they have reached a milestone and mention it in the commentary
        /// </summary>
        /// <param name="team">The batting team</param>
        private void MilestoneCheck(Team.TeamDetails[] team)
        {
            for (int i = 0; i < TeamSize; i++)
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
        /// <returns>Returns the commentary output</returns>
        private int BallCommentary(int roll)
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

            return commOutput;
        }

        /// <summary>
        /// Displays the final scoreboard at the end of the inning
        /// </summary>
        /// <param name="team">The team who batted</param>
        private void DisplayInningsScoreboard(Team.TeamDetails[] team)
        {
            for (int i = 0; i < TeamSize; i++)
            {
                Console.WriteLine($" {team[i].PlayerName} ({team[i].Score})");
            }
        }
    }
}