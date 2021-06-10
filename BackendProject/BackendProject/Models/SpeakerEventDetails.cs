using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class SpeakerEventDetails
    {
        public int Id { get; set; }

       
        public int EventDetailsId { get; set; }
        public EventDetails EventDetails { get; set; }

       
        public int UpCommingEventsId { get; set; }
        public UpCommingEvents UpCommingEvents { get; set; }
    }
}
