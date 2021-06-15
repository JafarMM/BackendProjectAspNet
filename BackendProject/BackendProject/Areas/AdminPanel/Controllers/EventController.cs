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

            var url = Url.Action("EventDetail", "Event", new { Area = "default", events.Id }, protocol: HttpContext.Request.Scheme);
            var message = $"<a href={url}>Click to show for new event</a>";
            var subscribes = await _dbContext.Subscribes.ToListAsync();
            foreach (var sub in subscribes)
            {
                await Helper.SendMessage(sub.Email, message, $" {events.CourseName} New event is created,If you want to show,to click.");
            }

            return RedirectToAction("Index");
        }
        
    }
}
