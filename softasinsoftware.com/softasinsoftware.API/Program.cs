using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using System.Net;
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

app.MapGet("/youtubeplaylistvideos", [AllowAnonymous] async (IYouTubeVideosService youtubeservice) =>
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
app.MapGet("/gearlist", [AllowAnonymous] async (ApplicationDbContext db) => await db.GearList.AsNoTracking().ToListAsync());

// Add a Gear Item to the List
app.MapPost("/gear", async (ApplicationDbContext db, GearItem gear) =>
{
    await db.GearList.AddAsync(gear);
    await db.SaveChangesAsync();
    return Results.Created($"/gear/{gear.Id}", gear);
});

// Finding a Gear Item
app.MapGet("/gear/{id}", async (ApplicationDbContext db, int id) => await db.GearList.FindAsync(id));

// Update a Gear Item
app.MapPut("/gear/{id}", async (ApplicationDbContext db, GearItem updategear) =>
{
    var gear = await db.GearList.FindAsync(updategear.Id);

    if (gear is null) return Results.NotFound();

    gear.Name = updategear.Name;
    gear.Description = updategear.Description;
    gear.Image = updategear.Image;
    gear.URL = updategear.URL;
    gear.ShortURL = updategear.ShortURL;
    gear.URLAmazonNL = updategear.URLAmazonNL;
    gear.ShortURLAmazonNL = updategear.ShortURLAmazonNL;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/gear/{id}", async (ApplicationDbContext db, int id) =>
{
    var gear = await db.GearList.FindAsync(id);

    if (gear is null)
    {
        return Results.NotFound();
    }

    db.GearList.Remove(gear);

    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/usercount", [AllowAnonymous] (UserManager<IdentityUser> userMgr) =>
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

app.MapPost("/register-admin", [AllowAnonymous] async (UserManager<IdentityUser> userMgr) =>
{
    try
    {
        //TODO: Get from Azure Vault
        string username = builder.Configuration["Authentication:InitialUser"];
        string password = builder.Configuration["Authentication:InitialSecret"];

        // register admin
        var newUser = new IdentityUser
        {
            UserName = username,
            Email = username
        };

        var result = await userMgr.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
            return Results.Ok(new RegisterResult { Successful = false, Errors = errors });
        }

        return Results.Ok(new RegisterResult { Successful = true });

    }
    catch (Exception exception)
    {
        return Results.BadRequest(exception.Message);
    }
});

app.MapPost("/accounts", async (UserManager<IdentityUser> userMgr, RegisterModel model) =>
{
    var newUser = new IdentityUser
    {
        UserName = model.Email,
        Email = model.Email
    };

    var result = await userMgr.CreateAsync(newUser, model.Password);

    if (!result.Succeeded)
    {
        var errors = result.Errors.Select(x => x.Description);

        return Results.Ok(new RegisterResult { Successful = false, Errors = errors });

    }

    return Results.Ok(new RegisterResult { Successful = true });
});

app.MapPost("/login", [AllowAnonymous] async (SignInManager<IdentityUser> signInManager, LoginModel login) =>
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


app.MapGet("/imagedownload/{filename}", (string filename) =>
{
    string path = Path.Combine(app.Environment.ContentRootPath,
                           app.Environment.EnvironmentName,
                           "unsafe_uploads",
                           filename);

    var mimeType = "image/png";
    return Results.File(path, contentType: mimeType);
});

app.MapPost("/filesave", async (HttpRequest request) =>
{
    try
    {
        int maxAllowedFiles = 3;
        long maxFileSize = 1024 * 1024 * 15;

        if (!request.HasFormContentType)
        {
            return Results.BadRequest();
        }

        var form = await request.ReadFormAsync();

        Uri? resourcePath = new Uri($"{request.Scheme}://{request.Host}/");

        List<UploadResult> uploadResults = new();

        bool uploaded = false;

        int errorCode = 0;
        int filesProcessed = 0;

        string filenameTrustedForStorage = string.Empty;
        string filenameUntrusted = string.Empty;
        string filenameTrustedForDisplay = string.Empty;

        foreach (var file in form.Files)
        {
            UploadResult result = new();

            // Reset Uploadstate
            filenameTrustedForStorage = string.Empty;
            uploaded = false;
            errorCode = 0;

            filenameUntrusted = file.FileName;
            filenameTrustedForDisplay = WebUtility.HtmlEncode(filenameUntrusted);

            if (filesProcessed < maxAllowedFiles)
            {
                long fileLength = file.Length;
                if (fileLength == 0)
                {
                    errorCode = 1;
                    Console.WriteLine($"'{filenameTrustedForDisplay}' length is 0 (Err: {errorCode})");
                }
                else if (fileLength > maxFileSize)
                {
                    errorCode = 2;
                    Console.WriteLine($"'{filenameTrustedForDisplay}' of {fileLength} bytes is larger than the limit of {maxFileSize} bytes (Err: {errorCode})");
                }
                else
                {
                    filenameTrustedForStorage = Path.GetRandomFileName();

                    string path = Path.Combine(app.Environment.ContentRootPath,
                                               app.Environment.EnvironmentName,
                                               "unsafe_uploads",
                                               filenameTrustedForStorage);

                    try
                    {
                        await using (FileStream stream = new FileStream(path, FileMode.Create))
                            await file.CopyToAsync(stream);

                        Console.WriteLine($"'{filenameUntrusted}' saved at {path}");
                        uploaded = true;
                    }
                    catch (Exception ex)
                    {
                        errorCode = 3;
                        Console.WriteLine($"'{filenameTrustedForDisplay}' error on upload (Err: {errorCode}): {ex.Message}");
                    }
                }
            }
            else
            {
                errorCode = 4;
                Console.WriteLine($"'{filenameTrustedForDisplay}' not uploaded because the request exceeded the allowed {maxAllowedFiles} of files (Err: {errorCode})");
            }

            result.FileNameOriginal = filenameUntrusted;
            result.FileNameStored = filenameTrustedForStorage;
            result.Uploaded = uploaded;
            result.ErrorCode = errorCode;

            uploadResults.Add(result);

            filesProcessed++;
        }

        return Results.Ok(uploadResults);
    }
    catch (Exception exception)
    {
        return Results.BadRequest(exception.Message);
    }
});

string path = Path.Combine(app.Environment.ContentRootPath, app.Environment.EnvironmentName, "unsafe_uploads");

//TODO: See if we can use Static Files in Minimal API projects

//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), path)),
//    RequestPath = "/Files"
//});

app.CreateDbIfNotExists();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
