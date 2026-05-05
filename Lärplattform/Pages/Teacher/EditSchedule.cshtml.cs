using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class EditScheduleModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;

        public EditScheduleModel(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }



        [BindProperty]

        public UpdateScheduleViewModel UpdateSchedule { get; set; } = new UpdateScheduleViewModel();
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        [BindProperty(SupportsGet = true)]
        public int ScheduleID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = HttpClientFactory.CreateClient("APIClient");
           
            var response = await httpClient.GetAsync($"api/Schedule/{ScheduleID}");
            if (response.IsSuccessStatusCode)
            {
                var schedule = await response.Content.ReadFromJsonAsync<ScheduleViewModel>();
                if (schedule != null)
                {
                  
                    UpdateSchedule.StartDate = schedule.StartDate;
                    UpdateSchedule.EndDate = schedule.EndDate;
                    UpdateSchedule.Location = schedule.Location;
                    UpdateSchedule.CourseID = schedule.CourseID;
                }
            }
            var coursesResponse = await httpClient.GetAsync("api/Course");
            if (coursesResponse.IsSuccessStatusCode)
            {
                Courses = await coursesResponse.Content.ReadFromJsonAsync<List<CourseViewModel>>() ?? new List<CourseViewModel>();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync( int Duration)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UpdateSchedule.EndDate = UpdateSchedule.StartDate.AddHours(Duration);
            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.PutAsJsonAsync($"api/Schedule/{ScheduleID}", UpdateSchedule);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Schedule");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the schedule: {errorMessage}");

                var coursesResponse = await httpClient.GetAsync("api/Course");
                if (coursesResponse.IsSuccessStatusCode)
                {
                    Courses = await coursesResponse.Content.ReadFromJsonAsync<List<CourseViewModel>>() ?? new List<CourseViewModel>();
                }

                return Page();
            }

        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.DeleteAsync($"api/Schedule/{ScheduleID}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Schedule");
            }
            else
            {
               var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the schedule: {errorMessage}");
                return Page();
            }
        }
    }
}