using softasinsoftware.API.Data;

namespace softasinsoftware.API.Extensions.Microsoft.Extensions
{
    public static class Extensions
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<GearDbContext>();

            if (context.Database.EnsureCreated())
            {
                DbInitializer.Initialize(context);
            }
        }
    }
}
