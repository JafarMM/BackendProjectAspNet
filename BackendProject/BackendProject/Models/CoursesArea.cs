using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class CoursesArea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreationTime { get; set; }
        public string UserId { get; set; }

        public ICollection<CourseCategory> CategoryCourses { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModificationTime { get; set; }


        [NotMapped]
        public IFormFile Photo { get; set; }
        public CourseDetail CourseDetail { get; set; }
    }
}
