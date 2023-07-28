using Microsoft.AspNetCore.Mvc;
using ShortUrl.Web.Interfaces;
using ShortUrl.Web.Models;

namespace ShortUrl.Web.Api
{
    [ApiController]
    [Route("api/alias")]
    public class AliasController : Controller
    {
        private readonly IAliasService _aliasService;

        public AliasController(IAliasService aliasService)
        {
            _aliasService = aliasService;
        }

        [HttpPost("CheckAlias")]
        public async Task<bool> CheckAlias([FromForm] Alias alias)
        {
            var isAlisExists = await _aliasService.IsAliasExist(alias.AliasValue);
            return isAlisExists;
        }

        [HttpPost("SubmitAlias")]
        public async Task<string> SubmitAlias([FromForm] AliasUrl aliasUrl)
        {
            await _aliasService.AddAlias(aliasUrl.AliasValue, aliasUrl.UrlValue);
            return "https://localhost:7054/" + aliasUrl.AliasValue;
        }
    }
}
