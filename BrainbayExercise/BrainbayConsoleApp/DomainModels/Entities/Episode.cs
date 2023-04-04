using BrainbayConsoleApp.DomainModels.Abstractions;

namespace BrainbayConsoleApp.DomainModels.Entities
{
    public class Episode : Entity
    {
        public string Name { get; set; }
        List<Character> Characters { get; set; } = new List<Character>();
    }
}