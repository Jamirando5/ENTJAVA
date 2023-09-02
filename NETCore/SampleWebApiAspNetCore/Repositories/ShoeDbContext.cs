using Microsoft.EntityFrameworkCore;
using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public class ShoeDbContext : DbContext
    {
        public ShoeDbContext(DbContextOptions<ShoeDbContext> options)
            : base(options)
        {
        }

        public DbSet<ShoeEntity> ShoeItems { get; set; } = null!;
    }
}
