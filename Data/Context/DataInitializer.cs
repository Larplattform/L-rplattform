using Microsoft.AspNetCore.Identity;
using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Data.Context
{
    /// <summary>
    /// Seeds initial data for the application.
    /// </summary>
    /// <param name="dbContext">The database to seed for.</param>
    /// <param name="userManager">The manager to seed users.</param>
    public class DataInitializer(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        /// <summary>
        /// Seeds initial data asynchronously.
        /// </summary>
        public async Task SeedData()
        {
            await dbContext.Database.MigrateAsync();

            await SeedRoles();
            await SeedUsers();
            await SeedCourses();

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Seeds initial users for the application.
        /// </summary>
        private async Task SeedUsers()
        {
            await AddUserIfNotExists("Admin1@admin.com", "Erik", "Lund", "Lundgatan 2", "Stockholm", "Sweden", "Hejsan123#", ["Admin"]);
            await AddUserIfNotExists("Elev1@elev.com", "Amanda", "Persson", "Lundgatan 29", "Stockholm", "Sweden", "Hejsan123#", ["Student"]);
            await AddUserIfNotExists("Larare@larare.com", "Gunila", "Andersson", "Lundgatan 3", "Stockholm", "Sweden", "Hejsan123#", ["Teacher"]);
        }

        /// <summary>
        /// Seeds initial roles for the application.
        /// </summary>
        private async Task SeedRoles()
        {
            await AddRoleIfNotExisting("Admin");
            await AddRoleIfNotExisting("Student");
            await AddRoleIfNotExisting("Teacher");
        }

        /// <summary>
        /// Seeds initial sessions for the application.
        /// </summary>
        /// <returns></returns>
        private async Task SeedCourses()
        {
            if (await dbContext.Courses.AnyAsync())
            {
                return;
            }


            var teacher = await userManager.FindByEmailAsync("Larare@larare.com");
            var student = await userManager.FindByEmailAsync("Elev1@elev.com");

            if (teacher == null)
            {
                throw new Exception("Teacher user not found. Ensure users are seeded before courses.");
            }

            var MathematicsCourse = new Course
            {
                SubjectName = "Matematik 101",
                TotalMarks = 100,
                ClassName = "MatteA",
                TeacherID = teacher.Id,
                Users = new List<User> { teacher, student! }



            };

            await dbContext.Courses.AddAsync(MathematicsCourse);
            await dbContext.SaveChangesAsync();

            var Mathlesson = new Lesson
            {
                CourseID = MathematicsCourse.CourseID,
                Title = "Introduktion till Algebra",
                Content = "Innehåll för Matematik 101",

                Description = "Lär dig grunderna i algebra, inklusive variabler, ekvationer och funktioner.",

            };

            await dbContext.Lessons.AddAsync(Mathlesson);
            await dbContext.SaveChangesAsync();

            var MathAssignment = new Assigment
            {
                Title = "Algebra Uppgift 1",
                Description = "Lös följande ekvationer: 2x + 3 = 7, x^2 - 4 = 0",
                Marks = 20,
                LessonID = Mathlesson.LessonID,
                IsPublished = true

            };

            await dbContext.Assigments.AddAsync(MathAssignment);
            await dbContext.SaveChangesAsync();

            var Schedule = new Schedule
            {
                CourseID = MathematicsCourse.CourseID,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                Location = "Online",
                Course = MathematicsCourse

            };

            await dbContext.Schedules.AddAsync(Schedule);
            await dbContext.SaveChangesAsync();


        }

        /// <summary>
        /// Adds a role by name if it does not already exist.
        /// </summary>
        /// <param name="roleName">The name to add.</param>
        private async Task AddRoleIfNotExisting(string roleName)
        {
            var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (role != null)
            {
                return;
            }

            role = new IdentityRole<int> { Name = roleName, NormalizedName = roleName };
            await dbContext.Roles.AddAsync(role);
        }

        /// <summary>
        /// Adds a user by username if it doesn't already exist, along with password and roles.
        /// </summary>
        /// <param name="userName">The username to set. Is be used to identify the user.</param>
        /// <param name="password">The password to set. Will be hashed.</param>
        /// <param name="roles">Roles to give this user.</param>
        private async Task AddUserIfNotExists(string userName, string firstName, string lastName, string address, string city, string country, string password, string[] roles)
        {
            var existingUser = await userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return;
            }
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                Country = country,
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, password);
            await userManager.AddToRolesAsync(user, roles);
        }

    }

}
