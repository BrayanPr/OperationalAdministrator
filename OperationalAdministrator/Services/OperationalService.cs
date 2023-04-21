using System.Reflection.Metadata.Ecma335;
using DB;
using DB.Models;
using OperationalAdministrator.Common;
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
        public IEnumerable<History> GetHistoriesByTeam(int teamID) => _context.TeamHistory.Where(x => x.NewTeam == teamID || x.OldTeam == teamID).ToList();
        public IEnumerable<History> GetHistoriesByUser(int userID) => _context.TeamHistory.Where(x => x.UserId == userID).ToList();
        public IEnumerable<History> GetHistoriesByUserName(string userName) 
        {
            User? user = _context.Users.Where(x=>x.Name.StartsWith(userName)).FirstOrDefault();

            if (user == null) throw new NotFoundException($"User with name : {userName} not founded");

            return _context.TeamHistory.Where(x=>x.UserId == user.UserId).ToList();
        }
        public History MoveUser(int userID, int teamID)
        {
            User user = this.userService.getUser(userID);

            if (user == null) throw new NotFoundException($"User with id : {userID} not founded");

            Team team = this.teamService.getTeam(teamID);

            if (team == null) throw new NotFoundException($"Team with id : {teamID} not founded");

            if (user.TeamId == teamID) throw new BadRequestException($"User with id : {userID} is already the  team with id : {teamID}");

            try
            {
                History history = new History()
                {
                    NewTeam = teamID,
                    OldTeam = user.TeamId,
                    UserId = user.UserId,
                };

                user.TeamId = teamID;

                _context.TeamHistory.Add(history);

                _context.SaveChanges();

                return history;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException($"Error while trying to move user with id : {userID} to team with id : {teamID}");
            }

            
        }
    }
}
