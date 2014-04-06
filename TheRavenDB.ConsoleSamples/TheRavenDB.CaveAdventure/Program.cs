/*
 *  Code samples for
 *  "Edgar Allan Poe
 *    presents
 *      The Raven DB"
 *  
 *  Colossal Cave Adventure Console Game
 *
 * 
 *  by Jaime Gonzalez Garcia
 * 
 */

using System;
using TheRavenDB.TextAdventure.Core.Engine;

namespace TheRavenDB.CaveAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
        }

        private static void RunGame()
        {
            GenerateGameIfItDoesntExist();

            var gameHost = new GameHost();
            var game = gameHost.HostGame();

            game.Load(SwenugTextAdventureGenerator.SwenugTextAdventure);

            // TODO: extract to a ConsoleGameHost
            var action = "";
            while (action != "q" && action != "quit")
            {
                Console.WriteLine(game.Describe());
                
                Console.Write("$ ");
                action = Console.ReadLine();
                game.PerformAction(action);
                Console.Clear();
            }

            Console.WriteLine("\n\nByeeeee!!! :)\n\n");
        }

        private static void GenerateGameIfItDoesntExist()
        {
            var caveAdventureGenerator = new SwenugTextAdventureGenerator();
            caveAdventureGenerator.Generate();
        }
    }
}
