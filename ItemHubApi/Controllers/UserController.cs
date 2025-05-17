using Microsoft.AspNetCore.Mvc;
using HubApi.Models;  
using HubApi.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;


        public UsersController(AppDbContext context, IConfiguration configuration)
        {
            Console.WriteLine("UsersController initialized");
            _context = context;
            _configuration = configuration;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == userLoginDto.email);
            if (user == null)
            {
                return NotFound();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.userId.ToString()),
                new Claim(ClaimTypes.Role, user.role),
                new Claim("name", user.name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            bool passwordValid = BCrypt.Net.BCrypt.Verify(userLoginDto.password, user.hashedPassword);
            if (!passwordValid)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = new JwtSecurityToken(
                issuer: "ItemHub",
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
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