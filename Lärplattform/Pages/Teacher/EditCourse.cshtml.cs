using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class EditCourseModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;
        public readonly UserManager<User> _userManager;

        public EditCourseModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [BindProperty]
        public UpdateCourseViewModel NewCourse { get; set; } = new UpdateCourseViewModel();

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please log in again.");
                return Page();
            }

            NewCourse.TeacherID = user.Id;

            ModelState.Remove("NewCourse.TeacherID");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updateCourseDTO = new UpdateCourseDTO 
            {

                SubjectName = NewCourse.SubjectName,
                TotalMarks = NewCourse.TotalMarks,
                ClassName = NewCourse.ClassName,
                TeacherID = NewCourse.TeacherID
            };

            var client = HttpClientFactory.CreateClient("APIClient");
            var response = await client.PutAsJsonAsync($"api/Course/{id}", updateCourseDTO);
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

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = HttpClientFactory.CreateClient("APIClient");
            var response = await client.DeleteAsync($"api/Course/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Teacher");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error deleting course: {response.StatusCode} , {errorContent}");
                return Page();
            }
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            var client = HttpClientFactory.CreateClient("APIClient");

            var response = await client.GetAsync($"api/Course/{id}");

            if (response.IsSuccessStatusCode)
            {
                var courseDTO = await response.Content.ReadFromJsonAsync<CourseDTO>();
                if (courseDTO != null)
                {
                    NewCourse = new UpdateCourseViewModel
                    {
                        SubjectName = courseDTO.SubjectName,
                        TotalMarks = courseDTO.TotalMarks,
                        ClassName = courseDTO.ClassName,
                        TeacherID = courseDTO.TeacherID
                    };
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Course data is null.");
                    return Page();
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error fetching course: {response.StatusCode} , {errorContent}");
                return Page();
            }
        }

    }
}
