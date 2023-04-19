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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IQueryHandler<GetCharactersByPlanetNameQuery, List<Character>>, GetCharactersByPlanetNameQueryHandler>();
builder.Services.AddTransient<ICommandHandler<CharactersCommand, List<Character>>, CharactersCommandHandler>();
builder.Services.AddTransient<ICommandHandler<InsertCharacterCommand, Character>, InsertCharacterCommandHandler>();
builder.Services.AddTransient<BrainBayDbContext, BrainBayDbContext>();
builder.Services.AddTransient<IRickAndMortyApiClient, RickAndMortyApiClient>();
builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();
builder.Services.AddAutoMapper(typeof(CharacterDtoMapper));


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Characters}/{action=Index}/{id?}");

app.Run();
