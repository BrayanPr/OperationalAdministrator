using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Models;

namespace OperationalAdministrator.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();

        public User? getUser(int id);

        public User? createUser(UserDTO user);

        public bool replaceUser(int id, UserDTO user);

        public bool deleteUser(int id);

        public AuthResponse? Auth(AuthRequest model);
    }
}
