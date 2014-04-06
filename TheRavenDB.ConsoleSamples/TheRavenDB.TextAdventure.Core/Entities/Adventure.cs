using TheRavenDB.TextAdventure.Core.Entities.Maps;

namespace TheRavenDB.TextAdventure.Core.Entities
{
    public class Adventure
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Map Map { get; set;}
    }
}