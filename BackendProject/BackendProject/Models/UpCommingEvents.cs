using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class UpCommingEvents
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string CourseName { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public ICollection<SpeakerEventDetails> SpeakerEventDetails { get; set; }
    }
}
