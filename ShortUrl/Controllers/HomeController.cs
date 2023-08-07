using Microsoft.AspNetCore.Mvc;
using ShortUrl.Web.Interfaces;

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

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Application started");
            return View();
        }

        [HttpGet("{alias}")]
        public IActionResult Index(string alias)
        {
            if (alias == "ShortUrl.styles.css")
            {
                return View();
            }
            else
            {
                var url = _aliasService.GetUrl(alias);
                if (url is null)
                {
                    _logger.LogError("Alias {@alias} does not exist", alias);
                    return RedirectToAction("HandleError");
                }
                else
                {
                    _logger.LogInformation($"Redirecting to {url} ");
                    return Redirect(url);
                }
            }
        }

        public IActionResult HandleError()
        {
            return View();
        }
    }
}