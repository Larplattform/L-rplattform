using Data.DTOs;
using Data.Entities;
using Lärplattform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lärplattform.Pages
{
    public class IndexModel : PageModel
    {

        public readonly IHttpClientFactory _HttpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public TotalCountDTO TotalCount { get; set; } = new TotalCountDTO();

        public async Task<IActionResult> OnGetAsync()
        {
           
            

            var client = _HttpClientFactory.CreateClient("APIClient");

            var response = await client.GetAsync($"api/TotalStats/TotalStats");

            if (response.IsSuccessStatusCode)
            {
               var result = await response.Content.ReadFromJsonAsync<TotalCountDTO>();
                if(result != null)
                {
                    TotalCount = result;
                }
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Error fetching totalstats: {response.StatusCode}");
                return Page();
            }
        }
    }
}
