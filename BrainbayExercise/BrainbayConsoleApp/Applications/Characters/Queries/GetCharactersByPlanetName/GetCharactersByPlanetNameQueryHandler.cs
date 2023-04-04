using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.Persistence.Abstractions;

namespace BrainbayConsoleApp.Applications.Characters.Queries.GetCharactersByPlanetName
{
    public class GetCharactersByPlanetNameQueryHandler : IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>>
    {
        private readonly ICharacterRepository _characterRepository;

        public GetCharactersByPlanetNameQueryHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<List<Character>> HandleAsync(GetCharactersByPlanetNameQuery query)
        {
            List<Character> result = new();
            var characters = await _characterRepository.GetCharactersAsync();
            result = characters;
            if (!string.IsNullOrEmpty(query.PlanetName))
            {
                result = characters.Where(x => x.Origin.Name == query.PlanetName).ToList();
            }
            return result;
        }
    }
}
