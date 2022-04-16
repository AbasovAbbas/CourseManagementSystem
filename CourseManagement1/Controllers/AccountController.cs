using CourseManagement.Models;
using CourseManagement.Models.ViewModels;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class AccountController : Controller
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(AccountController));
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
            using (ThreadContext.Stacks["NDC"].Push(Request.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var user = await userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                        {
                            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
                            if (result.Succeeded)
                            {
                                if (await userManager.IsInRoleAsync(user, "Admin"))
                                {
                                    LogicalThreadContext.Properties["user"] = user.UserName;
                                    _log.Info("logged in to the system");
                                    return RedirectToAction("index", "admin");
                                }
                                if (await userManager.IsInRoleAsync(user, "Teacher"))
                                {
                                    LogicalThreadContext.Properties["user"] = user.UserName;
                                    _log.Info("logged in to the system");
                                    return RedirectToAction("index", "teacher", new { user.Id });
                                }
                                if (await userManager.IsInRoleAsync(user, "Student"))
                                {
                                    LogicalThreadContext.Properties["user"] = user.UserName;
                                    _log.Info("logged in to the system");
                                    return RedirectToAction("index", "student", new { user.GroupId });
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Password yanlisdir");
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"{ex.Message}");
                        throw;
                    }
                }
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}
