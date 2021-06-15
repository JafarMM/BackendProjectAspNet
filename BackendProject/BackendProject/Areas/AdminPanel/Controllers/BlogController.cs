using BackendProject.Areas.Utils;
using BackendProject.DataAccesLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var blogs = _dbContext.Blogs.Include(x => x.BlogDetails).Where(x => x.IsDeleted == false).OrderByDescending(x=> x.LastModificationTime).ToList();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (blog.Photo == null)
            {
                ModelState.AddModelError("Photo", "Shekil bosh olmamalidir!");
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Yuklediyiniz shekil deyildir!!!");
                return View();
            }
            if (!blog.Photo.IsSizeAllowed(4000))
            {
                ModelState.AddModelError("Photo", "Yuklediyiniz sheklin olchusu 4 mb dan az olmalidir.");
                return View();
            }
            var fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, blog.Photo);

            blog.Image = fileName;

            var isExist = await _dbContext.Blogs.AnyAsync(x => x.Description == blog.Description && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda blog movcuddur");
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            blog.Date = DateTime.Now;
            blog.LastModificationTime = DateTime.Now;

            await _dbContext.AddRangeAsync(blog, blog.BlogDetails);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
