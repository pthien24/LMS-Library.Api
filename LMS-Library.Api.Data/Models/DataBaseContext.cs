using FLMS_Library.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Api.Data.Models
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Course>? Courses { get; set; }
        public DbSet<Document>? Documents { get; set; }
        public DbSet<Enrollment>? Enrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Thiết lập quan hệ giữa Course và Document
            builder.Entity<Course>()
            .HasMany(c => c.Documents)
            .WithOne(d => d.Course)
            .HasForeignKey(d => d.CourseID);


            // Thiết lập quan hệ giữa Enrollment và Course
            builder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);


            // Thiết lập quan hệ giữa Enrollment và ApplicationUser
            builder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            SeedRoles(builder);
            Seeddata(builder);
        }

        private static void Seeddata(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole()
                    {
                        Name = "Admin",
                        ConcurrencyStamp = "1",
                        NormalizedName = "Admin"
                    }

                 );
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Course>().HasData
                (
                    new Course()
                    {
                        Id = 1,
                        Name = "LTHDT",
                        Description = "LTHDT",
                        Author = "Admin"
                    },
                    new Course()
                    {
                        Id = 2,
                        Name = "LTHDT-2",
                        Description = "LTHDT-2",
                        Author = "Admin"
                    }
                    

                 );
        }
    }
}
