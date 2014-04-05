using Raven.Client;
using Raven.Client.Document;

namespace TheRavenDB.TextAdventure.Core.Repositories
{
    public class DocumentStoreFacade : IDocumentStoreFacade
    {
        public IDocumentStore GetDocumentStore()
        {
            return new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "textadventure"
            }.Initialize();
        }
    }
}