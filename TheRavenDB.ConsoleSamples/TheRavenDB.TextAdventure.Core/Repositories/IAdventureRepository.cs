using System.ComponentModel.Composition;
using TheRavenDB.TextAdventure.Core.Entities.Maps;

namespace TheRavenDB.TextAdventure.Core.Repositories
{
    [InheritedExport]
    public interface IAdventureRepository
    {
        Room GetAdventureStartingRoom(string adventure);
    }
}