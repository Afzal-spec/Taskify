using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Taskify.Api.Data;
using Taskify.Api.DTOs;
using Taskify.Api.DTOs.Auth;
using Taskify.Api.Models;

namespace Taskify.Api.Services
{
    public class AuthService
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration config;
        private readonly PasswordHasher<User> hasher;

        public AuthService(AppDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            this.config = config;
            this.hasher = new PasswordHasher<User>();
        }

        // REGISTER
        public async Task<User> RegisterAsync(RegisterDto dto)
        {
            var userExists = await dbContext.Users.AnyAsync(u => u.Email == dto.Email);
            if (userExists)
                return null;

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };

            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        // LOGIN
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return null;

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return GenerateToken(user);
        }

        // JWT GENERATION
        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("userid", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
