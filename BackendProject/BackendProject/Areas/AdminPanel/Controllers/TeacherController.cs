using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
   
    public class TeacherController:Controller
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
            var teacherDetails = _dbContext.TeacherDetails.Include(x => x.Teacher).ThenInclude(x=> x.Position).FirstOrDefault(x => x.TeacherId == id);

            if (teacherDetails == null)
            {
                return NotFound();
            }
            return View(teacherDetails);
        }


    }
}
