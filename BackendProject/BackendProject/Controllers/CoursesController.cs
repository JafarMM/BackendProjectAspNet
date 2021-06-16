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
            var courses = _dbContext.CoursesArea.Include(x => x.CourseDetail).Where(x => x.IsDeleted == false && x.CourseDetail.IsDeleted == false).ToList();

            return View(courses);
        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseDetail = _dbContext.CourseDetail.Include(x=> x.CoursesArea).FirstOrDefault(x=> x.CoursesAreaId==id);
            if (courseDetail == null)
            {
                return NotFound();
            }
            return View(courseDetail);
        }

        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NotFound();
            }

            var courses = await _dbContext.CoursesArea.Where(x => x.IsDeleted == false && x.Title.Contains(search.ToLower()))
                .OrderByDescending(x => x.LastModificationTime).ToListAsync();

            return PartialView("_CourseSearchPartial", courses);
        }
    }
}
