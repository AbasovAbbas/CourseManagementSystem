﻿using CourseManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CourseManagement.Models.ViewModels;

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
        public DbSet<StudentSubject> StudentSubject { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<StudentSubject>()
               .HasOne(x => x.Subject)
               .WithMany(x => x.StudentSubjects)
               .HasForeignKey(x => x.SubjectId);
            
            builder.Entity<TeacherSubject>()
                .HasOne(x => x.Subject)
                .WithMany(x => x.TeacherSubjects)
                .HasForeignKey(x => x.SubjectId);

            builder.Entity<TeacherStudent>()
                .HasOne(x => x.Teacher)
                .WithMany(x => x.TeacherStudents)
                .HasForeignKey(x => x.TeacherId);

            base.OnModelCreating(builder);
        }
    }
}
