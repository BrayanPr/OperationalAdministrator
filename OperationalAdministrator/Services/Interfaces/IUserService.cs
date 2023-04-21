using DB.DTOs;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Models;

namespace OperationalAdministrator.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();

        public User getUser(int id);

        public User createUser(UserDTO user);

        public bool replaceUser(int id, UserUpdateDTO user);

        public bool deleteUser(int id);

        public AuthResponse? Auth(AuthRequest model);

    }
}
