using Microsoft.EntityFrameworkCore;
using ShortUrl.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
