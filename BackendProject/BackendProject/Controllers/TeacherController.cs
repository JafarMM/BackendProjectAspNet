using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var teacher = _dbContext.Teachers.Include(x => x.SocialMedias).Include(x=> x.TeacherDetails).Include(x=> x.Position).ToList();
            return View(teacher);
        }
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NotFound();
            }

            var teachers = await _dbContext.Teachers.Where(x => x.IsDeleted == false && x.Name.Contains(search.ToLower())).ToListAsync();

            return PartialView("_SearchTeacherPartial", teachers);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teacherDetail = _dbContext.TeacherDetails.Include(x => x.Teacher).Where(x=> x.IsDeleted == false).FirstOrDefault(x => x.TeacherId == id);

            if (teacherDetail == null)
            {
                return NotFound();
            }
            return View(teacherDetail);
        }
         

    }
}
