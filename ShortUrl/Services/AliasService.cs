using Microsoft.IdentityModel.Tokens;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.ApplicationCore.Interfaces;
using ShortUrl.Controllers;
using ShortUrl.Web.Interfaces;

namespace ShortUrl.Web.Services
{
    public class AliasService : IAliasService
    {
        private readonly ILogger<AliasService> _logger;
        private readonly IRepository<AliasUrl> _aliasRepository;
        private readonly IConfiguration _configuration;

        public AliasService(ILogger<AliasService> logger, IRepository<AliasUrl> aliasRepository, IConfiguration configuration)
        {
            _aliasRepository = aliasRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public void AddAlias(string alias, string url, string userId)
        {
            _aliasRepository.Create(new AliasUrl(alias, url, userId));
            _logger.LogInformation($"New shortened link to URL {url} with alias {alias} is created");
        }

        public void DeleteAlias(string alias)
        {
            var item = _aliasRepository.Get(x => x.Alias == alias).FirstOrDefault();
            if (item is null)
            {
                _logger.LogError($"Alise {alias} is not found in DB");
            }
            else
            {
                _aliasRepository.Delete(item);
                _logger.LogInformation($"Alise {alias} is deleted");
            }
        }

        public async Task<IEnumerable<Models.AliasUrlModel>> GetMyUrls(string userId)
        {
            var items = (await _aliasRepository.GetAsync(x => x.UserId == userId)).Select(x => 
                new Models.AliasUrlModel() { AliasValue = _configuration.GetValue<string>("Url") + x.Alias, UrlValue = x.Url});
            return items;
        }

        public string? GetUrl(string alias)
        {
            var item = _aliasRepository.Get(x => x.Alias == alias).FirstOrDefault();
            return item?.Url;
        }

        public bool IsAliasExist(string alias)
        {
            var items = _aliasRepository.Get(x => x.Alias == alias);
            return !items.IsNullOrEmpty();
        }
    }
}
