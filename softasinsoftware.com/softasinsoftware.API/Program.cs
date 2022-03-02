using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;

using softasinsoftware.API;
using softasinsoftware.API.Data;
using softasinsoftware.API.Services;
using softasinsoftware.Shared.Models;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("GearList") ?? "Data Source=gearlist.db";

// builder.Services.AddSqlite<GearDbContext>(connectionString);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Soft as in Software API",
        Description = "Provides Data for Soft as in Software",
        Version = "v1"});
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.WithOrigins("http://localhost:5000", "https://localhost:5001", "https://www.softasinsoftware.com")
           .AllowAnyMethod()
           .AllowAnyHeader());
});

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IYouTubeVideosService, YouTubeVideosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Soft as in Software API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/youtubeplaylistvideos", async (IYouTubeVideosService youtubeservice) =>
{
    try
    {
        bool disableCache = false;
        string playListID = "YouTube:PlayListID";

        YouTubeVideoList videolist = await youtubeservice.GetYouTubePlayListVideosAsync(playlistID: playListID, disableCache: disableCache);
        return Results.Ok(videolist);
    }
    catch (global::System.Exception)
    {
        return Results.BadRequest();
    }
}).Produces<YouTubeVideoList>();

// Get all Gear Items in a List
// app.MapGet("/gearlist", async (GearDbContext db) => await db.GearList.AsNoTracking().ToListAsync());

// Add a Gear Item to the List
//app.MapPost("/gear", async (GearDb db, Gear gear) =>
//{
//    await db.GearList.AddAsync(gear);
//    await db.SaveChangesAsync();
//    return Results.Created($"/gear/{gear.Id}", gear);
//});


//app.MapGet("/gear/{id}", async (GearDb db, int id) => await db.GearList.FindAsync(id));

//app.MapPut("/gear/{id}", async (GearDb db, Gear updategear, int id) =>
//{
//    var gear = await db.GearList.FindAsync(id);

//    if (gear is null) return Results.NotFound();

//    gear.Name = updategear.Name;
//    gear.Description = updategear.Description;

//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});

//app.MapDelete("/gear/{id}", async (GearDb db, int id) =>
//{
//    var gear = await db.GearList.FindAsync(id);

//    if (gear is null)
//    {
//        return Results.NotFound();
//    }

//    db.GearList.Remove(gear);

//    await db.SaveChangesAsync();
//    return Results.Ok();
//});

// app.CreateDbIfNotExists();
app.Run();
