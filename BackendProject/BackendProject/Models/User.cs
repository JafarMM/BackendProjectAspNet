using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class User:IdentityUser
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Image { get; set; }

        public List<CoursesArea> Courses { get; set; }
        public string Role { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
