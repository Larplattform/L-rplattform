using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    [Authorize(Roles = "Teacher")]
    public class SubmissionsModel : PageModel
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public SubmissionsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<SubmissonsViewModel> Submission  = new List<SubmissonsViewModel>();

        [BindProperty(SupportsGet = true)]
        public int AssigmentId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
        public async Task<IActionResult> OnGetAsync(int AssigmentId)
        {
          

            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Submissions/assigment{AssigmentId}");
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
