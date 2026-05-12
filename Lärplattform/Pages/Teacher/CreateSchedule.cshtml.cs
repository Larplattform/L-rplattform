using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public SelectList Courses { get; set; } = new SelectList( new List<CourseViewModel>(), "CourseID", "SubjectName");

        public SelectList Location = new SelectList(new List<dynamic>(), "Value", "Text");
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

                    var teacherCourses = courses.Where(c => c.TeacherID.ToString() == UserId).ToList();
                    Courses = new SelectList(teacherCourses, "CourseID", "SubjectName");

                }
                else
                {
                    throw new ApplicationException("Failed to retrieve courses from the API.");
                } 
                await PopulateDropDown();

            }
            }
       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                    await PopulateDropDown();
                return Page();
            }

            if(Schedule.StartDate >= Schedule.EndDate)
            {
                ModelState.AddModelError(string.Empty, "Enddate must be after Startdate");
                return Page();
            }
         

            var client = HttpClientFactory.CreateClient("APIClient");

           

            var createdScheduleDto = new Data.DTOs.CreateScheduleDTO
            {
                CourseID = Schedule.CourseID,
                EndDate = Schedule.EndDate ?? DateTime.Now,
                StartDate = Schedule.StartDate ?? DateTime.Now,
                Location = (Data.DTOs.LocationEnumCreateDTO)Schedule.Location



            };

            var response = await client.PostAsJsonAsync("api/Schedule", createdScheduleDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Schedule");
            }
            ModelState.AddModelError(string.Empty, "An error occurred while creating the schedule. Please try again.");
            await PopulateDropDown();
            return Page();
        }

        public async Task PopulateDropDown()
        {
            try
            {
                var UserId = _userManager.GetUserId(User);
                var httpClient = HttpClientFactory.CreateClient("APIClient");
                var response = await httpClient.GetAsync("api/Course");
                if (response.IsSuccessStatusCode)
                {
                    var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
                    if (courses != null && UserId != null)
                    {
                        var teacherCourses = courses.Where(c => c.TeacherID.ToString() == UserId).ToList();
                        Courses = new SelectList(teacherCourses, "CourseID", "SubjectName");
                    }

                }
                else
                {
                    throw new ApplicationException("Failed to retrieve courses from the API.");
                }

                var locationValues = Enum.GetValues(typeof(LocationEnumCreateViewModel))
                   .Cast<LocationEnumCreateViewModel>()
                   .Select(e => new { Value = (int)e, Text = e.ToString() })
                   .ToList();
                Location = new SelectList(locationValues, "Value", "Text");


            } catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while populating the dropdown.", ex);
            }

        }
    }
}
