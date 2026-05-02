using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class CreateLessonModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;

        public CreateLessonModel(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public CreateLessonViewModel NewLesson { get; set; } = new CreateLessonViewModel();
        public void OnGet(int courseID)
        {
            NewLesson.CourseID = courseID;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var createLessonDTO = new CreateLessonViewModel
            {
                Title = NewLesson.Title,
                Description = NewLesson.Description,
                Content = NewLesson.Content,
                CourseID = NewLesson.CourseID
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
