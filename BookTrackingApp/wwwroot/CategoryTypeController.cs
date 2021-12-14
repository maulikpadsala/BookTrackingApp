using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.wwwroot
{
    public class CategoryTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
