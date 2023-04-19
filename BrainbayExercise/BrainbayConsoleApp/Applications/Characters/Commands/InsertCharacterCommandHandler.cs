using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.Persistence.Abstractions;

namespace BrainbayConsoleApp.Applications.Characters.Commands
{
    public class InsertCharacterCommandHandler : ICommandHandler<InsertCharacterCommand, Character>
    {
        private readonly ICharacterRepository _characterRepository;

        public InsertCharacterCommandHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Character> HandleAsync(InsertCharacterCommand command)
        {
            await _characterRepository.InsertAsync(command.Character);
            return command.Character;
        }
    }
}
