using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Data.Context
{
    public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : IdentityDbContext<User, IdentityRole<int>, int>(options)
    {
        /// <summary>
        /// Set of all tracked sessions.
        /// </summary>

        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DbSet<Lesson> Lessons { get; set; } = null!;



        /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.OnModelCreating"/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ============================================
            // SESSION ENTITY CONFIGURATION
            // ============================================
        

        }
       
       

        }
    }

