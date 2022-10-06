using Microsoft.EntityFrameworkCore;
using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .Property(u => u.Grade)
                .HasConversion(
                    v => v.ToString(),
                    v => (Grade)Enum.Parse(typeof(Grade), v)
                );
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}   
