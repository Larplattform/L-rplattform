using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class PlanningsModel : PageModel
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly UserManager<User> _userManager;

        public PlanningsModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public List<AssigementsViewModel> Assigements { get; set; } = new List<AssigementsViewModel>();

      
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var currentUserId = user.Id;

            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Assigments/teacher/{currentUserId}");

            if (response.IsSuccessStatusCode)
            {
                var teacher = await response.Content.ReadFromJsonAsync<List<AssigementsViewModel>>();
                if(teacher != null && teacher.Any())
                {
                    Assigements = teacher.Select(
                   a => new AssigementsViewModel
                   {
                       AssigmentID = a.AssigmentID,
                       Title = a.Title,
                       Description = a.Description,
                       Marks = a.Marks,
                      CourseID = a.CourseID,
                      Url = a.Url,


                       IsPublished = a.IsPublished
                   }).ToList();
                }
               
               
                else
                {
                    return NotFound("Teacher not found.");
                }

                return Page();
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error retrieving teacher data.");

            }
        }
    }
}
