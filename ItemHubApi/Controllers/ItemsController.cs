using Microsoft.AspNetCore.Mvc;
using HubApi.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<ItemDetails>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDetails>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (!await ItemExistsAsync(id))
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<IEnumerable<ItemDetails>>> SearchItems(string searchText)
        {
            var items = await _context.Items
                .Where(i => i.name.Contains(searchText))
                .ToListAsync();

            if (!items.Any())
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDetails>> PostItem(ItemDetails item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.itemId }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(int id, ItemDetails item)
        {
            if (id != item.itemId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ItemExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDetails>> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        [HttpGet("activated")]
        public async Task<ActionResult<IEnumerable<ItemDetails>>> GetActivatedItems()
        {
            var items = await _context.Items.Where(i => i.activated == true).ToListAsync();
            return Ok(items);
        }

        [HttpGet("unactivated")]
        public async Task<ActionResult<IEnumerable<ItemDetails>>> GetUnactivatedItems()
        {
            var items = await _context.Items.Where(i => i.activated == false).ToListAsync();
            return Ok(items);
        }


        [HttpGet("count")]
        public async Task<ActionResult<int>> GetItemsCount()
        {
            var count = await _context.Items.CountAsync();
            return Ok(count);
        }

        // Helper
        private async Task<bool> ItemExistsAsync(int id)
        {
            return await _context.Items.AnyAsync(e => e.itemId == id);
        }
    }
}
