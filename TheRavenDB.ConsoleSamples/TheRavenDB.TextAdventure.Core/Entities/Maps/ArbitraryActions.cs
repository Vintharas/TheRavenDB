using System.Collections.Generic;
using System.Linq;
using Raven.Abstractions.Extensions;

namespace TheRavenDB.TextAdventure.Core.Entities.Maps
{
    // it would be interesting to abstract more and generalize these concept of actions
    //   - motion actions
    //   - arbitrary actions
    //   - item actions...
    //   etc XD
    public class ArbitraryActions : Dictionary<string, string>
    {

        public ArbitraryActions() { }
        public ArbitraryActions(params ArbitraryAction[] actions)
        {
            actions.ForEach(a => this[a.Action] = a.Response);
        }

        public IEnumerable<string> Actions { get { return this.Select(kv => kv.Key).ToArray(); } }
    }
}