using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApi.Data;
using MyApi.Models;
using MyApi.Models.DTOs;
using MyApi.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, PasswordService passwordService, IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT.
        /// </summary>
        /// <param name="loginRequest">The login credentials.</param>
        /// <returns>A JWT upon successful authentication.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);

            if (user == null || !_passwordService.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return Problem(detail: "Invalid username or password.", statusCode: StatusCodes.Status401Unauthorized);
            }

            var token = GenerateJwtToken(user.Id, user.Username, user.Role);
            return Ok(new LoginResponse { Token = token });
        }

        /// <summary>
        /// Registers a new user. (Admin Only)
        /// </summary>
        /// <param name="registerRequest">The user registration details.</param>
        /// <returns>The created user.</returns>
        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Username == registerRequest.Username);
            if (existingUser)
            {
                return BadRequest(new ProblemDetails { Detail = "Username already exists." });
            }

            var user = new User
            {
                Username = registerRequest.Username,
                PasswordHash = _passwordService.HashPassword(registerRequest.Password),
                Role = registerRequest.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Avoid returning the password hash
            user.PasswordHash = string.Empty;

            return CreatedAtAction(nameof(Login), new { /* no route values */ }, user);
        }

        private string GenerateJwtToken(int userId, string username, string role)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT key is not configured in appsettings.Development.json");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
