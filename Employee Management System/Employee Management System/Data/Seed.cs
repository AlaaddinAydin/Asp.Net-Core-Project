using Employee_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net;

namespace Employee_Management_System.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee()
                        {
                            Name = "Murat",
                            Surname = "Microsoft",    
                            Salary = 3000,
                            Department = new Department()
                            {
                                DepartmentName = "Human Resources",
                            }
                         },
                        new Employee()
                        {
                            Name = "Mert",
                            Surname = "Kuru",
                            Salary = 7500,
                            Department = new Department()
                            {
                                DepartmentName = "Information Technology",
                            }
                        },
                        new Employee()
                        {
                            Name = "Canan",
                            Surname = "Duran",
                            Salary = 10000,
                            Department = new Department()
                            {
                                DepartmentName = "Lead Programmer",
                            }
                        },
                        new Employee()
                        {
                            Name = "Kamuran",
                            Surname = "Coskun",
                            Salary = 15000,
                            Department = new Department()
                            {
                                DepartmentName = "Programmer",
                            }
                        }
                    });
                    context.SaveChanges();
                }
                
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Department = new Department()
                        {
                            DepartmentName = "Admin",
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Department = new Department()
                        {
                            DepartmentName = "Management",
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
