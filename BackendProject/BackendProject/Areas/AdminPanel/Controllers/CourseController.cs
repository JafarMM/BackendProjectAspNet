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
using System.IO;

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

            var courses = _dbContext.CoursesArea.Include(x=> x.CourseDetail).Where(x=> x.IsDeleted==false).ToList();
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

        public async Task <IActionResult> Create()
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

            var fileName = await FileUtil.GenerateFileAsync(Areas.Utils.Constants.ImageFolderPath,coursesArea.Photo);

            coursesArea.Image = fileName;

            var isExist = await _dbContext.CoursesArea.AnyAsync(x => x.Title == coursesArea.Title && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kurs var!!!");
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
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _dbContext.CoursesArea.Include(x => x.CourseDetail)
                .Where(x => x.CourseDetail.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (course == null)
                return NotFound();

            return View(course);
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CoursesArea coursesArea)
        {
            if (id == null)
                return NotFound();

            if (id != coursesArea.Id)
                return BadRequest();

            var Course = await _dbContext.CoursesArea.Include(x => x.CourseDetail)
                .Where(x => x.CourseDetail.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (Course == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var fileName = Course.Image;

            if (coursesArea.Photo != null)
            {
                if (!coursesArea.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Sechdiyiniz shekil deyildir.");
                    return View();
                }

                if (!coursesArea.Photo.IsSizeAllowed(3000))
                {
                    ModelState.AddModelError("Photo", "Sechdiyiniz shekil 3mb dan artiqdir!");
                    return View();
                }

                var path = Path.Combine(Areas.Utils.Constants.ImageFolderPath, coursesArea.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                fileName = await FileUtil.GenerateFileAsync(Areas.Utils.Constants.ImageFolderPath, "coursesArea", coursesArea.Photo);
            }

            var isExist = await _dbContext.CoursesArea.AnyAsync(x => x.Title == coursesArea.Title && x.Id != coursesArea.Id && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kurs movcuddur!");
                return View();
            }

            Course.Image = fileName;
            Course.Title = coursesArea.Title;
            Course.Description = coursesArea.Description;
            Course.CourseDetail = coursesArea.CourseDetail;
            Course.LastModificationTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _dbContext.CoursesArea.Include(x => x.CourseDetail)
                .Where(x => x.CourseDetail.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _dbContext.CoursesArea.Include(x => x.CourseDetail)
                .Where(x => x.CourseDetail.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (course == null)
                return NotFound();

            course.IsDeleted = true;
            course.CourseDetail.IsDeleted = true;
            

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
