using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaU4y5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("admin/Home")]
        [Route("admin/Home/Index")]
        [Route("admin")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
