using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Student
{
    public class ScheduleModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;
        public readonly UserManager<User> _userManager;

        public ScheduleModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            this.httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public List<ScheduleViewModel> Schedules { get; set; } = new List<ScheduleViewModel>();

        public int PageSize { get; set; }

        public int PageNumber { get; set; }



        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var currentUserId = user.Id;

            var httpClient = httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Schedule/studentId/{currentUserId}/page/{pageNumber}/size/{pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Schedules = System.Text.Json.JsonSerializer.Deserialize<List<ScheduleViewModel>>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                }) ?? new List<ScheduleViewModel>();
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
