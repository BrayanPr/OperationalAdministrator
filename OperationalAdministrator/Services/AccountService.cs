using DB;
using DB.DTOs;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Common;
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
        public Account getAccount(int id)
        {
            Account? account = _context.Accounts.Find(id);

            if (account == null) throw new NotFoundException($"Account with id : {id} not founded");

            return account;
        }
        public Account createAccount(AccountDTO account)
        {
            Account? existingAccount = _context.Accounts.Where(x => x.AccountName == account.AccountName).FirstOrDefault();

            if (existingAccount != null) throw new DuplicatedEntryException($"Account with the name : {account.AccountName} already exists");

            // Add the user to the context and save changes
            Account newAccount = new Account()
            { 
                AccountName = account.AccountName,
                OperationManagerName = account.OperationManagerName,
                TeamId = account.TeamId,
                CustomerName = account.CustomerName,
            };
            try
            {
                _context.Accounts.Add(newAccount);
                _context.SaveChanges();
                return newAccount;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error whlie creating account");
            }
        }

        public bool deleteAccount(int id)
        {
            Account? account = _context.Accounts.FirstOrDefault(x => x.AccountId == id);

            if (account == null) throw new NotFoundException($"Account with id : {id} not founded");

            try
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw new ServerErrorException($"Error while deleting account with id : {id}"); }
            
        }

        public bool replaceAccount(int id, AccountDTO account)
        {
            Account? existing_account = _context.Accounts.FirstOrDefault( x => x.AccountId == id);

            if (existing_account == null) throw new NotFoundException($"Account with id : {id} not founded");

            try
            {
                existing_account.CustomerName = account.CustomerName;
                existing_account.AccountName = account.AccountName;
                existing_account.OperationManagerName = account.OperationManagerName;
                existing_account.TeamId = account.TeamId;
                _context.SaveChanges();
                return false;
            } catch (Exception ex)
            {
                throw new ServerErrorException($"Error while replacing account with id : {id}");
            }

            
        }

    }
}
