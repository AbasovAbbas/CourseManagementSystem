using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<GroupSubject> GroupSubjects { get; set; }
        public virtual List<ApplicationUser> Students { get; set; }
        [NotMapped]
        public List<SelectListItem> drpSubjects { get; set; }
        [Display(Name = "Subjects")]
        [NotMapped]
        public int[] SubjectIds { get; set; }
    }
}
