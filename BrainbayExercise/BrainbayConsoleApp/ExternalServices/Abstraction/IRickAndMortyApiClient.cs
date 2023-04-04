using BrainbayConsoleApp.DomainModels;

namespace BrainbayConsoleApp.ExternalServices.Abstraction
{
    public interface IRickAndMortyApiClient
    {
        Task<List<Character>> GetAsync();
    }
}
