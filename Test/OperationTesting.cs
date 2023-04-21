using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;
using Moq;
using OperationalAdministrator.Services.Interfaces;

namespace Test
{
    public class OperationTesting
    {
        [Fact]
        public void MoveUser()
        {
            // Arrange
            Mock<ITeamService> teamService = new Mock<ITeamService>();

            Mock<IUserService> userService = new Mock<IUserService>();

            Mock<IOperationalService> operationService = new Mock<IOperationalService>();

            User user = new User()
            {
                UserId = 1,
                Name = "Test",
                Email = "test@mail.com",
                Password = "newPassword",
                role = "user",
                cv = "",
                englishLevel = "",
                experience = "",
                TeamId = null,
                Team = null
            };

            Team team1 = new Team()
            {
                Name = "Test",
                Description = "Test",
                TeamId = 1
            };

            Team team2 = new Team()
            {
                Name = "Test2",
                Description = "Test2",
                TeamId = 2
            };

            userService.Setup(x => x.getUser(1)).Returns(user);

            teamService.Setup(x => x.getTeam(1)).Returns(team1);

            teamService.Setup(x => x.getTeam(2)).Returns(team2);

            operationService.Setup(x => x.MoveUser(user.UserId, team1.TeamId)).Returns(new History()
            {
                NewTeam = team1.TeamId,
                OldTeam = user.TeamId,
                UserId = user.UserId,
            });

            
            // Act
            History res = operationService.Object.MoveUser(1,1);


            // Assert
            Assert.Equal(null, res.OldTeam);

            Assert.Equal(1, res.NewTeam);

            Assert.Equal(1, res.UserId);
        }
    }
}
