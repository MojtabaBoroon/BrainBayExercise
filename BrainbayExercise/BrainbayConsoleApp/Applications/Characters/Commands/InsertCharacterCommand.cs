using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.DomainModels;

namespace BrainbayConsoleApp.Applications.Characters.Commands
{
    public class InsertCharacterCommand : ICommand<Character>
    {
        public Character Character { get; set; }
    }
}
