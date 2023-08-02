using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.ApplicationCore.Interfaces;
using ShortUrl.Infrastructure.Data;
using ShortUrl.Web.Interfaces;

namespace ShortUrl.Web.Services
{
    public class AliasService : IAliasService
    {
        private readonly IRepository<Alias> _aliasRepository;

        public AliasService(IRepository<Alias> aliasRepository)
        {
            _aliasRepository = aliasRepository;
        }

        public void AddAlias(string alias, string url, string userId)
        {
            
            _aliasRepository.Create(new Alias(alias, url, userId));
        }

        public async Task<IEnumerable<Models.AliasUrl>> GetMyUrls(string userId)
        {
            var items = (await _aliasRepository.GetAsync(x => x.UserId == userId)).Select(x => 
                new Models.AliasUrl() { AliasValue = "https://localhost:7054/" + x.AliasName, UrlValue = x.Url});
            return items;
        }

        public string? GetUrl(string alias)
        {
            var item = (_aliasRepository.Get(x => x.AliasName == alias)).FirstOrDefault();
            return item?.Url;
        }

        public bool IsAliasExist(string alias)
        {
            var items = _aliasRepository.Get(x => x.AliasName == alias);
            return !items.IsNullOrEmpty();
        }
    }
}
