using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class ReportsModel : PageModel
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public ReportsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<SubmissonsViewModel> Report { get; set; } = new List<SubmissonsViewModel>();

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int StudentId { get; set; }

        [BindProperty(SupportsGet =true)]
        public int CourseId { get; set; }
        public async Task<IActionResult> OnGetAsync(int studentId,int courseId,int pageNumber = 1, int pageSize = 10)
        {
            
            StudentId = studentId;
            CourseId = courseId;
            PageNumber = pageNumber;
            PageSize = pageSize;

            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync($"api/Submissions/Gradereport{courseId}/{studentId}/{pageNumber}/size/{pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Report = System.Text.Json.JsonSerializer.Deserialize<List<SubmissonsViewModel>>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                }) ?? new List<SubmissonsViewModel>();
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
