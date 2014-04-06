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
using System.Collections.Generic;
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
            // Continue with... TextAdventure.Utils to create adventures
            var caveAdventureGenerator = new CaveAdventureGenerator();
            caveAdventureGenerator.Generate();

            // Continue with... ConsoleGameHost to provide gameplay in a console app
            var gameHost = new GameHost();
            var game = gameHost.HostGame();

            game.Load(CaveAdventureGenerator.ColossalCaveAdventure);
            Console.WriteLine(game.Describe());
            
            Console.ReadLine();
        }
    }
}
