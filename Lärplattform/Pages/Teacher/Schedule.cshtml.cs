using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class ScheduleModel : PageModel
    {
        public readonly IHttpClientFactory httpClientFactory;

        public ScheduleModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public List<ScheduleViewModel> Schedules { get; set; } = new List<ScheduleViewModel>();

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
       
            var httpClient = httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Schedule/page/{pageNumber}/size/{pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Schedules = System.Text.Json.JsonSerializer.Deserialize<List<ScheduleViewModel>>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
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
