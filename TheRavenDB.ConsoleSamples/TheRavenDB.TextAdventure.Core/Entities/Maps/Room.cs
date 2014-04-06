
namespace TheRavenDB.TextAdventure.Core.Entities.Maps
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoomPathways Pathways { get; set; }
        public ArbitraryActions ArbitraryActions { get; set; }

        public Room()
        {
            Pathways = new RoomPathways();
            ArbitraryActions = new ArbitraryActions(); 
        }
    }
}