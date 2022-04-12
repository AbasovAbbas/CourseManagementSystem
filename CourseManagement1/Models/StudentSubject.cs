using CourseManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }
        


    }
}
