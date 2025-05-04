using Microsoft.AspNetCore.Mvc;
using HubApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int userId)
        {
            var reservations = await _context.Reservations.Where(r => r.userId == userId).ToListAsync();
            return Ok(reservations);
        }

        [HttpGet("item/{itemId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByItem(int itemId)
        {
            var reservations = await _context.Reservations.Where(r => r.itemId == itemId).ToListAsync();
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return Ok(reservation);
        }
    }
}