using BackendProject.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var events = _dbContext.UpCommingEvents.Include(x=> x.SpeakerEventDetails).Include(x=>x.EventDetails).ToList();
            return View(events);
        }

        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventDetail = _dbContext.EventDetails.Include(x=> x.UpCommingEvents).FirstOrDefault(x => x.UpCommingEventsId == id);
        
            if (eventDetail == null)
            {
                return NotFound();
            }
            return View(eventDetail);
        }
        public IActionResult SpeakerEventDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var speakerEventDetails = _dbContext.SpeakerEventDetails.Include(x => x.Speaker).FirstOrDefault(x => x.SpeakerId == id);

            if (speakerEventDetails == null)
            {
                return NotFound();
            }
            return View(speakerEventDetails);
        }
    }
}
