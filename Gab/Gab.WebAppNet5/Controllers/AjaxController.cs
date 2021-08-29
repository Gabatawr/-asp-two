using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gab.WebAppNet5.Controllers
{
    public class AjaxController : Controller
    {
        public IActionResult Groups()
        {
            ViewBag.Entity = "Groups";
            return View("Index");
        }

        public IActionResult Teachers()
        {
            ViewBag.Entity = "Teachers";
            return View("Index");
        }
    }
}
