using Microsoft.AspNetCore.Mvc;
using ShortUrl.Web.Interfaces;
using ShortUrl.Web.Models;
using System.Diagnostics;

namespace ShortUrl.Controllers
{
    [Route("/")]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAliasService _aliasService;

        public HomeController(ILogger<HomeController> logger, IAliasService aliasService)
        {
            _logger = logger;
            _aliasService = aliasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{alias}")]
        public IActionResult Index(string alias)
        {
            var url = _aliasService.GetUrl(alias);
            if (url is null)
                return View();
            else return Redirect(url);
        }
    }
}