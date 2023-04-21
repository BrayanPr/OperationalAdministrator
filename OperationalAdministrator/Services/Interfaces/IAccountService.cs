using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Models;

namespace OperationalAdministrator.Services.Interfaces
{
    public interface IAccountService
    {
        public IEnumerable<Account> GetAccounts();

        public Account? getAccount(int id);

        public Account? createAccount(AccountDTO account);

        public bool replaceAccount(int id, AccountDTO account);

        public bool deleteAccount(int id);
    }
}
