using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DB;
using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OperationalAdministrator.Models;
using OperationalAdministrator.Services.Interfaces;

namespace OperationalAdministrator.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private OperationalAdministratorContext _context;
        public UserService(OperationalAdministratorContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IEnumerable<User> GetUsers() => _context.Users.ToList();
        public User? getUser(int id)
        {
            // Set the properties of the user instance
            User user = _context.Users.Find(id);
            if (user == null) return null;
            user.hashPassword();  // Hash the user's password

            return user;
        }
        public User? createUser(UserDTO user)
        {
            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                role = user.role,
            };

            newUser.hashPassword(); // Hash the user's password

            // Add the user to the context and save changes
            User nUser = _context.Users.Add(newUser).Entity;

            // Verify the query executes correctly
            if(_context.SaveChanges() > 0){
                return nUser;
            }

            //else return null
            return null;
        }
        public bool replaceUser(int id, UserDTO user)
        {
            User existingUser = _context.Users.Find(id);

            if (existingUser != null)
            {
                // Update the existing user's properties with the new values
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
                existingUser.hashPassword(); // Hash the user's password

                // Save changes to the context
                return _context.SaveChanges() > 0;
            }

            return false;
        }
        public bool deleteUser(int id)
        {
            User userToDelete = _context.Users.Find(id);

            if (userToDelete != null)
            {
                // Remove the user from the context and save changes
                _context.Users.Remove(userToDelete);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
        public AuthResponse? Auth(AuthRequest model)
        {
            User? existingUser = _context.Users.Where(u => u.Email == model.email).FirstOrDefault();

            if (existingUser == null || !existingUser.verifyPassword(model.password))
            {
                return null;
            }
            var jwt = _configuration.GetSection("JWT").Get<Common.JWT>();

            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", existingUser.UserId.ToString()),
                new Claim("Role", existingUser.role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));

            SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: signing
                );

            AuthResponse response = new AuthResponse()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return response;
        }

    }
}
