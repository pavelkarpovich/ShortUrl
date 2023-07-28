using Microsoft.EntityFrameworkCore;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.Infrastructure.Data;
using ShortUrl.Web.Interfaces;

namespace ShortUrl.Web.Services
{
    public class AliasService : IAliasService
    {
        private readonly ApplicationDbContext _dbContext;

        public AliasService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAlias(string alias, string url)
        {
            await _dbContext.Aliases.AddAsync(new Alias(alias, url));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsAliasExist(string alias)
        {
            return await _dbContext.Aliases
                .Where(x => x.AliasName == alias)
                .AnyAsync();
        }

        public async Task<string> GetUrl(string alias)
        {
            return await _dbContext.Aliases
                .Where(x => x.AliasName == alias).Select(x => x.Url).FirstOrDefaultAsync();
        }
    }
}
