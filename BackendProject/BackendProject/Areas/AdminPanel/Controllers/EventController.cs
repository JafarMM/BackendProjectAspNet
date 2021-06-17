using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.Areas.Utils;
using BackendProject.Data;
using BackendProject.DataAccesLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = Roles.AdminRole)]
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var events = _dbContext.UpCommingEvents.Include(x => x.SpeakerEventDetails).Include(x => x.EventDetails).Where(x=> x.IsDeleted==false).ToList();
            return View(events);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var events = _dbContext.EventDetails.Include(x => x.UpCommingEvents).ThenInclude(x => x.SpeakerEventDetails).ThenInclude(x => x.Speaker).FirstOrDefault(x => x.UpCommingEventsId == id);

            if (events == null)
            {
                return NotFound();
            }
            return View(events);
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

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var events = await _dbContext.UpCommingEvents.Include(x => x.EventDetails)
                .Where(x => x.EventDetails.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (events == null)
                return NotFound();

            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, UpCommingEvents events)
        {
            if (id == null)
                return NotFound();

            if (id != events.Id)
                return BadRequest();

            var Event = await _dbContext.UpCommingEvents.Include(x => x.EventDetails)
                .Where(x => x.EventDetails.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (Event == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var fileName = Event.Image;

            if (events.Photo != null)
            {
                if (!events.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yuklediyiniz shekil deyildir!!!");
                    return View();
                }

                if (!events.Photo.IsSizeAllowed(3000))
                {
                    ModelState.AddModelError("Photo", "Shekliniz 3 mb dan artiq olchudedir!!!");
                    return View();
                }

                var path = Path.Combine(Constants.ImageFolderPath, Event.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, events.Photo);
            }

            Event.Image = fileName;
            Event.CourseName = events.CourseName;
            Event.City = events.City;
            Event.Time = events.Time;
            Event.EventDetails = events.EventDetails;
            Event.LastModificationTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var events = await _dbContext.UpCommingEvents.Include(x => x.EventDetails)
                .Where(x => x.EventDetails.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (events == null)
                return NotFound();

            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEvent(int? id)
        {
            if (id == null)
                return NotFound();

            var events = await _dbContext.UpCommingEvents.Include(x => x.EventDetails)
                .Where(x => x.EventDetails.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (events == null)
                return NotFound();

            events.IsDeleted = true;
            events.EventDetails.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
