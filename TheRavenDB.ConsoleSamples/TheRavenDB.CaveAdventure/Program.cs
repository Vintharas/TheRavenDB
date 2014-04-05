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
            // Continue with... TextAdventure.Utils to create adventures
            var gameGenerator = new CaveAdventureGameGenerator();

            // Continue with... ConsoleGameHost to provide gameplay in a console app
            var gameHost = new GameHost();
            var game = gameHost.HostGame();

            game.Load("Colossal Cave Adventure");
            
            Console.ReadLine();
        }

    }

    internal class CaveAdventureGameGenerator
    {
        public void GenerateGame()
        {
        }
    }
}
