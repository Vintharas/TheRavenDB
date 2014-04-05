using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace TheRavenDB.TextAdventure.Core.Engine
{
    public class GameHost
    {
        public IGame HostGame()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            var game = container.GetExportedValue<IGame>();
            return game;
        }

    }
}