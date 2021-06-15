using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.Areas.Utils;
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
  
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var events = _dbContext.UpCommingEvents.Include(x => x.SpeakerEventDetails).Include(x => x.EventDetails).ToList();
            return View(events);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventDetail = _dbContext.EventDetails.Include(x => x.UpCommingEvents).ThenInclude(x => x.SpeakerEventDetails).ThenInclude(x => x.Speaker).FirstOrDefault(x => x.UpCommingEventsId == id);

            if (eventDetail == null)
            {
                return NotFound();
            }
            return View(eventDetail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UpCommingEvents events)
        {
            if (events.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo bosh olmamalidir!!!");
                return View();
            }

            if (!events.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Bu shekil deyildir!!!");
                return View();
            }

            if (!events.Photo.IsSizeAllowed(3000))
            {
                ModelState.AddModelError("Photo", "Shekliniz 3 mb dan artiq olmamalidir!!!");
                return View();
            }

            var fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, events.Photo);
            events.Image = fileName;

            if (!ModelState.IsValid)
            {
                return View();
            }

            events.CreateTime = DateTime.Now;
            events.LastModificationTime = DateTime.Now;

            await _dbContext.AddRangeAsync(events, events.EventDetails);
            await _dbContext.SaveChangesAsync();

            List<Subscribe> subscribes = _dbContext.Subscribes.ToList();
            string subject = "Creat event";
            string url = "https://localhost:44315/Event/Details/" + events.Id;
            string message = $"<a href={url}>New event is created,if you want to show,please to click</a>";
            foreach (Subscribe sub in subscribes)
            {
                await Helper.SendMessage(subject, message, sub.Email);
            }

            return RedirectToAction("Index");
        }
        
    }
}
