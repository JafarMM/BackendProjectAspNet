using BackendProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.DataAccesLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Bio> Bio { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<CoursesArea> CoursesArea {get; set;}
        public DbSet<UpCommingEvents> UpCommingEvents { get; set; }
    }
}
