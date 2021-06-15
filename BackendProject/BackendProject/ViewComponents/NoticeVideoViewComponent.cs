using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class NoticeVideoViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public NoticeVideoViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var noticevideo = await _dbContext.NoticeVideo.FirstOrDefaultAsync();

            return View(noticevideo);
        }
    }
}
