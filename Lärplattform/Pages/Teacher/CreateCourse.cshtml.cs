using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class CreateCourseModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;
        public readonly UserManager<User> _userManager;

        public CreateCourseModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateCourseViewModel NewCourse { get; set; } = new CreateCourseViewModel();

        public async Task<IActionResult> OnPostAsync(int Duration)
        {
          
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
               ModelState.AddModelError(string.Empty, "User not found. Please log in again.");
                return Page();
            }

           NewCourse.TeacherID = user.Id;

            ModelState.Remove("NewCourse.TeacherID");

            if(!ModelState.IsValid)
            {
                return Page();
            }

            var start = NewCourse.StartDate ?? DateTime.Now;

            var end = NewCourse.EndDate ?? DateTime.Now.AddMonths(Duration);

            var createCourseDTO = new CreateCourseDTO
            {
                SubjectName = NewCourse.SubjectName,
                TotalMarks = NewCourse.TotalMarks,
                ClassName = NewCourse.ClassName,
                Url = NewCourse.Url,
                StartDate = start,
                EndDate = end,
                TeacherID = user.Id,
            };

          

            var client = HttpClientFactory.CreateClient("APIClient");
            var response = await client.PostAsJsonAsync("api/Course", createCourseDTO);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Teacher");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error creating course: {response.StatusCode} , {errorContent}");
                return Page();
            }
        }
        public void OnGet()
        {
        }
    }
}
