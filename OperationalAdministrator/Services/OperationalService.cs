using DB;
using DB.Models;
using OperationalAdministrator.Services.Interfaces;

namespace OperationalAdministrator.Services
{
    public class OperationalService : IOperationalService
    {

        private OperationalAdministratorContext _context;

        private IUserService userService;

        private ITeamService teamService;

        public OperationalService(IUserService _userService, ITeamService _teamService, OperationalAdministratorContext context) 
        { 
            _context = context;
            teamService = _teamService;
            userService = _userService;
        }
        public IEnumerable<History> GetHistories() => _context.TeamHistory.ToList();
        public IEnumerable<History> GetHistoriesByDates(DateTime startDate, DateTime endDate) => _context.TeamHistory.Where(x => x.date <= endDate && x.date >= startDate).ToList();
        public IEnumerable<History> GetHistoriesByTeam(int teamID) => _context.TeamHistory.Where(x=>x.NewTeam ==  teamID || x.OldTeam == teamID).ToList();
        public IEnumerable<History> GetHistoriesByUser(int userID) => _context.TeamHistory.Where(x=>x.UserId == userID).ToList();
        public History? MoveUser(int userID, int teamID)
        {
            User user = this.userService.getUser(userID);

            if (user == null || user.TeamId == teamID) return null;

            Team team = this.teamService.getTeam(teamID);

            if (team == null) return null;

            History history = new History()
            {
                NewTeam = teamID,
                OldTeam = user.TeamId,
                UserId = user.UserId,
            };

            user.TeamId = teamID;

            _context.TeamHistory.Add(history);

            return (_context.SaveChanges() > 0) ? history : null;
        }
    }
}
