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
        public DateTime Date { get; set; }
        public string CourseName { get; set; }
        public DateTime Time { get; set; }
        public string City { get; set; }

    }
}
