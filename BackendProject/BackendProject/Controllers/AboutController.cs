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
    public class AboutController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AboutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var teacher = _dbContext.Teachers.Include(x => x.SocialMedias).Include(x => x.TeacherDetails).Include(x=> x.Position).ToList();
            return View(teacher);
            
        }

        
    }
}
