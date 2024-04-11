using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazoom.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Amazoom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Amazoom.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

       

        [AllowAnonymous]
        // GET: Item
        public async Task<IActionResult> Index( string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            if (_context.Item == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var items = from m in _context.Item
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name!.Contains(searchString));
            }

            return View(await items.Where(s => s.InCart==false).ToListAsync());
        }
        
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        
        public IActionResult Privacy()
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
