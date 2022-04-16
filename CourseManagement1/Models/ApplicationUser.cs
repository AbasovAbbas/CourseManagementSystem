using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int EnrollmentNo { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public virtual List<Payment> Payments{ get; set; }
        public virtual List<TeacherSubject> TeacherSubjects { get; set; }
        
        

    }
}
