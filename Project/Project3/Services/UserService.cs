using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Sections;
using Microsoft.IdentityModel.Tokens;
using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Helpers;
using Project3.Models;

namespace Project3.Services;

public class UserService : IUserService
{
     private readonly LocalDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(LocalDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task RegisterStudent(RegisterRequest model)
        {
            var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

            var user = new User()
            {
                Email = model.Email,
                Login = model.Login,
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(1)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var role = _context.Roles.SingleOrDefault(r => r.Name == model.Role);
            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            var userRole = new User_Role()
            {
                UserId = user.UserId,
                RoleId = role.RoleId
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<string[]> Login(LoginRequest loginRequest)
        {
            var user = _context.Users.SingleOrDefault(u => u.Login == loginRequest.Login);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid login");
            }

            string passwordHashFromDb = user.Password;
            string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

            if (passwordHashFromDb != curHashedPassword)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            var tokens = GenerateTokens(user);

            user.RefreshToken = tokens[1];
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return tokens;
        }

        private string[] GenerateTokens(User user)
        {
            var userRoles = _context.UserRoles
                .Where(ur => ur.UserId == user.UserId)
                .Select(ur => ur.Role.Name)
                .ToList();
            
            Console.WriteLine($"User {user.Login} has the following roles: {string.Join(", ", userRoles)}");
        
            var userclaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login)
            };
            userclaim.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["ValidIssuer"],
                audience: _configuration["ValidAudience"],
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            
            Console.WriteLine($"User {user.Login} has the following roles: {string.Join(", ", userclaim)}");
            
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = SecurityHelpers.GenerateRefreshToken();

            return new[] { accessToken, refreshToken };
        }
}