using Microsoft.AspNet.Identity;
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
        private IConfiguration _configuration;

        public AliasController(IAliasService aliasService, IConfiguration configuration)
        {
            _aliasService = aliasService;
            _configuration = configuration;
        }

        [HttpPost("CheckAlias")]
        public bool CheckAlias([FromForm] AliasModel alias)
        {
            var isAlisExists = _aliasService.IsAliasExist(alias.AliasValue);
            return isAlisExists;
        }


        [HttpPost("SubmitAlias")]
        public string SubmitAlias([FromForm] AliasUrlModel aliasUrl)
        {
            var userId = User.Identity.GetUserId();
            _aliasService.AddAlias(aliasUrl.AliasValue, aliasUrl.UrlValue, userId);
            var hostUrl = _configuration.GetValue<string>("Url");
            return hostUrl + aliasUrl.AliasValue;
        }

        [HttpGet("GetMyUrls")]
        public async Task<IEnumerable<AliasUrlModel>> GetMyUrls()
        {
            var userId = User.Identity.GetUserId();
            var list = await _aliasService.GetMyUrls(userId);
            return list;
        }

        [HttpDelete("DeleteAlias")]
        public IActionResult DeleteAlias([FromForm] AliasModel alias)
        {
            _aliasService.DeleteAlias(alias.AliasValue);
            return new NoContentResult();
        }
    }
}
