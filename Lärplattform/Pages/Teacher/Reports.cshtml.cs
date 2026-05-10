using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class ReportsModel : PageModel
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly UserManager<User> _userManager;

        public ReportsModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public List<GradeReportViewModel> Report { get; set; } = new List<GradeReportViewModel>();

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

      
        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            int teacherid = user.Id;
          
            PageNumber = pageNumber;
            PageSize = pageSize;

            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Submissions/AllGradeReports{teacherid}/{pageNumber}/size/{pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Report = System.Text.Json.JsonSerializer.Deserialize<List<GradeReportViewModel>>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                }) ?? new List<GradeReportViewModel>();
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, "An error occurred while fetching Report.");
            }
            return Page();
        }
    }
}
