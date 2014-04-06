using System.Collections.Generic;
using System.Linq;
using Raven.Abstractions.Extensions;

namespace TheRavenDB.TextAdventure.Core.Entities.Maps
{
    public class RoomPathways : Dictionary<string, string>
    {
        public RoomPathways() { }

        public RoomPathways(params RoomPathway[] roomPathway)
        {
            roomPathway.ForEach(r => this[r.Direction] = r.RoomId);
        }

        public IEnumerable<string> Exits { get { return this.Select(kv => kv.Key).ToArray(); } }
    }
}