using CourseManagement.Models;
using CourseManagement.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                { 
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent : false, true);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("index", "admin");
                        }

                        if (await userManager.IsInRoleAsync(user, "Teacher"))
                        {
                            return RedirectToAction("index", "teacher", new {user.Id });
                        }

                        if (await userManager.IsInRoleAsync(user, "Student"))
                        {
                            return RedirectToAction("index", "student", new { user.Id } );
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bele istifadeci movcud deyil");
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}
