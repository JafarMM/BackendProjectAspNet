﻿// <auto-generated />
using System;
using BackendProject.DataAccesLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendProject.Models.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("About");
                });

            modelBuilder.Entity("BackendProject.Models.Bio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacebookUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FooterLogo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FooterNumber")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("PinterestUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TwitterUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vurl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bio");
                });

            modelBuilder.Entity("BackendProject.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BackendProject.Models.BlogDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId")
                        .IsUnique();

                    b.ToTable("BlogDetails");
                });

            modelBuilder.Entity("BackendProject.Models.CourseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Assesment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClassDuration")
                        .HasColumnType("int");

                    b.Property<int>("CoursesAreaId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("HowToApply")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("SkillLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Students")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoursesAreaId")
                        .IsUnique();

                    b.ToTable("CourseDetail");
                });

            modelBuilder.Entity("BackendProject.Models.CoursesArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CoursesArea");
                });

            modelBuilder.Entity("BackendProject.Models.EventDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventDetails");
                });

            modelBuilder.Entity("BackendProject.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PositionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("BackendProject.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Slider");
                });

            modelBuilder.Entity("BackendProject.Models.SocialMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("SocialMedia");
                });

            modelBuilder.Entity("BackendProject.Models.SpeakerEventDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventDetailsId")
                        .HasColumnType("int");

                    b.Property<int>("UpCommingEventsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventDetailsId");

                    b.HasIndex("UpCommingEventsId");

                    b.ToTable("SpeakerEventDetails");
                });

            modelBuilder.Entity("BackendProject.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("BackendProject.Models.TeacherDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Experience")
                        .HasColumnType("bigint");

                    b.Property<string>("Faculty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillPercent")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId")
                        .IsUnique();

                    b.ToTable("TeacherDetails");
                });

            modelBuilder.Entity("BackendProject.Models.Testimonial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Testimonial");
                });

            modelBuilder.Entity("BackendProject.Models.UpCommingEvents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UpCommingEvents");
                });

            modelBuilder.Entity("BackendProject.Models.BlogDetails", b =>
                {
                    b.HasOne("BackendProject.Models.Blog", "Blog")
                        .WithOne("BlogDetails")
                        .HasForeignKey("BackendProject.Models.BlogDetails", "BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("BackendProject.Models.CourseDetail", b =>
                {
                    b.HasOne("BackendProject.Models.CoursesArea", "CoursesArea")
                        .WithOne("CourseDetail")
                        .HasForeignKey("BackendProject.Models.CourseDetail", "CoursesAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoursesArea");
                });

            modelBuilder.Entity("BackendProject.Models.SocialMedia", b =>
                {
                    b.HasOne("BackendProject.Models.Teacher", "Teacher")
                        .WithMany("SocialMedias")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("BackendProject.Models.SpeakerEventDetails", b =>
                {
                    b.HasOne("BackendProject.Models.EventDetails", "EventDetails")
                        .WithMany("SpeakerEventDetails")
                        .HasForeignKey("EventDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProject.Models.UpCommingEvents", "UpCommingEvents")
                        .WithMany("SpeakerEventDetails")
                        .HasForeignKey("UpCommingEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventDetails");

                    b.Navigation("UpCommingEvents");
                });

            modelBuilder.Entity("BackendProject.Models.Teacher", b =>
                {
                    b.HasOne("BackendProject.Models.Position", "Position")
                        .WithMany("Teachers")
                        .HasForeignKey("PositionId");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("BackendProject.Models.TeacherDetails", b =>
                {
                    b.HasOne("BackendProject.Models.Teacher", "Teacher")
                        .WithOne("TeacherDetails")
                        .HasForeignKey("BackendProject.Models.TeacherDetails", "TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("BackendProject.Models.Blog", b =>
                {
                    b.Navigation("BlogDetails");
                });

            modelBuilder.Entity("BackendProject.Models.CoursesArea", b =>
                {
                    b.Navigation("CourseDetail");
                });

            modelBuilder.Entity("BackendProject.Models.EventDetails", b =>
                {
                    b.Navigation("SpeakerEventDetails");
                });

            modelBuilder.Entity("BackendProject.Models.Position", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("BackendProject.Models.Teacher", b =>
                {
                    b.Navigation("SocialMedias");

                    b.Navigation("TeacherDetails");
                });

            modelBuilder.Entity("BackendProject.Models.UpCommingEvents", b =>
                {
                    b.Navigation("SpeakerEventDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
