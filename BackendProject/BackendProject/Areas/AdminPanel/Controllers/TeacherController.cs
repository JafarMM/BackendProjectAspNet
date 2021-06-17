using BackendProject.Areas.Utils;
using BackendProject.Data;
using BackendProject.DataAccesLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = Roles.AdminRole)]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var teachers = _dbContext.Teachers.Include(x => x.SocialMedias).Include(x => x.TeacherDetails).Include(x => x.Position).ToList();
            return View(teachers);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teacherDetails = _dbContext.TeacherDetails.Include(x => x.Teacher).ThenInclude(x => x.Position).FirstOrDefault(x => x.TeacherId == id);

            if (teacherDetails == null)
            {
                return NotFound();
            }
            return View(teacherDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
         

            if (teacher.Photo == null)
            {
                ModelState.AddModelError("Photo", "Shekil bosh olmamalidir");
                return View();
            }

            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Bu shekil deyildir.");
                return View();
            }

            if (!teacher.Photo.IsSizeAllowed(3000))
            {
                ModelState.AddModelError("Photo", "Sizin shekil olchunuz 3 mb dan choxdur.");
                return View();
            }

            var fileName = await FileUtil.GenerateFileAsync(Areas.Utils.Constants.ImageFolderPath,teacher.Photo);

            teacher.Image = fileName;   

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _dbContext.AddRangeAsync(teacher, teacher.TeacherDetails);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher =  _dbContext.Teachers.Include(x => x.TeacherDetails)
                .Where(x => x.TeacherDetails.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (teacher == null)
                return NotFound();

            return View(teacher);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teacher teacher)
        {
            if (id == null)
                return NotFound();

            if (id != teacher.Id)
                return BadRequest();

            var Teacher = await _dbContext.Teachers.Include(x => x.TeacherDetails)
                .Where(x => x.TeacherDetails.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (Teacher == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var fileName = teacher.Image;

            if (teacher.Photo != null)
            {
                if (!teacher.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Sechdiyiniz shekil deyildir.");
                    return View();
                }

                if (!teacher.Photo.IsSizeAllowed(3000))
                {
                    ModelState.AddModelError("Photo", "Sechdiyiniz shekil 3mb dan artiqdir!");
                    return View();
                }

                var path = Path.Combine(Areas.Utils.Constants.ImageFolderPath, teacher.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                fileName = await FileUtil.GenerateFileAsync(Areas.Utils.Constants.ImageFolderPath, "teacher", teacher.Photo);
            }


            Teacher.Image = fileName;
            Teacher.Name = teacher.Name;
            Teacher.TeacherDetails.Phone = teacher.TeacherDetails.Phone;
            Teacher.TeacherDetails.SkillPercent = teacher.TeacherDetails.SkillPercent;
            Teacher.TeacherDetails.SkillPercentCommunication = teacher.TeacherDetails.SkillPercentCommunication;
            Teacher.TeacherDetails.SkillPercentDesign = teacher.TeacherDetails.SkillPercentDesign;
            Teacher.TeacherDetails.SkillPercentDevolopment = teacher.TeacherDetails.SkillPercentDevolopment;
            Teacher.TeacherDetails.SkillPercentInnovation = teacher.TeacherDetails.SkillPercentInnovation;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _dbContext.Teachers.Include(x => x.TeacherDetails)
                .Where(x => x.TeacherDetails.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (teacher == null)
                return NotFound();

            return View(teacher);
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
