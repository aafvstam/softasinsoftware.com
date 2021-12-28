using Microsoft.Extensions.Caching.Memory;

using softasinsoftware.API.Services;
using softasinsoftware.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IYouTubeVideosService, YouTubeVideosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin());


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

app.Run();

