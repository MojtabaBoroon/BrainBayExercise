// See https://aka.ms/new-console-template for more information
using BrainbayConsoleApp.Applications.Abstraction;
using BrainbayConsoleApp.Applications.Characters.Commands;
using BrainbayConsoleApp.Applications.Characters.Queries.GetCharactersByPlanetName;
using BrainbayConsoleApp.DataAccessContext;
using BrainbayConsoleApp.DataTransferring.Mapper;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.ExternalServices;
using BrainbayConsoleApp.ExternalServices.Abstraction;
using BrainbayConsoleApp.Persistence;
using BrainbayConsoleApp.Persistence.Abstractions;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider serviceProvider = ServiceRegistrator();
var charactersCommandHandler = serviceProvider.GetService<ICommandHandler<CharactersCommand, List<Character>>>();
await charactersCommandHandler.HandleAsync(new CharactersCommand());

ServiceProvider ServiceRegistrator()
{
    return new ServiceCollection()
        .AddTransient<ICharacterRepository, CharacterRepository>()
        .AddTransient<IRickAndMortyApiClient, RickAndMortyApiClient>()
        .AddTransient<IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>>, GetCharactersByPlanetNameQueryHandler>()
        .AddTransient<ICommandHandler<CharactersCommand, List<Character>>, CharactersCommandHandler>()
        .AddTransient<BrainBayDbContext, BrainBayDbContext>()
        .AddAutoMapper(typeof(CharacterDtoMapper))
        .BuildServiceProvider();
}
