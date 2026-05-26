using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class LessonModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;

        public LessonModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public List<LessonViewModel> Lessons { get; set; } = new List<LessonViewModel>();
        [BindProperty(SupportsGet = true)]
        public int CourseId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            var client = httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"api/Lessons/course/{CourseId}");
            if (response.IsSuccessStatusCode)
            {
                var lessonDTOs = await response.Content.ReadFromJsonAsync<List<LessonViewModel>>() ?? new List<LessonViewModel>();
                Lessons = lessonDTOs.Select(dto => new LessonViewModel
                {
                    LessonID = dto.LessonID,
                    Title = dto.Title,
                    Description = dto.Description,
                    Content = dto.Content,
                    CourseID = dto.CourseID,
                    TeacherID = dto.TeacherID,
                }).ToList();
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Error fetching lessons: {response.StatusCode}");
                return Page();


            }
        }
    }
}
