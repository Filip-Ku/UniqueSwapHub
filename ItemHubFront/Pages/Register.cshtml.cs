using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemHubFront.DTO;
using System.Net.Http;

namespace ItemHubFront.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserRegisterDto UserRegister { get; set; }

        [BindProperty]
        public string? ConfirmPassword { get; set; }

        private readonly HttpClient _httpClient;

        public RegisterModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

         public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserRegister.Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return Page();
            }

             var userData = new UserRegisterDto
             {
                Email = UserRegister.Email,
                Password = UserRegister.Password,
                Name = UserRegister.Name,
                FirstName = UserRegister.FirstName,
                LastName = UserRegister.LastName
            };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5250/api/users/register",userData);



            return RedirectToPage("./Login");
        }
    }
}
