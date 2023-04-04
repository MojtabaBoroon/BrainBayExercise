using BrainbayConsoleApp.DataAccessContext;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BrainbayConsoleApp.Persistence
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly BrainBayDbContext _brainBayDbContext;
        public CharacterRepository(BrainBayDbContext context)
        {
            _brainBayDbContext = context;
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            return await _brainBayDbContext.Characters
                 .Include(c => c.Origin)
                 .Include(c => c.Location)
                 .Include(c => c.Episodes)
                 .ToListAsync();
        }

        public async Task InsertAsync(List<Character> characters)
        {
            using (var context = new BrainBayDbContext())
            {
                ClearDbTables(context);
                await context.Characters.AddRangeAsync(characters);
                await context.SaveChangesAsync();
            }
        }

        public async Task InsertAsync(Character character)
        {
            using (var context = new BrainBayDbContext())
            {
                await context.Characters.AddAsync(character);
                await context.SaveChangesAsync();
            }
        }

        private static void ClearDbTables(BrainBayDbContext context)
        {
            var DbCharacters = context.Characters
                 .Include(c => c.Episodes)
                 .Include(c => c.Location)
                 .Include(c => c.Origin);

            if (DbCharacters != null)
            {
                foreach (var character in DbCharacters)
                {
                    context.Episodes.RemoveRange(character.Episodes);
                    context.Locations.RemoveRange(character.Location);
                    context.Origins.RemoveRange(character.Origin);
                }

                context.Characters.RemoveRange(DbCharacters);

                context.SaveChanges();
            }
        }
    }
}
