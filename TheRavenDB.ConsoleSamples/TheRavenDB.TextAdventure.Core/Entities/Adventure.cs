namespace TheRavenDB.TextAdventure.Core.Entities
{
    public class Adventure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Room StartingRoom { get; set;}
    }
}