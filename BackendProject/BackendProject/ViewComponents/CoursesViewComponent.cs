using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class CoursesViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public CoursesViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _dbContext.CoursesArea.Include(x=> x.CourseDetail).Where(x=> x.IsDeleted==false).OrderByDescending(x=> x.Id).ToListAsync();

            return View(courses);
        }
    }
}
