using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models.ViewModels
{
    public class StudentAttandance
    {
        public List<ApplicationUser> Students { get; set; }
        public Attandance Attandance { get; set; }
    }
}
