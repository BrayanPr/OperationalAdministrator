using DB;
using DB.DTOs;
using DB.Models;
using OperationalAdministrator.Common;
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

            Team? n_team = null;

            n_team = _context.Teams.FirstOrDefault(t => t.Name == team.Name);

            if (n_team != null) throw new DuplicatedEntryException($"Team with name : {team.Name} already exist");

            n_team = new Team()
            {
                Name = team.Name,
                Description = team.Description,
            };
            _context.Teams.Add(n_team);
            try
            {
                _context.SaveChanges();
            
                return n_team;
            
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error while trying to create team");
            }

        }

        public bool deleteTeam(int id)
        {
            Team? team = _context.Teams.Find(id);

            if (team == null) throw new NotFoundException($"Team with the id : {id} not founded");

            try
            {
                _context.Teams.Remove(team);
                return (_context.SaveChanges() > 0);
            }
            catch(Exception e) { throw new ServerErrorException("Error while deleting team"); };
        }

        public Team? getTeam(int id) {
            Team? team = _context.Teams.Find(id);
            if (team == null) throw new NotFoundException($"Team with id : {id} not founded");
            return team;
        }

        public IEnumerable<Team> GetTeams() => _context.Teams.ToList();

        public bool replaceTeam(int id, TeamDTO team)
        {
            Team? existingTeam = _context.Teams.Find(id);

            if (existingTeam == null) throw new NotFoundException($"Team with id : {id} not founded");

            try
            {
                existingTeam.Name = team.Name;
                existingTeam.Description = team.Description;
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new ServerErrorException($"Error trying to replace team");
            }
        }
    }
}
