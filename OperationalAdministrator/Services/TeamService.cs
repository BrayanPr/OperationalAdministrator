using DB;
using DB.Models;
using DB.Models.DTOs;
using OperationalAdministrator.Services.Interfaces;

namespace OperationalAdministrator.Services
{
    public class TeamService : ITeamService
    {
        OperationalAdministratorContext _context;
        public TeamService(OperationalAdministratorContext context) {
            _context = context;
        }

        public Team? createTeam(TeamDTO team)
        {
            Team n_team = new Team()
            {
                Name = team.Name,
                Description = team.Description,
            };
            _context.Teams.Add(n_team);

            if (_context.SaveChanges() > 0)
            {
                return n_team;
            }
            return null;

        }

        public bool deleteTeam(int id)
        {
            Team team = _context.Teams.Where(x => x.TeamId == id).FirstOrDefault();

            if (team != null)
            {
                _context.Teams.Remove(team);
                return (_context.SaveChanges() > 0);
            }
            return false;
        }

        public Team? getTeam(int id) => _context.Teams.Where(x => x.TeamId == id).FirstOrDefault();

        public IEnumerable<Team> GetTeams() => _context.Teams.ToList();

        public bool replaceTeam(int id, TeamDTO team)
        {
            Team existingTeam = _context.Teams.Where(x => x.TeamId == id).FirstOrDefault();
            if (existingTeam != null)
            {
                existingTeam.Name = team.Name;
                existingTeam.Description = team.Description;
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
