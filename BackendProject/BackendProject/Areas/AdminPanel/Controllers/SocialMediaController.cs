using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task <IActionResult> Index()
        {
            var socialMedia = await _dbContext.SocialMedias.Include(x=> x.Teacher).ToListAsync();
            return View();
        }
        public async Task<IActionResult> Detail()
        {
            return View();
        }
    }
}
