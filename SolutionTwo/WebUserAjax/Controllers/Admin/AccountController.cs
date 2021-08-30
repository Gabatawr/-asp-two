using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUserAjax.Data.Entities;

namespace WebUserAjax.Controllers.Admin
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Index
        public IActionResult Index()
        {
            return View("../Admin/Account/Index", _userManager.Users);
        }
    }
}
