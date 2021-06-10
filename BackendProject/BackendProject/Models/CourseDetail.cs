using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string AboutCourse { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assesment { get; set; }
        public int Price { get; set; }

        [ForeignKey("CoursesArea")]
        public int CoursesAreaId { get; set; }
        public CoursesArea CoursesArea { get; set; }
    }
}
