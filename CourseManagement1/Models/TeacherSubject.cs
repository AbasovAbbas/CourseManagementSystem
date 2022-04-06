using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class TeacherSubject
    {
        public int Id { get; set; }
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        
    }
}
