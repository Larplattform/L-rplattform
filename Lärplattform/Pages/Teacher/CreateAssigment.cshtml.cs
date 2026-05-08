using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lärplattform.Pages.Teacher
{
    public class CreateAssigmentModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;

        public readonly UserManager<User>  _userManager;

        public CreateAssigmentModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager )
        {
            HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [BindProperty]

        public CreateAssigmentsViewModel Assigment { get; set; } = new CreateAssigmentsViewModel();

        public SelectList Courses { get; set; } = new SelectList(new List<CourseViewModel>(), "CourseID", "SubjectName");



        public async Task OnGetAsync()
        {
          

            var UserId = _userManager.GetUserId(User);

            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/Course");
            if (response.IsSuccessStatusCode)
            {
                var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
                if (courses != null && UserId != null)
                {

                    var teacherCourses = courses.Where(c => c.TeacherID.ToString() == UserId).ToList();
                    Courses = new SelectList(teacherCourses, "CourseID", "SubjectName");

                }
                else
                {
                    throw new ApplicationException("Failed to retrieve courses from the API.");
                }
                await PopulateDropDown();

            }
        }
        public async Task<IActionResult> OnPostAsync(int duration)
        {
          if(!ModelState.IsValid)
            {
                await PopulateDropDown();
                return Page();
            }

            var end = Assigment.DueDate ?? DateTime.Now.AddMonths(duration);

            var newassigment = new CreateAssigmentsDTO
            {
                Title = Assigment.Title,
                Description = Assigment.Description,
                Url = Assigment.Url,
                DueDate = end,
                Marks = Assigment.Marks,
                CourseID = Assigment.CourseID,
            };

            
            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.PostAsJsonAsync("api/Assigments", newassigment);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Plannings");

            }
           
            ModelState.AddModelError(string.Empty, "Failed to create the assignment. Please try again.");
            await PopulateDropDown();
            return Page();
        }


        public async Task PopulateDropDown()
        {
            try
            {
                var UserId = _userManager.GetUserId(User);
                var httpClient = HttpClientFactory.CreateClient("APIClient");
                var response = await httpClient.GetAsync("api/Course");
                if (response.IsSuccessStatusCode)
                {
                    var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
                    if (courses != null && UserId != null)
                    {
                        var teacherCourses = courses.Where(c => c.TeacherID.ToString() == UserId).ToList();
                        Courses = new SelectList(teacherCourses, "CourseID", "SubjectName");
                    }

                }
                else
                {
                    throw new ApplicationException("Failed to retrieve courses from the API.");
                }

               


            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while populating the dropdown.", ex);
            }

        }
    }
}
