using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class TeacherModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;
        public readonly UserManager<User> _userManager;
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public TeacherModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
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
            var response = await client.GetAsync("api/Course");
            if (response.IsSuccessStatusCode)
            {
                var courseDTOs = await response.Content.ReadFromJsonAsync<List<CourseDTO>>() ?? new List<CourseDTO>();
                Courses = courseDTOs.Where(dto => dto.TeacherID == currentUserId).Select(dto => new CourseViewModel
                {
                    CourseID = dto.CourseID,
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

