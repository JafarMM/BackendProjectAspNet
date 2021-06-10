using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class TestiMonialViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public TestiMonialViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonial = await _dbContext.Testimonial.FirstOrDefaultAsync();

            return View(testimonial);
        }
    }
}
