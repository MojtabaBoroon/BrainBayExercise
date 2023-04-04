using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.ExternalServices.Abstraction;
using BrainbayConsoleApp.Persistence.Abstractions;

namespace BrainbayConsoleApp.Applications.Characters.Commands
{
    public class CharactersCommandHandler : ICommandHandler<CharactersCommand, List<Character>>
    {
        private readonly IRickAndMortyApiClient _rickAndMortyApiClient;
        private readonly ICharacterRepository _characterRepository;

        List<Character> _characters = new List<Character>();

        public CharactersCommandHandler(IRickAndMortyApiClient charactersQueryExecutor, ICharacterRepository characterRepository)
        {
            _rickAndMortyApiClient = charactersQueryExecutor;
            _characterRepository = characterRepository;
        }

        public async Task<List<Character>> HandleAsync(CharactersCommand command)
        {
            _characters = await _rickAndMortyApiClient.GetAsync();
            var aliveCharacters = FindAliveCharacters(_characters);
            await _characterRepository.InsertAsync(aliveCharacters);

            return aliveCharacters;
        }

        private static List<Character> FindAliveCharacters(List<Character> _characters)
        {
            return _characters.Where(c => c.Status == "Alive")
                                    .Select(c => new Character
                                    {
                                        Name = c.Name,
                                        Status = c.Status,
                                        Created = c.Created,
                                        Image = c.Image,
                                        Species = c.Species,
                                        Type = c.Type,
                                        Url = c.Url,
                                        Gender = c.Gender,
                                        Episodes = c.Episodes,
                                        Location = c.Location,
                                        Origin = c.Origin
                                    }).ToList();
        }
    }
}
