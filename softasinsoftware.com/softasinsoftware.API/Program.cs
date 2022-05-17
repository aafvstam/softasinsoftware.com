using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using softasinsoftware.API.Data;
using softasinsoftware.API.Extensions.Microsoft.Extensions;
using softasinsoftware.API.Services;
using softasinsoftware.Shared.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(false);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Authentication:JwtIssuer"],
               ValidAudience = builder.Configuration["Authentication:JwtAudience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtSecurityKey"]))
           };
       });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Soft as in Software API",
        Description = "Provides Data for Soft as in Software",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.WithOrigins("http://localhost:5000", "https://localhost:5001", "https://www.softasinsoftware.com", "https://softasinsoftware.com")
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
    app.UseSwaggerUI(c =>
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

//Get all Gear Items in a List
app.MapGet("/gearlist", async (ApplicationDbContext db) => await db.GearList.AsNoTracking().ToListAsync());

// Add a Gear Item to the List
app.MapPost("/gear", async (ApplicationDbContext db, GearItem gear) =>
{
    await db.GearList.AddAsync(gear);
    await db.SaveChangesAsync();
    return Results.Created($"/gear/{gear.Id}", gear);
});

app.MapGet("/gear/{id}", async (ApplicationDbContext db, int id) => await db.GearList.FindAsync(id));



//app.MapPut("/gear/{id}", async (ApplicationDbContext db, GearItem updategear, int id) =>
//{
//    var gear = await db.GearList.FindAsync(id);

//    if (gear is null) return Results.NotFound();

//    gear.Name = updategear.Name;
//    gear.Description = updategear.Description;

//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});

//app.MapDelete("/gear/{id}", async (ApplicationDbContext db, int id) =>
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

app.MapGet("/usercount", async (UserManager<IdentityUser> userMgr) =>
{
    int usercount = -1;
    try
    {
        usercount = userMgr.Users.Count();
    }
    catch (Exception exception)
    {
        return Results.BadRequest(exception.Message);
    }

    return Results.Ok(usercount);
});

app.MapGet("/register-admin", async (UserManager<IdentityUser> userMgr) =>
{
    try
    {
        // register admin
        var newUser = new IdentityUser
        {
            // Get from Azure Vault
            UserName = builder.Configuration["Authentication:InitialUser"],
            Email = builder.Configuration["Authentication:InitialUser"]
        };

        var result = await userMgr.CreateAsync(newUser, builder.Configuration["Authentication:InitialSecret"]);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
        }
    }
    catch (Exception exception)
    {
        return Results.BadRequest(exception.Message);
    }

    return Results.Ok();
});

//app.MapPost("/accounts", async (UserManager<IdentityUser> userMgr, RegisterModel model) =>
//{
//    var newUser = new IdentityUser
//    {
//        UserName = model.Email,
//        Email = model.Email
//    };

//    var result = await userMgr.CreateAsync(newUser, model.Password);

//    if (!result.Succeeded)
//    {
//        var errors = result.Errors.Select(x => x.Description);

//        return Results.Ok(new RegisterResult { Successful = false, Errors = errors });

//    }

//    return Results.Ok(new RegisterResult { Successful = true });
//});

app.MapPost("/login", async (SignInManager<IdentityUser> signInManager, LoginModel login) =>
{
    var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

    if (!result.Succeeded) return Results.BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, login.Email)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtSecurityKey"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var expiry = DateTime.Now.AddDays(Convert.ToInt32(builder.Configuration["Authentication:JwtExpiryInDays"]));

    var token = new JwtSecurityToken(
        builder.Configuration["Authentication:JwtIssuer"],
        builder.Configuration["Authentication:JwtAudience"],
        claims,
        expires: expiry,
        signingCredentials: creds
    );

    return Results.Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
});

app.CreateDbIfNotExists();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
