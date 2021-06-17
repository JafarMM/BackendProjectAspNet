using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class EventCategory
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public UpCommingEvents Event { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}