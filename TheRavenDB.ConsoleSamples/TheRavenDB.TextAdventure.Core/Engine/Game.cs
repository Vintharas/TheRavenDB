using System;
using System.ComponentModel.Composition;
using System.Linq;
using TheRavenDB.TextAdventure.Core.Entities.Maps;
using TheRavenDB.TextAdventure.Core.Repositories;

namespace TheRavenDB.TextAdventure.Core.Engine
{
    public class Game : IGame
    {
        private const string YouSeeExits = "You see the exits: {0}";
        private const string YouSeeNoExits = "You cannot see any apparent exit...";
        private const string ActionResultMessage = ">> {0}";
        private const string DescribeTemplate = @"
====== {0} =====

{1}

{2}

{3}
";
        
        private readonly IAdventureRepository adventureRepository;
        private readonly IActionParser actionParser;
        private Room currentRoom;
        private string actionMessage; // TODO: don't like this implementation, there's too much state xD

        [ImportingConstructor]
        public Game(IAdventureRepository adventureRepository, IActionParser actionParser)
        {
            this.adventureRepository = adventureRepository;
            this.actionParser = actionParser;
        }

        public void Load(string adventure)
        {
            currentRoom = adventureRepository.GetAdventureStartingRoom(adventure);
        }

        public string Describe()
        {
            var exitsDescription = currentRoom.Pathways.Any() ?  String.Format(YouSeeExits, string.Join(", ", currentRoom.Pathways.Exits)) : YouSeeNoExits;
            var actionResultDescription = string.IsNullOrEmpty(actionMessage)? "" : string.Format(ActionResultMessage, actionMessage);

            return string.Format(DescribeTemplate, currentRoom.Name, currentRoom.Description, exitsDescription, actionResultDescription);
        }

        public bool PerformAction(string action)
        {
            if (IsArbitraryAction(action))
                return PerformArbitraryAction(action);
            if (actionParser.IsMotionAction(action))
                return PerformMotionAction(action);
            
            // no action performed
            actionMessage = "No comprendo?!";
            return false;
        }

        private bool IsArbitraryAction(string action)
        {
            // if 
            //   action in room is: "say hellow world"
            //   and the player writes: "say hello world to everyone"
            // it should still work
            return currentRoom.ArbitraryActions.Actions.Any(action.Contains);
        }

        private bool PerformArbitraryAction(string action)
        {
            var literalActionInRoom = currentRoom.ArbitraryActions.Actions.First(action.Contains);
            actionMessage = currentRoom.ArbitraryActions[literalActionInRoom];
            return true;
        }

        private bool PerformMotionAction(string action)
        {
            var actionPredicate = actionParser.ParseAction(action);
            if (PredicateHasValidPathway(actionPredicate))
            {
                var direction = currentRoom.Pathways.Exits.First(exit => actionPredicate.Object.Contains(exit));
                currentRoom = adventureRepository.GetRoom(currentRoom.Pathways[direction]);
                return true;
            }
            else
            {
                actionMessage = string.Format("There is no such place to go! {0}", actionPredicate.Object);
                return false;
            }
        }

        private bool PredicateHasValidPathway(ActionPredicate actionPredicate)
        {
            return currentRoom.Pathways.Exits.Any(exit => actionPredicate.Object.Contains(exit));
        }
    }

}