using System.IdentityModel.Tokens.Jwt;
using DB;
using DB.Models;
using DB.Models.DTOs;
using Moq;
using OperationalAdministrator.Common;
using OperationalAdministrator.Controllers;
using OperationalAdministrator.Models;
using OperationalAdministrator.Services.Interfaces;

namespace Test
{
    public class UserTesting
    {
        [Fact]
        public void CreatingUser()
        {

            UserDTO userDTO = new UserDTO()
            {
                Name = "Test",
                Email = "test@demo.com",
                Password = "newPassword1.",
                cv="",
                englishLevel="",
                experience="",
                role="admin"
            };

            User demoUser = new User()
            {
                UserId = 1,
                Name = "Test",
                Email = "test@demo.com",
                Password = "newPassword1.",
                cv = "",
                englishLevel = "",
                experience = "",
                role = "admin",
                TeamId = null,
                Team = null
            };

            demoUser.hashPassword();

            Mock<IUserService> service = new Mock<IUserService>();

            service.Setup(x => x.createUser(userDTO)).Returns(demoUser);

            var res = service.Object.createUser(userDTO);

            Assert.NotNull(res);

            Assert.Equal(demoUser.Password,res.Password);
        }
        [Fact]
        public void GettingUser()
        {
            User demoUser = new User()
            {
                UserId = 1,
                Name = "Test",
                Email = "test@demo.com",
                Password = "newPassword1.",
                cv = "",
                englishLevel = "",
                experience = "",
                role = "admin",
                TeamId = null,
                Team = null
            };

            demoUser.hashPassword();

            Mock<IUserService> service = new Mock<IUserService>();

            service.Setup(x => x.getUser(demoUser.UserId)).Returns(demoUser);

            var res = service.Object.getUser(1);

            Assert.NotNull(res);

            Assert.Equal("Test", res.Name);

        }
        [Fact]
        public void GettingUserWhenNull()
        {

            Mock<IUserService> service = new Mock<IUserService>();

            service.Setup(x => x.getUser(It.IsAny<int>())).Returns(()=>null);

            var res = service.Object.getUser(1);

            Assert.Null(res);
        }
    }
}