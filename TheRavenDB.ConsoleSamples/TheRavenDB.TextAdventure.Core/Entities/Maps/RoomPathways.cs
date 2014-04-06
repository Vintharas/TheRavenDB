using System.Collections.Generic;
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

        public void Add(RoomPathway pathway)
        {
            this[pathway.Direction] = pathway.RoomId;
        }
    }
}