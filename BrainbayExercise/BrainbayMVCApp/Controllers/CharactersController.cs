using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.Applications.Characters.Commands;
using BrainbayConsoleApp.Applications.Characters.Queries.GetCharactersByPlanetName;
using BrainbayConsoleApp.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BrainbayMVCApp.Controllers
{
    public class CharactersController : Controller
    {
        private readonly IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>> _getCharactersByPlanetQueryHandler;
        private readonly ICommandHandler<CharactersCommand, List<Character>> _charactersCommandHandler;
        private readonly ICommandHandler<InsertCharacterCommand, Character> _insertCharacterCommanddHandler;
        private readonly IMemoryCache _cache;
        private static List<Character> _characters = new();
        private string _planet = "";
        string _apiCalledCookieName = "ApiCalled";

        public CharactersController(
            IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>> getCharactersByPlanetQueryHandler,
            IMemoryCache cache,
            ICommandHandler<CharactersCommand, List<Character>> charactersCommandHandler,
            ICommandHandler<InsertCharacterCommand, Character> insertCharacterCommanddHandler)
        {
            _getCharactersByPlanetQueryHandler = getCharactersByPlanetQueryHandler;
            _cache = cache;
            _charactersCommandHandler = charactersCommandHandler;
            _insertCharacterCommanddHandler = insertCharacterCommanddHandler;
        }

        public async Task<IActionResult> Index(string planet)
        {
            List<Character> result = new List<Character>();
            _planet = planet;

            bool fromCache = false;

            await CallExternalApi();

            if (!_cache.TryGetValue("characters", out _characters) || _characters.Count == 1)
            {
                _characters = await _getCharactersByPlanetQueryHandler.HandleAsync(new GetCharactersByPlanetNameQuery { PlanetName = _planet });
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

        private async Task CallExternalApi()
        {
            if (!Request.Cookies.ContainsKey(_apiCalledCookieName))
            {
                await _charactersCommandHandler.HandleAsync(new CharactersCommand());

                var cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddYears(1),
                    HttpOnly = true
                };

                Response.Cookies.Append(_apiCalledCookieName, "true", cookieOptions);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Status, Species, Type, Gender, Origin, Location, Image, Episodes, Url, Created")] Character character)
        {
            character.Created = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _insertCharacterCommanddHandler.HandleAsync(new InsertCharacterCommand { Character = character });

                _cache.Remove("characters");
                return RedirectToAction(nameof(Index), new { planet = character.Origin.Name });
            }
            return View(character);
        }
    }
}
