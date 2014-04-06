using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheRavenDB.TextAdventure.Core.Exceptions;

namespace TheRavenDB.TextAdventure.Core.Engine
{
    public class ActionParser : IActionParser
    {
        public static readonly ReadOnlyCollection<string> MotionActions = new ReadOnlyCollection<string>(new List<string>{"go to", "go", "walk to", "walk"});

        public ActionPredicate ParseAction(string action)
        {
            if (IsMotionAction(action))
            {
                foreach (var motionAction in MotionActions.Where(action.Contains))
                    return new ActionPredicate
                    {
                        Verb = motionAction,
                        Object = action.Replace(motionAction, "").Trim()
                    };
            }
            throw new CantParseActionException(action);
        }

        public bool IsMotionAction(string action)
        {
            return MotionActions.Any(action.Contains);
        }
    }
}