using System.ComponentModel.Composition;
using System.Linq;
using Raven.Client;
using TheRavenDB.TextAdventure.Core.Entities;
using TheRavenDB.TextAdventure.Core.Entities.Maps;
using TheRavenDB.TextAdventure.Core.Exceptions;

namespace TheRavenDB.TextAdventure.Core.Repositories
{
    public class AdventureRepository : IAdventureRepository
    {
        private readonly IDocumentStore documentStore;

        [ImportingConstructor]
        public AdventureRepository(IDocumentStoreFacade documentStoreFacade)
        {
            documentStore = documentStoreFacade.GetDocumentStore();
        }
        
        public Room GetAdventureStartingRoom(string adventureName)
        {
            using (var session = documentStore.OpenSession())
            {
                var adventure = session.Query<Adventure>().FirstOrDefault(a => a.Name == adventureName);
                if (adventure == null)
                    throw new AdventureNotFoundException();
                else
                {
                    var room = session.Load<Room>(adventure.Map.StartingRoom.Id);
                    return room;
                }
            }
        }

        public Room GetRoom(string roomId)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Load<Room>(roomId);
            }
        }
    }
}