using Moq;
using NUnit.Framework;
using TheRavenDB.TextAdventure.Core.Engine;
using TheRavenDB.TextAdventure.Core.Entities.Maps;
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
            var game = GetGame(adventureRepository.Object);
            // Act
            game.Load("Colossal Cave Adventure");
            // Assert
            adventureRepository.Verify(r => r.GetAdventureStartingRoom("Colossal Cave Adventure"));
        }

        [Test]
        public void Describe_WhenCalledAfterLoadingTheGame_ShouldReturnTheDescriptionOfTheCurrentRoomInTheGame()
        {
            // Arrange
            var startingRoom = new Room
            {
                Description = "Welcome to Cave Adventure!!!"
            };
            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);
            var game = GetGame(adventureRepo.Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var firstRoomDescription = game.Describe();
            // Assert
            Assert.That(firstRoomDescription, Is.EqualTo(startingRoom.Description));
        }

        private Game GetGame(IAdventureRepository adventureRepo)
        {
            return new Game(adventureRepo);
        }
    }
}