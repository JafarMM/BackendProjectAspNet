using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class SearchViewModel
    {
        public List<CoursesArea> Courses { get; set; }
        public List<UpCommingEvents> Events { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
