using CourseManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Data
{
    public static class DbInitializer
    {
        static public IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                InitSubjects(db);
                string[] roleNames = { "Admin", "Teacher", "Student" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    var roleExits = roleManager.RoleExistsAsync(roleName);
                    if (roleExits.Result == false)
                    {
                        roleResult = roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                    }
                }
                string Email = "admin123@mail.ru";
                string Password = "Admin_123";
                if (userManager.FindByEmailAsync(Email).Result == null)
                {
                    ApplicationUser user = new ApplicationUser() {
                        UserName = Email,
                        Email = Email,
                        EmailConfirmed = true
                    };
                    IdentityResult result = userManager.CreateAsync(user, Password).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
            }
            return app;
        }

        private static void InitSubjects(ApplicationDbContext db)
        {
            if (!db.Subjects.Any())
            {
                    db.Subjects.AddRange(new Subject
                    {
                        Id = 1,
                        Title = "Cebr"
                    },new Subject
                    {
                        Id=2,
                        Title = "Hendese"
                    }, new Subject
                    {
                        Id=3,
                        Title = "C# proqramlasdirma"
                    }, new Subject
                    {
                        Id = 4,
                        Title = "Web proqramlasdirma"
                    }, new Subject
                    {
                        Id = 5,
                        Title = "VBIS - SQL"
                    }, new Subject
                    {
                        Id = 6,
                        Title = "Java proqramlasdirma"
                    });
                    db.SaveChanges();
            }  
        }
    }
}
