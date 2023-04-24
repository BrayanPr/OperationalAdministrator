using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.DTOs;
using DB.Models;
using Moq;
using OperationalAdministrator.Services;
using OperationalAdministrator.Services.Interfaces;

namespace Test
{
    public class TeamTesting
    {
        [Fact]
        public void CreatingTeam()
        {
            TeamDTO team = new TeamDTO()
            {
                Name = "Test",
                Description = "Test"
            };

            Team team1 = new Team()
            {
                TeamId = 1,
                Name = "Test",
                Description = "Test"
            };

            Mock<ITeamService> teamService = new Mock<ITeamService>();

            teamService.Setup(x => x.createTeam(team)).Returns(team1);

            Team team2 = teamService.Object.createTeam(team);

            Assert.NotNull(team2);

            Assert.Equal(team1.Name, team2.Name);

            Assert.Equal(team1.Description, team2.Description);

        }

        [Fact]
        public void GetAllTeams()
        {
            IEnumerable<Team> teams = new List<Team>{new Team() { Name = "Test", Description = "Test" }, new Team() { Name = "Test1", Description = "Test1" } };

            Mock<ITeamService> teamService = new Mock<ITeamService>();

            teamService.Setup(x => x.GetTeams()).Returns(teams);

            IEnumerable<Team> res = teamService.Object.GetTeams();

            Assert.NotNull(res);

            Assert.Equal(teams.Count(), res.Count());

            Assert.Equal(teams.First(), res.First());
        }
        [Fact]
        public void GetTeam()
        {
            Team team = new Team() {TeamId = 1, Name = "Test", Description = "Test" };

            Mock<ITeamService> teamService = new Mock<ITeamService>();

            teamService.Setup(x => x.getTeam(1)).Returns(team);

            Team res = teamService.Object.getTeam(1);

            Assert.NotNull(res);

            Assert.Equal(team, res);
        }
        [Fact]
        public void GetTeamWhenNull() 
        {
            Mock<ITeamService> teamService = new Mock<ITeamService>();

            teamService.Setup(x => x.getTeam(It.IsAny<int>())).Returns(()=>null);

            Team res = teamService.Object.getTeam(1);

            Assert.Null(res);
        }
    }
}
