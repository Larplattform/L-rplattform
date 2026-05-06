using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public SelectList CourseSelectList { get; set; } = new SelectList( new List<CourseViewModel>(), "CourseID", "SubjectName");

        [BindProperty(SupportsGet = true)]
        public int ScheduleID { get; set; }

        public SelectList Location { get; set; } = new SelectList( new List<dynamic>(), "Text", "Value");

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (UpdateSchedule == null)
                {
                    ModelState.AddModelError(string.Empty, "Schedule not found.");
                    return Page();
                }


                var httpClient = HttpClientFactory.CreateClient("APIClient");

                var response = await httpClient.GetAsync($"api/Schedule/{ScheduleID}");
                if (response.IsSuccessStatusCode)
                {
                    var schedule = await response.Content.ReadFromJsonAsync<ScheduleViewModel>();
                    if (schedule != null)
                    {
                        UpdateSchedule.StartDate = schedule.StartDate;
                        UpdateSchedule.EndDate = schedule.EndDate;
                        UpdateSchedule.Location = (LocationEnumUpdateViewModel)schedule.Location;
                        UpdateSchedule.CourseID = schedule.CourseID;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to fetch schedule details.");
                }

              

                await PopulateDropdown();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int Duration)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdown();
                return Page();
            }

            try
            {
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
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            await PopulateDropdown();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            try
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
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            await PopulateDropdown();
            return Page();
        }

        public async Task PopulateDropdown()
        {
            try
            {
                var httpClient = HttpClientFactory.CreateClient("APIClient");
                var response = await httpClient.GetAsync("api/Course");
                if (response.IsSuccessStatusCode)
                {
                    var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
                    if (courses != null)
                    {
                        CourseSelectList = new SelectList(courses, "CourseID", "SubjectName");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to fetch courses.");
                }

                var locationValues = Enum.GetValues(typeof(LocationEnumUpdateViewModel))
                    .Cast<LocationEnumUpdateViewModel>()
                    .Select(e => new { Value = (int)e, Text = e.ToString() })
                    .ToList();
                Location = new SelectList(locationValues, "Value", "Text");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while populating dropdowns: {ex.Message}");
            }
        }
    }
}