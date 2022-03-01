using Microsoft.EntityFrameworkCore;

using softasinsoftware.Shared.Models;

namespace softasinsoftware.API
{
    public class GearDbContext : DbContext
    {
        public GearDbContext(DbContextOptions options) : base(options) {}

        public DbSet<GearItem> GearList { get; set; }
    }
}
