using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class TeachersViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public TeachersViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teachers = await _dbContext.Teachers.ToListAsync();

            return View(teachers);
        }
    }
}
