using AttendanceSystemProject.Entities.Enum;
using AttendanceSystemProject.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Entities
{
    public class AttendanceDbContext : DbContext
    {
        private readonly string _ConnectionString;

        public AttendanceDbContext()
        {
            _ConnectionString = "Server = DESKTOP-QVPOJIS\\SQLEXPRESS; Database = AttendanceProjectDB; User Id = AttendanceProjectDB; Password = 1234567890;TrustServerCertificate = true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentAndCourseRelation>()
                .HasKey((x) => new { x.StudentId, x.CourseId });

                 modelBuilder.Entity<User>()
                .HasData(AdminSeed.Users);

            base.OnModelCreating(modelBuilder);

           
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAndCourseRelation> StudentAndCourseRelations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
