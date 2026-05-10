using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lärplattform.Pages.Teacher
{
    public class EditSubmissionModel : PageModel
    {
        public readonly IHttpClientFactory _HttpClientFactory;

        public EditSubmissionModel(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
          
        }

        [BindProperty]

        public UpdateSubmissionsViewModel UpdateSubmission {  get; set; } = new UpdateSubmissionsViewModel();

        public SelectList Grade { get; set; } = new SelectList(new List<dynamic>(), "Text", "Value");

        [BindProperty(SupportsGet = true)]
        public int Submissionid { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (UpdateSubmission == null)
                {
                    ModelState.AddModelError(string.Empty, "Submission not found.");
                    return Page();
                }


                var httpClient = _HttpClientFactory.CreateClient("APIClient");

                var response = await httpClient.GetAsync($"api/Submissions/{Submissionid}");
                if (response.IsSuccessStatusCode)
                {
                    var submission = await response.Content.ReadFromJsonAsync<SubmissonsViewModel>();
                    if (submission != null)
                    {
                        UpdateSubmission.Content = submission.Content;
                        UpdateSubmission.Feedback = submission.Feedback.ToString();
                        UpdateSubmission.Grade = (UpdateGradeEnumViewModel)submission.Grade;
                        UpdateSubmission.AssigmentId = submission.AssigmentId;
                        UpdateSubmission.UserId = submission.UserId;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to fetch submission details.");
                }



                await PopulateDropdown();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return Page();
        }




        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdown();
                return Page();
            }

            try
            {
              
                var httpClient = _HttpClientFactory.CreateClient("APIClient");
                var response = await httpClient.PutAsJsonAsync($"api/Submissions/{Submissionid}", UpdateSubmission);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Teacher/Submissions", new { AssigmentId = UpdateSubmission.AssigmentId});
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while updating the submission: {errorMessage}");
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
              

                var GradeValues = Enum.GetValues(typeof(UpdateGradeEnumViewModel))
                    .Cast<UpdateGradeEnumViewModel>()
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
