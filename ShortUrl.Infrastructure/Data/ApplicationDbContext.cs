using Microsoft.EntityFrameworkCore;
using ShortUrl.ApplicationCore.Entities;

namespace ShortUrl.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AliasUrl> Aliases { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
