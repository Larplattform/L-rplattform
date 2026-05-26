using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class CreateLessonModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;
        public readonly UserManager<User> _userManager;


        public CreateLessonModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }
        [BindProperty]
        public CreateLessonViewModel NewLesson { get; set; } = new CreateLessonViewModel();

        public void OnGet(int courseId)
        {
            NewLesson.CourseID = courseId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please log in again.");
                return Page();
            }
            if (NewLesson.CourseID == 0)
            {
                ModelState.AddModelError(string.Empty, "CourseID couldnt be found");
                return Page();
            }
            NewLesson.TeacherID = user.Id;

            ModelState.Remove("NewLesson.TeacherID");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createLessonDTO = new CreateLessonViewModel
            {
                Title = NewLesson.Title,
                Description = NewLesson.Description,
                Content = NewLesson.Content,
                CourseID = NewLesson.CourseID,
                TeacherID = NewLesson.TeacherID,
              
                
            };
            var client = HttpClientFactory.CreateClient("APIClient");
            var response = await client.PostAsJsonAsync("api/Lessons", createLessonDTO);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Lesson", new { CourseId = NewLesson.CourseID });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error creating lesson: {response.StatusCode} , {errorContent}");
                return Page();
            }
        }
    }
}
