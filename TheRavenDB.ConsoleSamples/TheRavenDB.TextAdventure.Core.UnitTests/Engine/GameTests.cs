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
            var game = GetGame(adventureRepository.Object, new Mock<IActionParser>().Object);
            // Act
            game.Load("Colossal Cave Adventure");
            // Assert
            adventureRepository.Verify(r => r.GetAdventureStartingRoom("Colossal Cave Adventure"));
        }

        [Test]
        public void Describe_WhenCalledAfterLoadingTheGame_ShouldReturnTheNameAndDescriptionOfTheCurrentRoomInTheGame()
        {
            // Arrange
            var startingRoom = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!"
            };
            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);
            var game = GetGame(adventureRepo.Object, new Mock<IActionParser>().Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var firstRoomDescription = game.Describe();
            // Assert
            Assert.That(firstRoomDescription, Contains.Substring(startingRoom.Name));
            Assert.That(firstRoomDescription, Contains.Substring(startingRoom.Description));
        }

        [Test]
        public void Describe_WhenCalledAfterLoadingTheGame_ShouldReturnThePossibleExistsOfTheCurrentRoomInTheGame()
        {
            // Arrange
            var startingRoom = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!",
                Pathways = new RoomPathways(
                    new RoomPathway{ Direction = "outside", RoomId = "rooms/2"},
                    new RoomPathway{ Direction = "up", RoomId = "rooms/3"})
            };
            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);
            var game = GetGame(adventureRepo.Object, new Mock<IActionParser>().Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var firstRoomDescription = game.Describe();
            // Assert
            Assert.That(firstRoomDescription, Contains.Substring("You see the exits: outside, up"));
        }

        [Test]
        public void Describe_WhenTheCurrentRoomHasNoPathways_ShouldReturnThatThereAreNoExitsAvailable()
        {
            // Arrange
            var roomWithNoPathways = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!"
            };
            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(roomWithNoPathways);
            var game = GetGame(adventureRepo.Object, new Mock<IActionParser>().Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var firstRoomDescription = game.Describe();
            // Assert
            Assert.That(firstRoomDescription, Contains.Substring("You cannot see any apparent exit..."));
        }

        // should be able to use synonyms
        [TestCase("go")]
        [TestCase("go to")]
        [TestCase("walk")]
        [TestCase("walk to")]
        public void PerformAction_WhenGivenAMotionAction_ShouldMoveToAnotherRoom(string motionActionVerb)
        {
            // Arrange
            var motionAction = string.Format("{0} outside", motionActionVerb);
            var startingRoom = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!",
                Pathways = new RoomPathways(
                    new RoomPathway{ Direction = "outside", RoomId = "rooms/2"})
            };
            var anotherRoom = new Room
            {
                Name = "Another room",
                Description = "This is another room"
            };

            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);
            adventureRepo.Setup(r => r.GetRoom("rooms/2")).Returns(anotherRoom);

            var actionParser = new Mock<IActionParser>();
            actionParser.Setup(ap => ap.IsMotionAction(motionAction)).Returns(true);
            actionParser.Setup(ap => ap.ParseAction(motionAction)).Returns(new ActionPredicate{ Verb = motionActionVerb, Object = "outside"});

            var game = GetGame(adventureRepo.Object, actionParser.Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var actionSucceeded = game.PerformAction(motionAction);
            // Assert
            var newRoomDescription = game.Describe();
            Assert.That(actionSucceeded);
            Assert.That(newRoomDescription, Contains.Substring(anotherRoom.Description));
        }


        [Test]
        public void PerformAction_WhenGivenAMotionActionThatLeadsToNowhere_ShouldShowAnErrorMessage()
        {
            // Arrange
            var motionAction = "go left";
            var startingRoom = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!",
                Pathways = new RoomPathways(
                    new RoomPathway{ Direction = "outside", RoomId = "rooms/2"})
            };
            var anotherRoom = new Room
            {
                Name = "Another room",
                Description = "This is another room"
            };

            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);
            adventureRepo.Setup(r => r.GetRoom("rooms/2")).Returns(anotherRoom);

            var actionParser = new Mock<IActionParser>();
            actionParser.Setup(ap => ap.IsMotionAction(motionAction)).Returns(true);
            actionParser.Setup(ap => ap.ParseAction(motionAction)).Returns(new ActionPredicate{ Verb = "go", Object = "left"});

            var game = GetGame(adventureRepo.Object, actionParser.Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var actionSucceeded = game.PerformAction(motionAction);
            // Assert
            var newRoomDescription = game.Describe();
            Assert.That(actionSucceeded, Is.Not.True);
            Assert.That(newRoomDescription, Contains.Substring(startingRoom.Description));
            Assert.That(newRoomDescription, Contains.Substring(">> There is no such place to go! left"));
        }

        [Test]
        public void PerformAction_WhenGivenAnActionThatTheGameCantUnderstand_ShouldShowSomeFunnyMessage()
        {
            // Arrange
            var motionAction = "what does the duck say?";
            var startingRoom = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!",
                Pathways = new RoomPathways(
                    new RoomPathway{ Direction = "outside", RoomId = "rooms/2"})
            };
            var anotherRoom = new Room
            {
                Name = "Another room",
                Description = "This is another room"
            };

            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);
            adventureRepo.Setup(r => r.GetRoom("rooms/2")).Returns(anotherRoom);

            var actionParser = new Mock<IActionParser>();
            actionParser.Setup(ap => ap.IsMotionAction(motionAction)).Returns(false);

            var game = GetGame(adventureRepo.Object, actionParser.Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var actionSucceeded = game.PerformAction(motionAction);
            // Assert
            var newRoomDescription = game.Describe();
            Assert.That(actionSucceeded, Is.Not.True);
            Assert.That(newRoomDescription, Contains.Substring(startingRoom.Description));
            Assert.That(newRoomDescription, Contains.Substring(">> No comprendo?!"));
        }

        [Test]
        public void PerformAction_WhenGivenAnArbitraryAction_ShouldShowMessageWithResponseToThatParticularAction()
        {
            // Arrange
            var arbitraryAction = "say hello world";
            var startingRoom = new Room
            {
                Name = "The Cave",
                Description = "Welcome to Cave Adventure!!!",
                ArbitraryActions = new ArbitraryActions(
                    new ArbitraryAction{ Action = "say hello world", Response = "Hello back!"})
            };

            var adventureRepo = new Mock<IAdventureRepository>();
            adventureRepo.Setup(r => r.GetAdventureStartingRoom("Colossal Cave Adventure")).Returns(startingRoom);

            var game = GetGame(adventureRepo.Object, new Mock<IActionParser>().Object);
            game.Load("Colossal Cave Adventure");
            // Act
            var actionSucceeded = game.PerformAction(arbitraryAction);
            // Assert
            var newRoomDescription = game.Describe();
            Assert.That(actionSucceeded, Is.True);
            Assert.That(newRoomDescription, Contains.Substring(startingRoom.Description));
            Assert.That(newRoomDescription, Contains.Substring(">> Hello back!"));
        }

        private Game GetGame(IAdventureRepository adventureRepo, IActionParser actionParser)
        {
            return new Game(adventureRepo, actionParser);
        }
    }
}