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
        public Account? getAccount(int id) => _context.Accounts.FirstOrDefault( a => a.AccountId == id);

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
            Account account = _context.Accounts.FirstOrDefault(x => x.AccountId == id);

            if (account != null)
            {
                _context.Accounts.Remove(account);
                return _context.SaveChanges() > 0;
            }
            return false; 
        }

        public bool replaceAccount(int id, AccountDTO account)
        {
            Account existing_account = _context.Accounts.FirstOrDefault( x => x.AccountId == id);

            if (existing_account != null)
            {
                existing_account.CustomerName = account.CustomerName;
                existing_account.AccountName = account.AccountName;
                existing_account.OperationManagerName = account.OperationManagerName;
                existing_account.TeamId = account.TeamId;
                return (_context.SaveChanges() > 0);
            }
            return false;
        }

    }
}
