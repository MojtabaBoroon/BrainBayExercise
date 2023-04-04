using BrainbayConsoleApp.DomainModels.Abstractions;

namespace BrainbayConsoleApp.DomainModels.Entities
{
    public class Location : Entity
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}