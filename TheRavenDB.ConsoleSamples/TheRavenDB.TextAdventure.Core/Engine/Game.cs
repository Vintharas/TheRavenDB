using System.ComponentModel.Composition;
using TheRavenDB.TextAdventure.Core.Entities.Maps;
using TheRavenDB.TextAdventure.Core.Repositories;

namespace TheRavenDB.TextAdventure.Core.Engine
{
    public class Game : IGame
    {
        private readonly IAdventureRepository adventureRepository;
        public Room CurrentRoom { get; private set; }

        [ImportingConstructor]
        public Game(IAdventureRepository adventureRepository)
        {
            this.adventureRepository = adventureRepository;
        }

        public void Load(string adventure)
        {
            CurrentRoom = adventureRepository.GetAdventureStartingRoom(adventure);
        }

        public string Describe()
        {
            return CurrentRoom.Description;
        }
    }

}