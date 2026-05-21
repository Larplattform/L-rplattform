using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Student
{
    public class SubmissionsModel : PageModel
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly UserManager<User> _userManager;

        public SubmissionsModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public List<SubmissonsViewModel> Submission = new List<SubmissonsViewModel>();

        [BindProperty(SupportsGet = true)]
        public int AssigmentId { get; set; }

       
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var currentUserId = user.Id;

            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Submissions/assignment/{AssigmentId}/student{currentUserId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Submission = System.Text.Json.JsonSerializer.Deserialize<List<SubmissonsViewModel>>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                }) ?? new List<SubmissonsViewModel>();
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, "An error occurred while fetching schedules.");
            }
            return Page();
        }
    }
}

