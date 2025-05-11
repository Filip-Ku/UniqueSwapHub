using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemHubFront.DTO;
using System.Text.Json;


namespace ItemHubFront.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserLoginDto UserLogin { get; set; }

        private readonly HttpClient _httpClient;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try 
            {
                var result = await _httpClient.PostAsJsonAsync("http://localhost:5250/api/users/login", UserLogin);
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to login");
                }
                else 
                {
                    var json = await result.Content.ReadAsStringAsync();

                    var userData = JsonSerializer.Deserialize<UserResponseDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return RedirectToPage("./Index");
                }
            }
            catch (HttpRequestException e) 
            {
                Console.WriteLine(e);
            }

            return RedirectToPage("./Index");
        }
    }
}
