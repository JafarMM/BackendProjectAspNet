using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class CourseCategory
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public CoursesArea Course { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}