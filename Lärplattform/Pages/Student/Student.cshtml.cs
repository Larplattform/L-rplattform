using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Student
{
    public class StudentModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;
        public readonly UserManager<User> _userManager;
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public StudentModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            this.httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }



        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var currentUserId = user.Id;

            var client = httpClientFactory.CreateClient("APIClient");

            var response = await client.GetAsync($"api/Course/student/{currentUserId}");

            if (response.IsSuccessStatusCode)
            {
                var courseDTOs = await response.Content.ReadFromJsonAsync<List<CourseDTO>>() ?? new List<CourseDTO>();
                Courses = courseDTOs.Select(dto => new CourseViewModel
                {
                    CourseID = dto.CourseID,
                    SubjectName = dto.SubjectName,
                    TotalMarks = dto.TotalMarks,
                    ClassName = dto.ClassName,
                    TeacherName = dto.TeacherName,
                    TeacherID = dto.TeacherID,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
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
