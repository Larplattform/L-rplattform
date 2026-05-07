using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lärplattform.Pages.Teacher
{
    public class CreateAssigmentModel : PageModel
    {
        public readonly IHttpClientFactory HttpClientFactory;

        public CreateAssigmentModel(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        [BindProperty]

        public CreateAssigmentsViewModel Assigment { get; set; } = new CreateAssigmentsViewModel();

        public SelectList Lessons { get; set; } = new SelectList(new List<LessonViewModel>(), "LessonID", "Title");

       

        public async Task OnGetAsync()
        {
          
            await PopulateDropdown();
          
          
            
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
          if(!ModelState.IsValid)
            {
                await PopulateDropdown();
                return Page();
            }
          

            
            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.PostAsJsonAsync("api/Assigments", Assigment);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Teacher/Plannings");

            }
           
            ModelState.AddModelError(string.Empty, "Failed to create the assignment. Please try again.");
            await PopulateDropdown();
            return Page();
        }


        public async Task PopulateDropdown()
        {
            var httpClient = HttpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("api/Lessons");
            if (response.IsSuccessStatusCode)
            {
                var lessons = await response.Content.ReadFromJsonAsync<List<LessonViewModel>>();
                if (lessons != null)
                {
                    Lessons = new SelectList(lessons, "LessonID", "Title");
                }
                else
                {
                    throw new ApplicationException("Failed to retrieve lessons from the API.");
                }
            }
        }
        }
}
