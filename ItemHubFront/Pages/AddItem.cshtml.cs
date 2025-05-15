using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemHubFront.DTO;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet; // ← dodaj to



namespace ItemHubFront.Pages;

public class AddItemModel : PageModel
{
    [BindProperty]
    public ItemDto Item { get; set; } = new ItemDto();
    private readonly Cloudinary _cloudinary;

    private readonly HttpClient _httpClient;


    public AddItemModel(Cloudinary cloudinary, IHttpClientFactory httpClientFactory)
    {
        _cloudinary = cloudinary;
        _httpClient = httpClientFactory.CreateClient();
        
    }


    public async Task<IActionResult> OnPostAsync(IFormFile imageUpload)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (imageUpload != null && imageUpload.Length > 0)
        {
            var imageUrl = await UploadImageToCloudinary(imageUpload);
            Item.imageUrl = imageUrl;
        }

        try 
        {
            Item.available = true;
            Item.activated = false;
            Item.createdAt = DateTime.UtcNow;
            Item.userId = 1;

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(Item));


            var response = await _httpClient.PostAsJsonAsync("http://localhost:5250/api/items", Item);
        
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add item");
            }
        }
        catch (Exception ex)
        {
            return Page();
        }
       return RedirectToPage("./Index");
    }

    private async Task<string> UploadImageToCloudinary(IFormFile file)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Transformation = new Transformation().Width(800).Height(800).Crop("limit")
        };

        var uploadResult = await Task.Run(() => _cloudinary.Upload(uploadParams));

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return uploadResult.SecureUrl.AbsoluteUri;
        }
        else
        {
            return string.Empty;
        }
    }
}
