using DB.Models;

namespace OperationalAdministrator.Services.Interfaces
{
    public interface IOperationalService
    {
        public History MoveUser(int userID, int teamID);

        public IEnumerable<History> GetHistories();

        public IEnumerable<History> GetHistoriesByUser(int userID);

        public IEnumerable<History> GetHistoriesByTeam(int teamID);

        public IEnumerable<History> GetHistoriesByUserName(string userName);

        public IEnumerable<History> GetHistoriesByDates(DateTime startDate, DateTime endDate);

    }
}
