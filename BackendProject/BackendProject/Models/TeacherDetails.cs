using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class TeacherDetails
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }
        public long Experience { get; set; }
        public string Faculty { get; set; }
        public int SkillPercent { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

         
    }
}
