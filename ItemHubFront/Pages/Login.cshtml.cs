using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemHubFront.DTO;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;


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
                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                var userResponse = JsonSerializer.Deserialize<UserResponseDto>(json);
                var token = userResponse.token;
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var name = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                HttpContext.Session.SetString("name", name ?? "");
                HttpContext.Session.SetString("userId", userId ?? "");
                HttpContext.Session.SetString("email", email ?? "");
                HttpContext.Session.SetString("role", role ?? "");

                return RedirectToPage("./Index");
            }
            catch (HttpRequestException e) 
            {
                Console.WriteLine(e);
            }

            return Page();
        }
    }
}
