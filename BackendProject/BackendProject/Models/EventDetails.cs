using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class EventDetails
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public ICollection<SpeakerEventDetails> SpeakerEventDetails { get; set; }
    }
}
