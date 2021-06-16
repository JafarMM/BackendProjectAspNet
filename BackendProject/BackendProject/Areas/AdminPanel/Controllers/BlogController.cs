using BackendProject.Areas.Utils;
using BackendProject.DataAccesLayer;
using BackendProject.Models;
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
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blog=await _dbContext.Blogs.Include(x=> x.BlogDetails).Where(x=> x.IsDeleted==false).FirstOrDefaultAsync(x=> x.Id==id && x.IsDeleted==false);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Blog blog)
        {
            if (id == null)
                return NotFound();

            if (id != blog.Id)
                return NotFound();
            var blog1 = await _dbContext.Blogs.Include(x => x.BlogDetails).Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (blog1 == null)
            {
                return NotFound();
            }
            var fileName = blog1.Image;

            if(blog.Photo != null)
            {
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yuklediyiniz shekil deyildir!!!");
                    return View();
                }
                if (!blog.Photo.IsSizeAllowed(4000))
                {
                    ModelState.AddModelError("Photo", "Yuklediyiniz sheklin hecmi 4 mb dan az olmalidir!");
                    return View();
                }
            }
            var path = Path.Combine(Constants.ImageFolderPath,"blog",blog1.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, "blog", blog.Photo);

            var isExist = await _dbContext.Blogs.AnyAsync(x => x.Description == blog.Description && x.Id != blog.Id && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda blog vardir");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
 
            blog1.Image = fileName;
            blog1.Description = blog.Description;
            blog1.BlogDetails = blog.BlogDetails;
            blog1.LastModificationTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task <IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var courses = await _dbContext.blogDetails.Include(x => x.Blog).FirstOrDefaultAsync(x => x.BlogId == id);

            if (courses == null)
                return NotFound();

            return View(courses);
        }
    }
}
