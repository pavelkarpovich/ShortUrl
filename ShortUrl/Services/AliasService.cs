using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.ApplicationCore.Interfaces;
using ShortUrl.Infrastructure.Data;
using ShortUrl.Web.Interfaces;

namespace ShortUrl.Web.Services
{
    public class AliasService : IAliasService
    {
        private readonly IRepository<AliasUrl> _aliasRepository;
        private readonly IConfiguration _configuration;

        public AliasService(IRepository<AliasUrl> aliasRepository, IConfiguration configuration)
        {
            _aliasRepository = aliasRepository;
            _configuration = configuration;
        }

        public void AddAlias(string alias, string url, string userId)
        {
            
            _aliasRepository.Create(new AliasUrl(alias, url, userId));
        }

        public void DeleteAlias(string alias)
        {
            var item = _aliasRepository.Get(x => x.AliasName == alias).FirstOrDefault();
            if (item is not null)
                _aliasRepository.Delete(item);
        }

        public async Task<IEnumerable<Models.AliasUrlModel>> GetMyUrls(string userId)
        {
            var items = (await _aliasRepository.GetAsync(x => x.UserId == userId)).Select(x => 
                new Models.AliasUrlModel() { AliasValue = "https://localhost:7054/" + x.AliasName, UrlValue = x.Url});
            return items;
        }

        public string? GetUrl(string alias)
        {
            var item = _aliasRepository.Get(x => x.AliasName == alias).FirstOrDefault();
            return item?.Url;
        }

        public bool IsAliasExist(string alias)
        {
            var items = _aliasRepository.Get(x => x.AliasName == alias);
            return !items.IsNullOrEmpty();
        }
    }
}
