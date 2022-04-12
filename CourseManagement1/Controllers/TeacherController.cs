using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Models.ViewModels;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class TeacherController : Controller
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(TeacherController));
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public TeacherController(UserManager<ApplicationUser> userManager, ApplicationDbContext context = null)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Teacher")]
        //bu action teacher panele yonlenir.
        public async Task<ActionResult> Index(string id)
        {
            ViewData["ID"] = id;
            var result =await _context.TeacherSubject.Include(x => x.Teacher).Include(x => x.Subject).Where(t => t.TeacherId == id).ToListAsync();
            return View(result);
        }
        public async Task<IActionResult> ListOfTeachers()
        {
            List<TeacherViewModel> list = new List<TeacherViewModel>();
            var result =await _userManager.GetUsersInRoleAsync("Teacher");
            foreach (var item in result)
            {
                list.Add(new TeacherViewModel {Name = item.Name, Age = item.Age, Address = item.Address, Email = item.Email});
            }
            return View(list);
        }


        [HttpGet]
        public IActionResult AddTeacher()
        {
            var model = new TeacherViewModel();
            model.drpSubjects = _context.Subjects.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    EmailConfirmed = true
                };
                if (model.SubjectIds.Length > 0)
                {
                    foreach (var subjectId in model.SubjectIds)
                    {
                        teacherSubjects.Add(new TeacherSubject { SubjectId = subjectId, TeacherId = user.Id });
                    }
                    user.TeacherSubjects = teacherSubjects;
                }
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");
                    LogicalThreadContext.Properties["user"] = user.Name;
                    _log.Info("teacher added by admin");
                    return RedirectToAction("ListOfTeachers", "teacher");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            model.drpSubjects = _context.Subjects.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(model);
        }
    }
}
