using BrainbayConsoleApp.DomainModels.Entities;
using Newtonsoft.Json;

namespace BrainbayConsoleApp.DataTransferring
{
    public class CharacterDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("origin")]
        public Origin Origin { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("episode")]
        public List<string> Episodes { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}
