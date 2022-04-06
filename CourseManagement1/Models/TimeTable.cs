using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class TimeTable
    {
        public int Id { get; set; }
        public string Time { get; set; }
        [ForeignKey("Teacher")]
        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        [ForeignKey("Student")]
        [Display(Name = "Student")]
        public string StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }
        [ForeignKey("Subject")]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }


    }
}
