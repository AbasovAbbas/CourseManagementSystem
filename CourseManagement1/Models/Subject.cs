using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public virtual List<GroupSubject> GroupSubjects { get; set; }
        //public virtual List<StudentSubject> TeacherSubjects1 { get; set; }
        public virtual List<TeacherSubject> TeacherSubjects { get; set; }
    }

}
