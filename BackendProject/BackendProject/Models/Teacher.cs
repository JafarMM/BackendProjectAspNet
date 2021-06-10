using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; set; }
        public bool IsDeleted { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
        public TeacherDetails TeacherDetails { get; set; }

    }
}
