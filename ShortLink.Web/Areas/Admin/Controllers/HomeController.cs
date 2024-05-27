﻿using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
