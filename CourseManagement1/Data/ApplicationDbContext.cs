using CourseManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeacherSubject> TeacherSubject { get; set; }
        public DbSet<GroupSubject> GroupSubject { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GroupSubject>()
                .HasOne(x => x.Subject)
                .WithMany(x => x.GroupSubjects)
                .HasForeignKey(x => x.SubjectId);
            
            builder.Entity<TeacherSubject>()
                .HasOne(x => x.Subject)
                .WithMany(x => x.TeacherSubjects)
                .HasForeignKey(x => x.SubjectId);

            base.OnModelCreating(builder);
        }
        public DbSet<CourseManagement.Models.Payment> Payment { get; set; }
        public DbSet<CourseManagement.Models.Group> Group { get; set; }
        public DbSet<CourseManagement.Models.Attandance> Attandance { get; set; }
    }
}
