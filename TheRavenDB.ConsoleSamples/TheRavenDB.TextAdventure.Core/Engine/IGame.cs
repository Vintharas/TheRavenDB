using System.ComponentModel.Composition;

namespace TheRavenDB.TextAdventure.Core.Engine
{
    [InheritedExport]
    public interface IGame
    {
        void Load(string adventure);
        string Describe();
        bool PerformAction(string action);
    }
}