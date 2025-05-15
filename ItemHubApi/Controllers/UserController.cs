using Microsoft.AspNetCore.Mvc;
using HubApi.Models;  
using HubApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace HubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            Console.WriteLine("UsersController initialized");
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == userLoginDto.email);
            if (user == null)
            {
                return NotFound();
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(userLoginDto.password, user.hashedPassword);
            if (!passwordValid)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new
            {
                userId = user.userId,
                email = user.email,
                name = user.name,
                role = user.role
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == userRegisterDto.email);
            if (existingUser != null)
            {
                return BadRequest("Email already exists");
            }

            var user = new User
            {
                email = userRegisterDto.email,
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.password),
                name = userRegisterDto.name,
                role = "user", 
                createdAt = DateTime.UtcNow,
                firstName = userRegisterDto.firstName,
                lastName = userRegisterDto.lastName
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }


    }
}