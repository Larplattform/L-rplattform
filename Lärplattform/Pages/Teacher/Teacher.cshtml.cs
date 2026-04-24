using Data.DTOs;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class TeacherModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

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
                var courseDTOs = await response.Content.ReadFromJsonAsync<List<CourseDTO>>() ?? new List<CourseDTO>();
                Courses = courseDTOs.Select(dto => new CourseViewModel
                {
                    SubjectName = dto.SubjectName,
                    TotalMarks = dto.TotalMarks,
                    ClassName = dto.ClassName,
                    TeacherID = dto.TeacherID,
                    Url = dto.Url,

                }).ToList();
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

