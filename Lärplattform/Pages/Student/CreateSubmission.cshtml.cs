using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages.Student
{
    public class CreateSubmissionModel : PageModel
    {
        public readonly IHttpClientFactory _HttpClientFactory;
        public readonly UserManager<User> _userManager;

        public CreateSubmissionModel(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _HttpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateSubmissionsViewModel CreateSubmission { get; set; } = new CreateSubmissionsViewModel();

        [BindProperty(SupportsGet = true)]
        public int AssigmentId { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var currentUserId = user.Id;

            CreateSubmission.UserId = currentUserId;

            ModelState.Remove("CreateSubmission.UserId");
         
            if (!ModelState.IsValid)
            {
             
                return Page();
            }

            var createSubmissionDTO = new CreateSubmissonsDTO
            {
                Content = CreateSubmission.Content,
                AssigmentId = this.AssigmentId,
                UserId = currentUserId
            };

            try
            {

                var httpClient = _HttpClientFactory.CreateClient("APIClient");
                var response = await httpClient.PostAsJsonAsync($"api/Submissions", createSubmissionDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Student/Submissions", new { AssigmentId = this.AssigmentId });
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the submission: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

           
            return Page();
        }




    }
}
