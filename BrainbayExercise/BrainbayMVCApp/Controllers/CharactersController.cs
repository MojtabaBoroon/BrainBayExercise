using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.Applications.Characters.Queries.GetCharactersByPlanetName;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.Persistence.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BrainbayMVCApp.Controllers
{
    public class CharactersController : Controller
    {
        private readonly IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>> _getCharactersByPlanetQueryHandler;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMemoryCache _cache;
        private static List<Character> _characters = new();
        private string _planet = "";

        public CharactersController(
            IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>> getCharactersByPlanetQueryHandler,
            ICharacterRepository characterRepository,
            IMemoryCache cache)
        {
            _getCharactersByPlanetQueryHandler = getCharactersByPlanetQueryHandler;
            _characterRepository = characterRepository;
            _cache = cache;
        }
        public async Task<IActionResult> Index(string planet)
        {
            List<Character> result = new List<Character>();
            _planet = planet;

            bool fromCache = false;

            if (!_cache.TryGetValue("characters", out _characters) || _characters.Count == 1)
            { 
                _characters = await _getCharactersByPlanetQueryHandler.HandleAsync(new GetCharactersByPlanetNameQuery { PlanetName = _planet});
                result = _characters.ToList();
                _cache.Set("characters", _characters, TimeSpan.FromMinutes(5));
            }
            else
            {
                result = _characters.ToList();
                if (!string.IsNullOrEmpty(_planet))
                {
                    result = _characters.Where(x => x.Origin.Name == _planet).ToList();
                }
                fromCache = true;
            }
            HttpContext.Response.Headers.Add("from-database", (!fromCache).ToString());

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Status, Species, Type, Gender, Origin, Location, Image, Episodes, Url, Created")] Character character)
        {
            character.Created = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _characterRepository.InsertAsync(character);

                _cache.Remove("characters");
                return RedirectToAction(nameof(Index), new { planet = character.Origin.Name });
            }
            return View(character);
        }
    }
}
