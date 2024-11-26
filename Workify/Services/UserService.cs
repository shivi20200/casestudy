using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Security.Claims;
using System.Text;
using Workify.Data;
using Workify.DTOs;
using Workify.Models;

namespace Workify.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterUserAsync(RegisterDto registerDto)
        {
            // Map RegisterDto to User model
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), // Hash the password
                Role = registerDto.Role // Ensure Role is either "Employer" or "JobSeeker"
            };

            // Check if the email already exists
            if (_context.Users.Any(u => u.Email == user.Email))
                throw new ArgumentException("Email is already registered.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "Registration successful";
        }


        public async Task<string> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            // Generate JWT token
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Name),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.Role, user.Role),
           // new Claim(JwtRegisteredClaimNames.Sub, user.Email),
           // new Claim("Role", user.Role),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
