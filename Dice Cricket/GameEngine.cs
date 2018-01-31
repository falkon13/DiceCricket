﻿//-----------------------------------------------------------------------
// <copyright file="GameEngine.cs" company="Falkon">
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
    /// The simulation of the game itself, holds all game logic.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// The score of the current batting team.
        /// </summary>
        private int score;

        /// <summary>
        /// The amount of wickets the batting team has lost.
        /// </summary>
        private int wickets;

        /// <summary>
        /// Method that holds the game engine logic.
        /// </summary>
        public void Engine()
        {
            var gameEngine = new GameEngine();
            Team userTeam = new Team();
            Team computerTeam = new Team();
            int teamSelected = TeamSelection.UserSelectingTeam();

            var userTeamDetails = userTeam.PopulateTeamPlayers(teamSelected);
            var computerTeamDetails = computerTeam.PopulateTeamPlayers(2);
            string userTeamName = userTeamDetails[0].TeamName;
            string computerTeamName = computerTeamDetails[0].TeamName;
            Console.WriteLine($"Welcome to today's match, it's {computerTeamDetails[0].TeamName} vs {userTeamDetails[0].TeamName}");
            gameEngine.CoinToss();
            Console.WriteLine($"Here is the line up for {userTeamDetails[0].TeamName}");
            this.DisplayTeam(teamSelected);
            Console.WriteLine("The game is ready to play. Press any key to start");
            Console.ReadKey();
            gameEngine.ScoreBoard(userTeamName, computerTeamName, userTeamDetails);
            while (gameEngine.wickets < 10)
            {
                userTeamDetails = gameEngine.BowlBalled(userTeamDetails);

                Console.ReadKey();
            }

            Console.WriteLine($"Team is out for {gameEngine.score}");
            this.DisplayInningsScoreboard(userTeamDetails);
        }

        /// <summary>
        /// Displays the team and their lineup.
        /// </summary>
        /// <param name="teamSelected">The team selected</param>
        public void DisplayTeam(int teamSelected)
        {
            var teamToDisplay = new Team();
            var teamToDisplayDetails = teamToDisplay.PopulateTeamPlayers(teamSelected);
            for (int i = 0; i < 11; i++)
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
            for (int i = 0; i < 11; i++)
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
            int actual = ballOutcome.Next(1, 6);
            Random isOut = new Random(Guid.NewGuid().GetHashCode());
            int outStatus = isOut.Next(1, 6);
            if (actual == 5)
            {
                if (outStatus == 3)
                {
                    this.wickets++;
                    this.BallCommentary(actual);
                    for (int i = 0; i < 11; i++)
                    {
                        if (team[i].BattingStatus == "Facing")
                        {
                            team[i].BattingStatus = "Out";
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
                for (int i = 0; i < 11; i++)
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
                    for (int i = 0; i < 11; i++)
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

                this.ScoreBoard("SA", "Eng", team);
                return team;
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
                            Console.WriteLine($"It's going out of the car park, he's absouletly demolished that ball for {roll}");
                            break;
                        case 5:
                            Console.WriteLine($"A beautiful reverse sweep and he get's his rewards that's a great {roll}");
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
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine($" {team[i].PlayerName} ({team[i].Score})");
            }
        }
    }
}