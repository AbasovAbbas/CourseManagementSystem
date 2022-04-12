using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models.ViewModels
{
    public class StudentViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int EnrollmentNo { get; set; }
        [NotMapped]
        public List<SelectListItem> drpSubjects { get; set; }
        [Display(Name = "Subjects")]
        [NotMapped]
        public int[] SubjectIds { get; set; }
        [NotMapped]
        public List<SelectListItem> drpTeachers { get; set; }
        [Display(Name = "Teachers")]
        [NotMapped]
        public string[] TeacherIds { get; set; }
        [NotMapped]
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
        public string ConfirmPassword { get; set; }
    }
}
