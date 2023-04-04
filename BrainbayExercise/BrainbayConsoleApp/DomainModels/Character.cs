using BrainbayConsoleApp.DomainModels.Abstractions;
using BrainbayConsoleApp.DomainModels.Entities;

namespace BrainbayConsoleApp.DomainModels
{
    public class Character : RootEntity
    {
        public string Name { get; set; }
        public string? Status { get; set; }
        public string? Species { get; set; }
        public string? Type { get; set; }
        public string? Gender { get; set; }
        public Origin? Origin { get; set; }
        public Location? Location { get; set; }
        public string? Image { get; set; }
        public List<Episode>? Episodes { get; set; } = new List<Episode>();
        public string? Url { get; set; }
        public DateTime Created { get; set; }
    }
}
