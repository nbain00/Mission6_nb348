using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Categories = movieContext.Categories.ToList();

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
                ViewBag.Categories = movieContext.Categories.ToList();
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult AllMovies()
        {
            var movies = movieContext.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int movieid)
        {
            ViewBag.Categories = movieContext.Categories.ToList();

            var movie = movieContext.Movies.Single(x => x.MovieID == movieid);

            return View("NewMovie", movie);
        }

        [HttpPost]
        public IActionResult Edit(MovieModel model)
        {
            movieContext.Update(model);
            movieContext.SaveChanges();

            return RedirectToAction("AllMovies");
        }

        [HttpGet]
        public IActionResult Delete(int movieid)
        {
            var movie = movieContext.Movies.Single(x => x.MovieID == movieid);

            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(MovieModel model)
        {
            movieContext.Movies.Remove(model);
            movieContext.SaveChanges();

            return RedirectToAction("AllMovies");
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
