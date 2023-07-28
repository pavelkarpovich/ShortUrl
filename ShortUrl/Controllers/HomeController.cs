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

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string alias)
        {
            var url =  await _aliasService.GetUrl(alias);
            return Redirect(url);
        }
    }
}