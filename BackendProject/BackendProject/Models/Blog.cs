using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int CommentCount { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public BlogDetails BlogDetails { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }

    }
}
