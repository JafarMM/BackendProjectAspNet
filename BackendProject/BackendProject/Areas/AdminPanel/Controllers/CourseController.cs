using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BackendProject.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using BackendProject.Areas.Utils;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var courses = _dbContext.CoursesArea.Include(x=> x.CourseDetail).ToList();
            return View(courses);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var courses = _dbContext.CourseDetail.Include(x=> x.CoursesArea).FirstOrDefault(x => x.CoursesAreaId == id);

            if (courses == null)
                return NotFound();

            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>Create(CoursesArea coursesArea)
        {
            if (coursesArea.Photo == null)
            {
                ModelState.AddModelError("Photo", "Shekil bosh olmamalidir");
                return View();
            }

            if (!coursesArea.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Bu shekil deyildir.");
                return View();
            }

            if (!coursesArea.Photo.IsSizeAllowed(3000))
            {
                ModelState.AddModelError("Photo", "Sizin shekil olchunuz 3 mb dan choxdur.");
                return View();
            }

            var fileName = await FileUtil.GenerateFileAsync(Areas.Utils.Constants.ImageFolderPath, "course", coursesArea.Photo);

            coursesArea.Image = fileName;

            var isExist = await _dbContext.CoursesArea.AnyAsync(x => x.Title == coursesArea.Title && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kurs vardir");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            coursesArea.CreationTime = DateTime.Now;
            coursesArea.LastModificationTime = DateTime.Now;

            await _dbContext.AddRangeAsync(coursesArea, coursesArea.CourseDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
