using System.ComponentModel.Composition;
using Raven.Client;

namespace TheRavenDB.TextAdventure.Core.Repositories
{
    [InheritedExport]
    public interface IDocumentStoreFacade
    {
        IDocumentStore GetDocumentStore();
    }
}