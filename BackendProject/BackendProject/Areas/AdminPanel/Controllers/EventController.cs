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
    }
}
