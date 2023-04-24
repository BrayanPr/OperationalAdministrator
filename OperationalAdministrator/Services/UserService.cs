using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DB;
using DB.DTOs;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OperationalAdministrator.Common;
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
            if( user.role != "super_admin") user.hashPassword();  // Hash the user's password
            return user;
        }
        public User createUser(UserDTO user)
        {

            User? _user = null;
            //validate uniqueness

            _user = _context.Users.Where( x => x.Name ==  user.Name ).FirstOrDefault();

            if(_user != null) throw new DuplicatedEntryException($"User with name: {user.Name} already registered");

            _user = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if (_user != null) throw new DuplicatedEntryException($"User with email: {user.Email} already registered");

            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                role = user.role,
                cv = user.cv,
                englishLevel = user.englishLevel,
                experience = user.experience,
            };

            newUser.hashPassword(); // Hash the user's password

            // Add the user to the context and save changes
            User nUser = _context.Users.Add(newUser).Entity;
                // Verify the query executes correctly
            _context.SaveChanges();
            return nUser;
        }
        public bool replaceUser(int id, UserUpdateDTO user)
        {
            User? existingUser = null;

            existingUser = _context.Users.Where(x => x.Name == user.Name).FirstOrDefault();

            if (existingUser != null && existingUser.UserId != id) throw new DuplicatedEntryException($"User with name: {existingUser.Name} already registered");

            existingUser = _context.Users.Find(id);

            if (existingUser == null) throw new NotFoundException($"User with id: {id} not found");

            // Update the existing user's properties with the new values
            existingUser.Name = user.Name;
            //existingUser.Password = user.Password;
            existingUser.cv = user.cv;
            existingUser.englishLevel = user.englishLevel;
            existingUser.experience = user.experience;
                
                if (existingUser.role != "super_admin")
                        existingUser.hashPassword(); // Hash the user's password

            // Save changes to the context
            try
            { 
                return _context.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                throw new ServerErrorException("Error while creating the user.");
            }

        }
        public bool deleteUser(int id)
        {
            User? userToDelete = _context.Users.Find(id);

            if (userToDelete == null) throw new NotFoundException($"User with id: {id} not found");

            try
            {
                // Remove the user from the context and save changes
                _context.Users.Remove(userToDelete);
                return _context.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                throw new ServerErrorException($"Error trying to delete with id : {id}");
            }
        }
        public AuthResponse Auth(AuthRequest model)
        {
            User? existingUser = _context.Users.Where(u => u.Email == model.email).FirstOrDefault();

            if (existingUser == null || !existingUser.verifyPassword(model.password)) throw new BadRequestException($"Wrong credentials");

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
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signing
                );

            AuthResponse response = new AuthResponse()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = existingUser.role
            };

            return response;
        }

    }
}
