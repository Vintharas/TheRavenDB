using System.ComponentModel.Composition;

namespace TheRavenDB.TextAdventure.Core.Engine
{
    [InheritedExport]
    public interface IActionParser
    {
        ActionPredicate ParseAction(string action);
        bool IsMotionAction(string action);
    }
}