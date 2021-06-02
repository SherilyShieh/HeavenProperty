using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HeavenProperty.Models;
using Microsoft.AspNetCore.Authorization;

namespace HeavenProperty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HeavenPropertyContext _context;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this._context = new HeavenPropertyContext();
        }
        [HttpGet]

        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SearchViewModel search)
        {
            if (ModelState.IsValid)
            {

                var result = this._context.Properties.Where(item => string.IsNullOrEmpty(search.Type) ? item.Location.Contains(search.Location) : item.Location.Contains(search.Location) &&
                item.Type.Equals(search.Type)).ToList();
                ViewBag.properties = result;
                await Task.Delay(0);
                return View();
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
