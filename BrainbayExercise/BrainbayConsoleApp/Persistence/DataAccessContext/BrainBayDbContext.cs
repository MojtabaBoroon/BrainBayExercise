using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainbayConsoleApp.DataAccessContext
{
    public class BrainBayDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog =BrainBayDb;Integrated Security=True;Encrypt=False");
        }
    }
}
