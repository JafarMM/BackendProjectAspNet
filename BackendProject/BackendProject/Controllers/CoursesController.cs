using BackendProject.DataAccesLayer;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CoursesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var courses = _dbContext.CoursesArea.Include(x => x.CourseDetail).ToList();

            return View(courses);
        }
        public IActionResult Details()
        {
            var courseDetail = _dbContext.CourseDetail.Include(x=> x.CoursesArea).FirstOrDefault();
            return View(courseDetail);
        }
    }
}
