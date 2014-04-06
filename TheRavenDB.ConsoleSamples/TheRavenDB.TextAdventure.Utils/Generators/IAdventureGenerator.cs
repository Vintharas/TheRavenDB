using System.ComponentModel.Composition;
using TheRavenDB.TextAdventure.Core.Entities;
using TheRavenDB.TextAdventure.Core.Entities.Maps;

namespace TheRavenDB.TextAdventure.Utils.Generators
{
    [InheritedExport]
    public interface IAdventureGenerator
    {
        bool HasAdventure(string adventureName);
        void CreateAdventure(Adventure adventure);
        void AddRooms(params Room[] rooms);
    }
}