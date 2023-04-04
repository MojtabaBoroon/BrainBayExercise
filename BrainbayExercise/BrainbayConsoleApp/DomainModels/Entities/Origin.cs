using BrainbayConsoleApp.DomainModels.Abstractions;

namespace BrainbayConsoleApp.DomainModels.Entities
{
    public class Origin : Entity
    {
        public string Name { get; set; }
        public string? Url { get; set; }
    }
}