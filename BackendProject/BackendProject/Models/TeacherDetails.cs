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
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public bool IsDeleted { get; set; }
        public int SkillPercent { get; set; }
        public int SkillPercentDesign { get; set; }
        public int SkillPercentTeamLeader { get; set; }
        public int SkillPercentInnovation { get; set; }
        public int SkillPercentDevolopment { get; set; }
        public int SkillPercentCommunication { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

         
    }
}
