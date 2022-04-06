using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public StudentController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Index(string id)
        {
            ViewData["Sid"] = id;
            var result = await _context.StudentSubject.Include(x => x.Student).Include(x => x.Subject).Where(t => t.StudentId == id).ToListAsync();
            return View(result);
        }
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> ListOfStudents()
        {
            List<StudentViewModel> list = new List<StudentViewModel>();
            var result = await _userManager.GetUsersInRoleAsync("Student");
            foreach (var item in result)
            {
                list.Add(new StudentViewModel { Name = item.Name, Age = item.Age, Address = item.Address, Email = item.Email });
            }
            return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> AddStudent()
        {
            var model = new StudentViewModel();
            model.drpSubjects = _context.Subjects.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            
            /*List<TeacherViewModel> list = new List<TeacherViewModel>();
            var result = await _userManager.GetUsersInRoleAsync("Teacher");
            foreach (var item in result)
            {
                list.Add(new TeacherViewModel { Id= item.Id,Name = item.Name, Age = item.Age, Address = item.Address, Email = item.Email });
            }

            model.drpTeachers = list.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            */
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> AddStudent(StudentViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            List<StudentSubject> studentSubjects = new List<StudentSubject>();
            List<StudentSubject> studentTeachers = new List<StudentSubject>();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    EnrollmentNo = model.EnrollmentNo,
                    EmailConfirmed = true
                };
                if (model.SubjectIds.Length > 0)
                {
                    foreach (var subjectId in model.SubjectIds)
                    {
                        studentSubjects.Add(new StudentSubject { SubjectId = subjectId, StudentId = user.Id });
                    }
                    user.StudentSubjects = studentSubjects;
                }

                /*if (model.TeacherIds.Length > 0)
                {
                    foreach (var teacherId in model.TeacherIds)
                    {
                        studentTeachers.Add(new StudentSubject { TeacherId = teacherId, StudentId = user.Id });
                    }
                    user.StudentSubjects = studentTeachers;
                }*/
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Student").Wait();
                    return RedirectToAction("listOfStudents", "student");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
