using BrainbayConsoleApp.Applications.Characters.Queries.GetCharactersByPlanetName;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.DomainModels.Entities;
using BrainbayConsoleApp.Persistence.Abstractions;
using Moq;

namespace BrainBayUnitTestsProject.Characters.QueryHandler
{
    public class GetCharactersByPlanetNameQueryHandlerTests
    {
        Mock<ICharacterRepository> _characterRepositoryMock;
        List<Character> _characters;

        public GetCharactersByPlanetNameQueryHandlerTests()
        {
            _characterRepositoryMock = new Mock<ICharacterRepository>();
            _characters = GetTestCharacters();
        }

        private List<Character> GetTestCharacters()
        {
            return new List<Character>
            {
                new Character { Id = Guid.NewGuid(), Name = "Rick", Origin = new Origin { Name = "Earth" } },
                new Character { Id = Guid.NewGuid(), Name = "Morty", Origin = new Origin { Name = "Earth" } },
                new Character { Id = Guid.NewGuid(), Name = "Summer", Origin = new Origin { Name = "Pluto" } }
            };
        }
        [Theory]
        [InlineData("Earth", 2)]
        [InlineData("Pluto", 1)]
        [InlineData("unkown", 0)]
        public async Task HandleAsync_PlanetIsValid_ReturnsCharactersWithMatchingPlanet(string planet, int  count)
        {
            // Arrange
            _characterRepositoryMock.Setup(x => x.GetCharactersAsync())
                                .ReturnsAsync(_characters);
            var handler = new GetCharactersByPlanetNameQueryHandler(_characterRepositoryMock.Object);

            // Act
            var result = await handler.HandleAsync(new GetCharactersByPlanetNameQuery { PlanetName = planet });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == count);
            Assert.True(result.All(x => x.Origin.Name == planet));
        }

        [Theory]
        [InlineData("", 3)]
        public async Task HandleAsync_PlanetIsNull_ReturnsAllCharacters(string planet, int count)
        {
            // Arrange
            _characterRepositoryMock.Setup(x => x.GetCharactersAsync())
                                .ReturnsAsync(_characters);
            var handler = new GetCharactersByPlanetNameQueryHandler(_characterRepositoryMock.Object);

            // Act
            var result = await handler.HandleAsync(new GetCharactersByPlanetNameQuery { PlanetName = planet });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == count);
        }
    }
}
