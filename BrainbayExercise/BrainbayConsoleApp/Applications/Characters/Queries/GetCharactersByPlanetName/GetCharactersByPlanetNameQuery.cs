using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.DomainModels;

namespace BrainbayConsoleApp.Applications.Characters.Queries.GetCharactersByPlanetName
{
    public class GetCharactersByPlanetNameQuery :IQuery<List<Character>>
    {
        public string PlanetName { get; set; }
    }
}
