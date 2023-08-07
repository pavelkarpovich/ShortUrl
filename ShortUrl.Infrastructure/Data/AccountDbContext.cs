using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShortUrl.ApplicationCore.Entities;

namespace ShortUrl.Infrastructure.Data
{
    public class AccountDbContext : IdentityDbContext<Account>
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }
    }
}
