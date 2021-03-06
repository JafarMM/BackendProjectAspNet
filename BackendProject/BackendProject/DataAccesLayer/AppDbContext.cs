using BackendProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.DataAccesLayer
{
    public class AppDbContext : IdentityDbContext<User>
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
        public DbSet<TeacherDetails> TeacherDetails { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<CourseDetail> CourseDetail { get; set; }
        public DbSet<BlogDetails> blogDetails { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<SpeakerEventDetails> SpeakerEventDetails { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<NoticeVideo> NoticeVideo { get; set; }
    }
}
