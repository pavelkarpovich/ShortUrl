using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.Controllers;
using ShortUrl.Web.ViewModels;

namespace ShortUrl.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        public AccountController(ILogger<HomeController> logger, UserManager<Account> userManager,
            SignInManager<Account> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Account user = new Account()
                {
                    Name = viewModel.Name,
                    UserName = viewModel.Email,
                    Email = viewModel.Email
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation($"User {viewModel.Email} is signed up");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"Error when attempting to sign up user {viewModel.Email}");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    viewModel.Email,
                    viewModel.Password,
                    viewModel.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {viewModel.Email} is signed in");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError($"Error when attempting to sign in user {viewModel.Email}");
                    ModelState.AddModelError("", "Wrong email and (or) password");
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"User is logged out");
            return RedirectToAction("Index", "Home");
        }

        //public async Task<ContentResult> GetUserName()
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    return Content(user.Name);
        //}
    }
}
