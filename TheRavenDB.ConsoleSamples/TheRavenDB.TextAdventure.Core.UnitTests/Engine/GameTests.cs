using Moq;
using NUnit.Framework;
using TheRavenDB.TextAdventure.Core.Engine;
using TheRavenDB.TextAdventure.Core.Exceptions;
using TheRavenDB.TextAdventure.Core.Repositories;

namespace TheRavenDB.TextAdventure.Core.UnitTests.Engine
{
    [TestFixture]
    public class GameTests
    {

        [Test]
        public void Load_WhenGivenTheNameOfAnTextAdventureThatDoesNoExist_ShouldLoadTheAdventureFirstRoom()
        {
            // Arrange
            var adventureRepository = new Mock<IAdventureRepository>();
            var game = new Game(adventureRepository.Object);
            // Act
            game.Load("Colossal Cave Adventure");
            // Assert
            adventureRepository.Verify(r => r.GetAdventureStartingRoom("Colossal Cave Adventure"));
        }
         
    }
}