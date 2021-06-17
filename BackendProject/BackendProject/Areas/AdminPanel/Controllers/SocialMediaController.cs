using BackendProject.Data;
using BackendProject.DataAccesLayer;
using BackendProject.Models;
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
    //[Authorize(Roles = Roles.AdminRole)]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task <IActionResult> Index()
        {
            var teachers = await _dbContext.Teachers.Include(x=> x.SocialMedias).Where(x=> x.IsDeleted==false).ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var socialMedia = await _dbContext.SocialMedias.Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (socialMedia == null)
                return NotFound();

            return View(socialMedia);
        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _dbContext.Teachers.Where(x => x.IsDeleted == false).ToListAsync();
            ViewBag.Teachers = teachers;
            Dictionary<string, string> sIcon = new Dictionary<string, string>();
            sIcon.Add("Facebook", "zmdi zmdi-facebook");
            sIcon.Add("Pinterest", "zmdi zmdi-pinterest");
            sIcon.Add("Vimeo", "zmdi zmdi-vimeo");
            sIcon.Add("Twitter", "zmdi zmdi-twitter");
            ViewBag.SocialICon = sIcon;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia socialMedia, int teacherId)
        {
            var teachers = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.SocialMedias).ToListAsync();
            ViewBag.Teachers = teachers;
            Dictionary<string, string> sIcon = new Dictionary<string, string>();
            sIcon.Add("Facebook", "zmdi zmdi-facebook");
            sIcon.Add("Pinterest", "zmdi zmdi-pinterest");
            sIcon.Add("Vimeo", "zmdi zmdi-vimeo");
            sIcon.Add("Twitter", "zmdi zmdi-twitter");
            ViewBag.SocialICon = sIcon;

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var teacher in teachers)
            {
                if (teacher.Id == teacherId)
                {
                    foreach (var item in teacher.SocialMedias)
                    {
                        if (item.IsDeleted == false && item.Link == socialMedia.Link && item.Icon == socialMedia.Icon)
                        {
                            ModelState.AddModelError("", "is exists");
                            return View();
                        }
                    }
                }
            }

            socialMedia.TeacherId = teacherId;
            socialMedia.IsDeleted = false;

            await _dbContext.SocialMedias.AddAsync(socialMedia);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
