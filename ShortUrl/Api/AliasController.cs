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

        public AliasController(IAliasService aliasService)
        {
            _aliasService = aliasService;
        }

        [HttpPost("CheckAlias")]
        public bool CheckAlias([FromForm] Alias alias)
        {
            var isAlisExists = _aliasService.IsAliasExist(alias.AliasValue);
            return isAlisExists;
        }


        [HttpPost("SubmitAlias")]
        public string SubmitAlias([FromForm] AliasUrl aliasUrl)
        {
            var userId = User.Identity.GetUserId();
            _aliasService.AddAlias(aliasUrl.AliasValue, aliasUrl.UrlValue, userId);
            return "https://localhost:7054/" + aliasUrl.AliasValue;
        }

        [HttpGet("GetMyUrls")]
        public async Task<IEnumerable<AliasUrl>> GetMyUrls()
        {
            var userId = User.Identity.GetUserId();
            //var list = new List<AliasUrl>
            //{
            //    new AliasUrl { UrlValue = "url1", AliasValue = "alias1" },
            //    new AliasUrl { UrlValue = "url2", AliasValue = "alias2" }
            //};

            var list = await _aliasService.GetMyUrls(userId);
            return list;
        }
    }
}
