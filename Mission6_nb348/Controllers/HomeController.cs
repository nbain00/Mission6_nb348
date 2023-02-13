using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission6_nb348.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6_nb348.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieDatabaseContext movieContext { get; set; }

        public HomeController(ILogger<HomeController> logger, MovieDatabaseContext context)
        {
            _logger = logger;
            movieContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewMovie(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                movieContext.Add(model);
                movieContext.SaveChanges();
                return View("Confirmation");
            }
            else 
            {
                return View();
            }
            
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
