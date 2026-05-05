using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Teacher
{
    public class CreateScheduleModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;
        public readonly UserManager<User> _userManager;

        public CreateScheduleModel(IHttpClientFactory httpClientFactory , UserManager<User> userManager)
        {
            HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateScheduleViewModel Schedule { get; set; } = new CreateScheduleViewModel();

        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public async Task OnGetAsync()
        {
           Schedule.StartDate = DateTime.Now;
            Schedule.EndDate = DateTime.Now.AddHours(1);

            var UserId = _userManager.GetUserId(User);
            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/Course");
            if (response.IsSuccessStatusCode)
            {
                var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
                if (courses != null && UserId != null)
                {
                    
                    Courses = courses.Where(c => c.TeacherID.ToString() == UserId).ToList();

                }
            }
        }
       
        public async Task<IActionResult> OnPostAsync(int Duration)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

         

            var client = HttpClientFactory.CreateClient("APIClient");

            var start = Schedule.StartDate ?? DateTime.Now;

            var end = Schedule.EndDate ?? DateTime.Now.AddHours(Duration);

            var createdScheduleDto = new Data.DTOs.CreateScheduleDTO
            {
                CourseID = Schedule.CourseID,
                EndDate = end,
                StartDate = start,
                Location = Schedule.Location,
               
                

            };

            var response = await client.PostAsJsonAsync("api/Schedule", createdScheduleDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Schedule");
            }

            return Page();
        }
    }
}
