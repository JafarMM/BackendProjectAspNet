using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public ICollection<SpeakerEventDetails> SpeakerEventDetails { get; set; }
        public bool IsDeleted { get; set; }
        public string Position { get; set; }
    }
}
