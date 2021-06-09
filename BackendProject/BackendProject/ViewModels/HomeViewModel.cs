using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Slider { get; set; }
        public About about { get; set; }
        public List<CoursesArea> CoursesAreas { get; set; }
        public List<UpCommingEvents> UpCommingEvents { get; set; }
        public Testimonial Testimonial { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
