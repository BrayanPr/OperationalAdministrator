using DB;
using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Services.Interfaces;

namespace OperationalAdministrator.Services
{
    public class AccountService : IAccountService
    {
        private OperationalAdministratorContext _context;
        public AccountService(OperationalAdministratorContext context) 
        {
            _context = context;
        }
        public IEnumerable<Account> GetAccounts () => _context.Accounts.ToList();
        public Account? getAccount(int id) => _context.Accounts.FirstOrDefault( a => a.TeamId == id);

        public Account? createAccount(AccountDTO account)
        {
            // Add the user to the context and save changes
            Account newAccount = new Account()
            { 
                AccountName = account.AccountName,
                OperationManagerName = account.OperationManagerName,
                TeamId = account.TeamId,
                CustomerName = account.CustomerName,
            };

            Account nAccount = _context.Accounts.Add(newAccount).Entity;

            // Verify the query executes correctly
            if (_context.SaveChanges() > 0)
            {
                return nAccount;
            }

            //else return null
            return null;
        }

        public bool deleteAccount(int id)
        {
            throw new NotImplementedException();
        }



        public bool replaceAccount(int id, Account account)
        {
            throw new NotImplementedException();
        }

    }
}
