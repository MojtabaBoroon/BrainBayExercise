using BrainbayConsoleApp.DomainModels;

namespace BrainbayConsoleApp.Persistence.Abstractions
{
    public interface ICharacterRepository
    {
        Task InsertAsync(List<Character> characters);
        Task InsertAsync(Character character);
        Task<List<Character>> GetCharactersAsync();
        public void ClearAsync();
    }
}
