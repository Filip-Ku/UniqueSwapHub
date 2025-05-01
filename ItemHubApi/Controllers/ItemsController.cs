using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public ActionResult<ItemDetails> GetItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }


        [HttpPost]
        public ActionResult<ItemDetails> PostItem(ItemDetails item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetItem), new { id = item.ItemId }, item);
        }

    }
}