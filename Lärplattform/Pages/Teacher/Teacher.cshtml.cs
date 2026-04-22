using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class TeacherModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;
        public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();

        public TeacherModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }



        public async Task<IActionResult> OnGetAsync()
        {
            var client = httpClientFactory.CreateClient("APIClient");
            var response = await client.GetAsync("api/Course");
            if (response.IsSuccessStatusCode)
            {
                Courses = await response.Content.ReadFromJsonAsync<List<CourseDTO>>() ?? new List<CourseDTO>();
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Error fetching courses: {response.StatusCode}");
                return Page();
            }
        }
    }
}

