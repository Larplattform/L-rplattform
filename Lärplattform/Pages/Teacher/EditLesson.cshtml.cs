using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class EditLessonModel : PageModel
    {
        public readonly IHttpClientFactory _HttpClientFactory;
        public readonly UserManager<User> _userManager;

        public EditLessonModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _HttpClientFactory = httpClientFactory;
            _userManager = userManager;
            
        }
        [BindProperty]
        public UpdateLessonViewModel NewLesson { get; set; } = new UpdateLessonViewModel();
        public async Task<IActionResult> OnGetAsync(int lessonId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please log in again.");
                return Page();
            }

            NewLesson.LessonID = lessonId;

           

            var client = _HttpClientFactory.CreateClient("APIClient");

            var response = await client.GetAsync($"api/Lessons/{lessonId}");

            if (response.IsSuccessStatusCode)
            {
                var lessonDTO = await response.Content.ReadFromJsonAsync<UpdateLessonViewModel>();
                if (lessonDTO != null)
                {
                    NewLesson.Title = lessonDTO.Title;
                    NewLesson.Description = lessonDTO.Description;
                    NewLesson.Content = lessonDTO.Content;
                    NewLesson.CourseID = lessonDTO.CourseID;
                    NewLesson.TeacherID = lessonDTO.TeacherID;
                }
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Error fetching lesson: {response.StatusCode}");
                return Page();


            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please log in again.");
                return Page();
            }
            NewLesson.TeacherID = user.Id;

            ModelState.Remove("NewLesson.TeacherID");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var updateLessonDTO = new UpdateLessonViewModel
            {
                LessonID = NewLesson.LessonID,
                Title = NewLesson.Title,
                Description = NewLesson.Description,
                Content = NewLesson.Content,
                CourseID = NewLesson.CourseID,
                TeacherID = NewLesson.TeacherID,
            };
            var client = _HttpClientFactory.CreateClient("APIClient");
            var response = await client.PutAsJsonAsync($"api/Lessons/{NewLesson.LessonID}", updateLessonDTO);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Lesson", new { CourseId = NewLesson.CourseID });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error updating lesson: {response.StatusCode} , {errorContent}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var client = _HttpClientFactory.CreateClient("APIClient");
            var response = await client.DeleteAsync($"api/Lessons/{NewLesson.LessonID}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Lesson", new { CourseId = NewLesson.CourseID });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error deleting lesson: {response.StatusCode} , {errorContent}");
                return Page();
            }
        }
        }
}
