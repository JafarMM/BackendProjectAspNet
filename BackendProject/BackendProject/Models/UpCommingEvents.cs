using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        public EventDetails EventDetails { get; set; }
        public ICollection<SpeakerEventDetails> SpeakerEventDetails { get; set; }
    }
}
