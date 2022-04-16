using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Models.ViewModels;
using log4net;
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
        static readonly ILog _log = LogManager.GetLogger(typeof(StudentController));
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public StudentController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Index(int groupId)
        {
            ViewData["Sid"] = groupId;
            var result = await _context.GroupSubject.Include(x => x.Group).Include(x => x.Subject).Where(t => t.GroupId == groupId).ToListAsync();
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
            ViewBag.GroupId = new SelectList(_context.Group, "Id", "Name");

            /*List<TeacherViewModel> list = new List<TeacherViewModel>();
            var result = await _userManager.GetUsersInRoleAsync("Teacher");
            foreach (var item in result)
            {
                list.Add(new TeacherViewModel { Id = item.Id, Name = item.Name, Age = item.Age, Address = item.Address, Email = item.Email });
            }
            model.drpTeachers = list.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
*/
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> AddStudent(StudentViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            /*List<GroupSubject> groupStudents = new List<GroupSubject>();
            List<TeacherStudent> studentTeachers = new List<TeacherStudent>();*/
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    GroupId = model.GroupId,
                    EnrollmentNo = model.EnrollmentNo,
                    EmailConfirmed = true
                };
                /*if (model.SubjectIds.Length > 0)
                {
                    foreach (var subjectId in model.SubjectIds)
                    {
                        studentSubjects.Add(new GroupSubject { SubjectId = subjectId, StudentId = user.Id });
                    }
                    user.StudentSubjects = studentSubjects;
                }*/

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Student").Wait();
                    LogicalThreadContext.Properties["user"] = user.UserName;
                    if (User.IsInRole("Admin"))
                    {
                        _log.Info("added by admin");
                    }
                    else
                    {
                        _log.Info($"added by {User.Identity.Name}");
                    }
                    
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
