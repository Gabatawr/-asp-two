using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUserAjax.Controllers.Admin
{
    //[Authorize]
    public class AdminDashboardController : Controller
    {
        public IActionResult Index() => View("../Admin/Index");
    }
}
