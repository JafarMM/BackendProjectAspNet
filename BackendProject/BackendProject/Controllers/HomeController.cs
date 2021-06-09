using BackendProject.DataAccesLayer;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var slider = _dbContext.Slider.ToList();
            var about = _dbContext.About.FirstOrDefault();
            var coursesabout = _dbContext.CoursesArea.ToList();
            var upcommingevents = _dbContext.UpCommingEvents.ToList();
            var testimonial = _dbContext.Testimonial.FirstOrDefault();
            var homeViewModel = new HomeViewModel
            {
                Slider = slider,
                about=about,
                CoursesAreas=coursesabout,
                UpCommingEvents=upcommingevents,
                Testimonial=testimonial
            };

            return View(homeViewModel);
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
