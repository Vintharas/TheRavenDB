using System.ComponentModel.Composition;
using System.Linq;
using Raven.Client;
using TheRavenDB.TextAdventure.Core.Entities;
using TheRavenDB.TextAdventure.Core.Entities.Maps;
using TheRavenDB.TextAdventure.Core.Repositories;

namespace TheRavenDB.TextAdventure.Utils.Generators
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AdventureGenerator : IAdventureGenerator
    {
        private readonly IDocumentStore documentStore;

        [ImportingConstructor]
        public AdventureGenerator(IDocumentStoreFacade documentStoreFacade)
        {
            documentStore = documentStoreFacade.GetDocumentStore();
        }

        public bool HasAdventure(string adventureName)
        {
            using (var documentSession = documentStore.OpenSession())
            {
                return documentSession.Query<Adventure>().Any(a => a.Name == adventureName);
            }
        }

        public void CreateAdventure(Adventure adventure)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(adventure.Map.StartingRoom);
                session.Store(adventure);
                session.SaveChanges();
            }
        }

        public void AddRooms(params Room[] rooms)
        {
            using (var session = documentStore.OpenSession())
            {
                foreach (var room in rooms)
                    session.Store(room);
                session.SaveChanges();
            }
        }
    }
}