using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Api.Data.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Course>? Courses { get; set; }
        public DbSet<Document>? Documents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Course>()
            .HasMany(c => c.Documents)
            .WithOne(d => d.Course)
            .HasForeignKey(d => d.CourseID);
            SeedRoles(builder);
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
