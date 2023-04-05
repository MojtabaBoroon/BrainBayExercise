using BrainbayConsoleApp.Applications.Characters.Commands;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.ExternalServices.Abstraction;
using BrainbayConsoleApp.Persistence.Abstractions;
using Moq;

namespace BrainBayUnitTestsProject.Characters.CommandHandler
{
    public class CharactersCommandHandlerTests
    {
        private readonly Mock<IRickAndMortyApiClient> _rickAndMortyApiClientMock;
        Mock<ICharacterRepository> _characterRepositoryMock;

        List<Character> _characters = new();

        public CharactersCommandHandlerTests()
        {
            _rickAndMortyApiClientMock = new Mock<IRickAndMortyApiClient>(); ;
            _characterRepositoryMock = new Mock<ICharacterRepository> ();
            _characters = Createcharacter();
        }

        private List<Character> Createcharacter()
        {
            return new List<Character>
                {
                new Character { Name = "Character 1", Status = "Alive" },
                new Character { Name = "Character 2", Status = "Dead" },
                new Character { Name = "Character 3", Status = "Alive" }
                };
        }

        [Fact]
        public async Task HandleAsync_InputIsValid_ShouldReturnAliveCharacters()
        {
            // Arrange
            var handler = new CharactersCommandHandler(_rickAndMortyApiClientMock.Object, _characterRepositoryMock.Object);

            _rickAndMortyApiClientMock.Setup(x => x.GetAsync()).ReturnsAsync(_characters);
            _characterRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<List<Character>>())).Returns(Task.CompletedTask);

            // Act
            var result = await handler.HandleAsync(new CharactersCommand());

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Character 1" && c.Status == "Alive");
            Assert.Contains(result, c => c.Name == "Character 3" && c.Status == "Alive");
        }

        [Fact]
        public async Task HandleAsync_InputIsValid_RepositoryIsCalled()
        {
            // Arrange
            var handler = new CharactersCommandHandler(_rickAndMortyApiClientMock.Object, _characterRepositoryMock.Object);

            _rickAndMortyApiClientMock.Setup(x => x.GetAsync()).ReturnsAsync(_characters);
            _characterRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<List<Character>>())).Returns(Task.CompletedTask);

            // Act
            var result = await handler.HandleAsync(new CharactersCommand());
            _characterRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<List<Character>>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_InputIsValid_ApiClientIsCalled()
        {
            // Arrange
            var handler = new CharactersCommandHandler(_rickAndMortyApiClientMock.Object, _characterRepositoryMock.Object);

            _rickAndMortyApiClientMock.Setup(x => x.GetAsync()).ReturnsAsync(_characters);
            _characterRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<List<Character>>())).Returns(Task.CompletedTask);

            // Act
            var result = await handler.HandleAsync(new CharactersCommand());
            _rickAndMortyApiClientMock.Verify(x => x.GetAsync(), Times.Once);
        }
    }
}
