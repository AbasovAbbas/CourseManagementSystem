using CourseManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
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
                //CreateLogTable();
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
                        EmailConfirmed = true,
                        EnrollmentNo = -1
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

        /*private static void CreateLogTable()
        {
            SqlConnection conn = new SqlConnection();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            string ConnectionString = "Integrated Security=SSPI;" +
            "Initial Catalog=SchoolDB;" +
            "Data Source=DESKTOP-L3IGC35;";
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string sql = "CREATE TABLE Logs" +
            "([Id] [int] IDENTITY(1, 1) NOT NULL," +
            "[Date] [datetime] NOT NULL," +
            "[Thread] [varchar](255) NOT NULL," +
            "[Level] [varchar](50) NOT NULL," +
            "[Logger] [varchar](255) NOT NULL," +
            "[Message] [varchar](4000) NOT NULL," +
            "[Exception] [varchar](2000) NULL)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }*/

        private static void InitSubjects(ApplicationDbContext db)
        {
            if (!db.Subjects.Any())
            {
                    db.Subjects.AddRange(new Subject
                    {
                        Title = "Cebr"
                    },new Subject
                    {
                        Title = "Hendese"
                    }, new Subject
                    {
                        Title = "C# proqramlasdirma"
                    }, new Subject
                    {
                        Title = "Web proqramlasdirma"
                    }, new Subject
                    {
                        Title = "VBIS - SQL"
                    }, new Subject
                    {
                        Title = "Java proqramlasdirma"
                    });
                    db.SaveChanges();
            }  
        }
    }
}
