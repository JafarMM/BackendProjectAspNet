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
    [Authorize(Roles = Roles.AdminRole)]
    public class AboutController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AboutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var about = _dbContext.About.SingleOrDefault();
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var about = await _dbContext.About.SingleOrDefaultAsync(x => x.Id == id);
            if (about == null)
                return NotFound();

            return View(about);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, About about)
        {
            if (id == null)
                return NotFound();

            if (id != about.Id)
                return BadRequest();

            var about1 = await _dbContext.About.SingleOrDefaultAsync(x => x.Id == id);
            if (about1 == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var fileName = about1.Image;

            if (about.Photo != null)
            {
                if (!about.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yuklediyiniz shekil deyildir!!!");
                    return View();
                }

                if (!about.Photo.IsSizeAllowed(4000))
                {
                    ModelState.AddModelError("Photo", "Yuklediyiniz sheklin olchusu 4 mb dan az olmalidir!!!.");
                    return View();
                }

                var path = Path.Combine(Constants.ImageFolderPath, "about", about1.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, "about", about.Photo);
            }

            about1.Image = fileName;
            about1.Title = about.Title;
            about1.Description = about.Description;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var about = await _dbContext.About.SingleOrDefaultAsync(x => x.Id == id);
            if (about == null)
                return NotFound();

            return View(about);
        }
    }
}
