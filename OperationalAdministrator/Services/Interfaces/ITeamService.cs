using DB.Models;
using DB.DTOs;

namespace OperationalAdministrator.Services.Interfaces
{
    public interface ITeamService
    {
        public IEnumerable<Team> GetTeams();

        public Team getTeam(int id);

        public Team createTeam(TeamDTO team);

        public bool replaceTeam(int id, TeamDTO team);

        public bool deleteTeam(int id);
    }
}
