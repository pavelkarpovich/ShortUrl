using Microsoft.AspNetCore.Mvc;
using ShortUrl.WebApi.Models;

namespace ShortUrl.WebApi.Controllers
{
    [ApiController]
    [Route("alias")]
    public class AliasController : ControllerBase
    {
        [HttpPost("CheckAlias")]
        public string CheckAlias([FromForm] Alias alias)
        {
            return "good";
        }
    }
}
