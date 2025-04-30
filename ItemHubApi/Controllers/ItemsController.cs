using Microsoft.AspNetCore.Mvc;
using HubApi.Data;
using HubApi.Models;  

namespace HubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDetails>> GetItems()
        {
            return Ok(_context.Items.ToList());
        }

    }
}