using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using TheRavenDB.TextAdventure.Core.Repositories;
using TheRavenDB.TextAdventure.Utils.Generators;

namespace TheRavenDB.TextAdventure.Utils.Factories
{
    public class AdventureGeneratorFactory
    {
        private readonly CompositionContainer container;

        public AdventureGeneratorFactory()
        {
            var utilsCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var coreCatalog = new AssemblyCatalog(Assembly.GetAssembly(typeof(IDocumentStoreFacade))); 
            var catalogs = new AggregateCatalog(utilsCatalog, coreCatalog);
            container = new CompositionContainer(catalogs);
        }

        public IAdventureGenerator Create()
        {
            return container.GetExportedValue<IAdventureGenerator>();
        }
    }
}