using AutoMapper;
using BrainbayConsoleApp.DataTransferring;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.DomainModels.Entities;
using BrainbayConsoleApp.ExternalServices.Abstraction;
using Newtonsoft.Json.Linq;

namespace BrainbayConsoleApp.ExternalServices
{
    public class RickAndMortyApiClient : IRickAndMortyApiClient
    {
        private readonly IMapper _characterMapper;

        public RickAndMortyApiClient(IMapper characterMapper)
        {
            _characterMapper = characterMapper;
        }

        public async Task<List<Character>> GetAsync()
        {
            using (var client = new HttpClient())
            {
                var characters = await client.GetAsync("https://rickandmortyapi.com/api/character/");
                if (!characters.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve characters.");
                }
                else
                {
                    var json = await characters.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(json);
                    var characterArray = JArray.FromObject(jsonObject["results"]);
                    var apiResult = characterArray.ToObject<List<CharacterDto>>();

                    var result = MapApiResult(apiResult);

                    return result;
                }
            }
        }

        private List<Character> MapApiResult(List<CharacterDto> characterDtos)
        {
            List<Character> characters = new();

            for (int i = 0; i < characterDtos.Count; i++)
            {
                characters.Add(_characterMapper.Map<Character>(characterDtos.ElementAt(i)));

                foreach (var episode in characterDtos.ElementAt(i).Episodes)
                {
                    characters.ElementAt(i).Episodes.Add(new Episode { Name = episode });
                }
            }

            return characters;
        }
    }
}
