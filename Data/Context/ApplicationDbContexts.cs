using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Data.Context
{
    public partial class ApplicationDbContexts(DbContextOptions<ApplicationDbContexts> options)
       : IdentityDbContext<User, IdentityRole<int>, int>(options)
    {
        /// <summary>
        /// Set of all tracked sessions.
        /// </summary>

        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DbSet<Lesson> Lessons { get; set; } = null!;

        public DbSet<Assigment> Assigments { get; set; } = null!;

        public DbSet<Submission> Submissions { get; set; } = null!;

        public DbSet<CourseUser> CourseUsers { get; set; } = null!;



        /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.OnModelCreating"/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseUser>()
         .HasKey(cu => new { cu.CourseID, cu.UserID });

            builder.Entity<CourseUser>()
                .HasOne(cu => cu.Course)
                .WithMany(c => c.CourseUsers)
                .HasForeignKey(cu => cu.CourseID);

            builder.Entity<CourseUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CourseUsers)
                .HasForeignKey(cu => cu.UserID);

        }
       
       

        }
    }

