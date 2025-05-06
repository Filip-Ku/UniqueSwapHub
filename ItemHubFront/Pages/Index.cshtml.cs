using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ItemHubFront.DTO;

namespace ItemHubFront.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;

    public List<ItemDto>? Items { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public async Task OnGetAsync()
    {
        try 
        {
            var response = await _httpClient.GetAsync("http://localhost:5250/api/items/unactivated");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Items = JsonSerializer.Deserialize<List<ItemDto>>(json);
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Błąd pobierania danych z API");
        }
    }
}
