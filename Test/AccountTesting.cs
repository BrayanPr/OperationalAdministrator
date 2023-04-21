using DB;
using DB.DTOs;
using DB.Models;
using DB.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Moq;
using OperationalAdministrator.Services;
using OperationalAdministrator.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Xunit;

namespace Test
{
    public class AccountTesting
    {


        [Fact]
        public void CreatingAccount()
        {
            // Arrange
            AccountDTO account = new AccountDTO()
            {
                AccountName = "Test",
                OperationManagerName = "Test",
                CustomerName = "Test",
            };

            Account newAccount = new Account()
            {
                AccountId = 1,
                AccountName = "Test",
                OperationManagerName = "Test",
                TeamId = 1,
                CustomerName = "Test",
            };

            Mock<IAccountService> service = new Mock<IAccountService>();

            service.Setup(x => x.createAccount(account)).Returns(newAccount);

            // Act
            Account createdAccount = service.Object.createAccount(account);

            // Assert
            Assert.NotNull(createdAccount);
            Assert.Equal(newAccount.AccountName, createdAccount.AccountName);
            Assert.Equal(newAccount.OperationManagerName, createdAccount.OperationManagerName);
            Assert.Equal(newAccount.TeamId, createdAccount.TeamId);
            Assert.Equal(newAccount.CustomerName, createdAccount.CustomerName);
        }

        [Fact]
        public void GetAllAccounts()
        {
            // Arrange
            IEnumerable<Account> accounts = new List<Account> { new Account() { AccountName = "Test", OperationManagerName = "Test", TeamId = 1, CustomerName = "Test" }, new Account() { AccountName = "Test1", OperationManagerName = "Test1", TeamId = 2, CustomerName = "Test1" } };

            Mock<IAccountService> service = new Mock<IAccountService>();

            service.Setup(x => x.GetAccounts()).Returns(accounts);

            // Act
            IEnumerable<Account> res = service.Object.GetAccounts();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(accounts.Count(), res.Count());
            Assert.Equal(accounts.First(), res.First());
        }

        [Fact]
        public void GetAccount()
        {
            // Arrange
            Account account = new Account() { AccountId = 1, AccountName = "Test", OperationManagerName = "Test", TeamId = 1, CustomerName = "Test" };

            Mock<IAccountService> service = new Mock<IAccountService>();

            service.Setup(x => x.getAccount(It.IsAny<int>())).Returns(account);


            // Act
            Account res = service.Object.getAccount(1);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(account, res);
        }

        [Fact]
        public void GetAccountWhenNull()
        {
            // Arrange
            Mock<IAccountService> service = new Mock<IAccountService>();

            service.Setup(x => x.getAccount(It.IsAny<int>())).Returns(()=>null);

            // Act
            Account res = service.Object.getAccount(1);

            // Assert
            Assert.Null(res);
        }
    }
}