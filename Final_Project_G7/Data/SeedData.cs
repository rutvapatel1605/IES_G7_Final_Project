using Final_Project_G7.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Final_Project_G7.Authorization;

//----------------Final_Project_G7------------------------

namespace Final_Project_G7.Data
{
    public class SeedData
    {
        //Method to initialize and seed the database 
        public static async Task Initialize(IServiceProvider serviceProvider, string adminUserPw, string managerUserPw)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminID = await EnsureUser(serviceProvider, adminUserPw, "admin@databasesolution.com");
                await EnsureRole(serviceProvider, adminID, Authorization.Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, managerUserPw, "manager@databasesolution.com");
                await EnsureRole(serviceProvider, managerID, Authorization.Constants.ContactManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string adminUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, adminUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        //Method to seed the database with initial contact data

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }

            //Adding a collection of Contact entities to the context

            context.Contact.AddRange(
                new Contact
                {
                    Name = "Debra Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    State = "Ontario",
                    Zip = "P7H 3K5",
                    Email = "debra@databasesolution.com",
                    Status = ContactStatus.Approved,
                    OwnerId = adminID
                },
                new Contact
                {
                    Name = "Thorsten Weinrich",
                    Address = "5678 1st Ave W",
                    City = "Orillia",
                    State = "Ontario",
                    Zip = "Y8U 6H6",
                    Email = "thorsten@databasesolution.com"
                },
                new Contact
                {
                    Name = "Yuhong Li",
                    Address = "9012 State st",
                    City = "Toronto",
                    State = "Ontario",
                    Zip = "A5D 9B3",
                    Email = "yuhong@databasesolution.com"
                },
                new Contact
                {
                    Name = "Jon Orton",
                    Address = "3456 Maple St",
                    City = "Hamilton",
                    State = "Ontario",
                    Zip = "H7N 7J8",
                    Email = "jon@databasesolution.com"
                },
                new Contact
                {
                    Name = "Diliana Alexieva-Bosseva",
                    Address = "7890 2nd Ave E",
                    City = "Milton",
                    State = "Ontario",
                    Zip = "L3M 5T4",
                    Email = "diliana@databasesolution.com"
                }
             );

            //save changes to persist the seeded data into the database
            context.SaveChanges();
        }
    }
}
