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
        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<CourseDetail> CourseDetail { get; set; }
        public DbSet<BlogDetails> blogDetails { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerEventDetails> SpeakerEventDetails { get; set; }
    }
}
