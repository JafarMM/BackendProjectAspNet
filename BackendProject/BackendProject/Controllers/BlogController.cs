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
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var blogs = _dbContext.Blogs.Include(x=> x.BlogDetails).Where(x=> x.IsDeleted==false).ToList();
            return View(blogs);
               
        }
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NotFound();
            }

            var blog = await _dbContext.Blogs.Where(x => x.IsDeleted == false && x.Description.Contains(search.ToLower()))
                .OrderByDescending(x => x.LastModificationTime).ToListAsync();

            return PartialView("_SearchBlogPartial", blog);
        }
        public IActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var blogDetail = _dbContext.blogDetails.Include(x => x.Blog).Where(x=> x.IsDeleted==false).FirstOrDefault(x => x.BlogId == id);
           
            if (blogDetail == null)
            {
                return NotFound();
            }
            return View(blogDetail);
        }
    }
}
