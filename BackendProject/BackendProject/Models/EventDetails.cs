using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class EventDetails
    {
        public int Id { get; set; }
        public String Description { get; set; }

        [ForeignKey("UpCommingEvents")]
        public int UpCommingEventsId { get; set; }
        public UpCommingEvents UpCommingEvents { get; set; }
      
    }
}
