using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lärplattform.Pages.Teacher
{
    public class EditFinalGradeModel : PageModel
    {
        public readonly IHttpClientFactory _HttpClientFactory;
        public readonly UserManager<User> _userManager;


        public EditFinalGradeModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [BindProperty]

        public GradeReportViewModel FinalGradeUpdate {  get; set; } = new GradeReportViewModel();

        public SelectList Grade { get; set; } = new SelectList(new List<dynamic>(), "Text", "Value");

       

        [BindProperty(SupportsGet = true)]

        public int CourseId { get; set; }

        [BindProperty(SupportsGet = true)]

        public int StudentId { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToPage("/Account/Login");
                }

                int teacherid = user.Id;

            
                var httpClient = _HttpClientFactory.CreateClient("APIClient");
                var response = await httpClient.GetAsync($"api/Submissions/GradeReport{CourseId}/{StudentId}/1/size/1");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                   var FinalGrade = System.Text.Json.JsonSerializer.Deserialize<List<GradeReportViewModel>>(content, new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,

                    }) ?? new List<GradeReportViewModel>();

                    FinalGradeUpdate = FinalGrade?.FirstOrDefault() ?? new GradeReportViewModel();
                }
                else
                {
                    // Handle error response
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching Report.");
                }

                await PoplulateDropDown();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please log in again.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                await PoplulateDropDown();
                return Page();
            }
            var client = _HttpClientFactory.CreateClient("APIClient");
           
            var response = await client.PostAsync($"api/Course/SetFinalGradeForCourse/{StudentId}/{CourseId}/{FinalGradeUpdate.FinalGrade}",null);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Reports");
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the grade. Please try again.");
            await PoplulateDropDown(); 
            
            
            return Page();

        }


        public async Task PoplulateDropDown()
        {
            try
            {


                var GradeValues = Enum.GetValues(typeof(GradeEnumViewModel))
                    .Cast<GradeEnumViewModel>()
                    .Select(e => new { Value = (int)e, Text = e.ToString() })
                    .ToList();
                Grade = new SelectList(GradeValues, "Value", "Text");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while populating dropdowns: {ex.Message}");
            }
        }
    }
}
