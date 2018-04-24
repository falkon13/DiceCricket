//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Jonathan le Grange">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Dice_Cricket
{
    using System;

    /// <summary>
    /// Handles starting of program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of application
        /// </summary>
        /// <param name="args">Console line arguments</param>
        private static void Main(string[] args)
        {
            var gameEngine = new GameEngine();
            gameEngine.Engine();
            Console.ReadKey();
        }
    }
}