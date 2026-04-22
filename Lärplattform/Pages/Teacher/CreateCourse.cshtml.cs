using Data.DTOs;
using Data.Entities;
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
        public CreateCourseDTO NewCourse { get; set; } = new CreateCourseDTO();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                NewCourse.TeacherID = user.Id;
               
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }
            
         

            var client = HttpClientFactory.CreateClient("APIClient");
            var response = await client.PostAsJsonAsync("api/Course", NewCourse);
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
