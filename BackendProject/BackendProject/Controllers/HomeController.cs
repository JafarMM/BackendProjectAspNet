using BackendProject.DataAccesLayer;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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
            var coursesabout = _dbContext.CoursesArea.Take(3).ToList();
            var upcommingevents = _dbContext.UpCommingEvents.Take(4).ToList();
            var testimonial = _dbContext.Testimonial.FirstOrDefault();
            var blogs = _dbContext.Blogs.Take(3).ToList();
            var banner = _dbContext.Banner.FirstOrDefault();
            var homeViewModel = new HomeViewModel
            {
                Slider = slider,
                about=about,
                coursesAreas=coursesabout,
                UpCommingEvents=upcommingevents,
                Testimonial=testimonial,
                Blogs=blogs,
                Banner=banner,
                 
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

        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Content("Error");
            }

            var courses = _dbContext.CoursesArea.Include(x => x.CourseDetail).OrderByDescending(x => x.Id)
                .Where(x => x.Title.ToLower().Contains(search.ToLower())).Take(4).ToList();

            return PartialView("_SearchGlobalPartial", courses);
        }

        public async Task <IActionResult> Subscribe(string email)
        {
            if (email == null)
            {
                return Content("You must write email address");
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return Content("It is not email address!");
            }
            var isExist = await _dbContext.Subscribes.AnyAsync(x=> x.Email ==email);
            if (isExist)
            {
                return Content("You are already subscribe Edu Home site");
            }
            Subscribe subscribe = new Subscribe { Email = email };
            await _dbContext.Subscribes.AddAsync(subscribe);
            await _dbContext.SaveChangesAsync();
                return Content("Congratulations!");
        }
    }
}
