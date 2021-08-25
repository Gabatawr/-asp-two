using Microsoft.AspNetCore.Mvc;

namespace Gab.WebAppNet5.Controllers
{
    public class SchoolController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult MethodName() => View();
    }
}
