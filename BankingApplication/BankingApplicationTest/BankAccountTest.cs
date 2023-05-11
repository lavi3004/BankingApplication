using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services;
using BankingApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankingApplicationTest
{
    
    [TestClass]
    public  class BankAccountTest
    {
        private BankAccountService bankAccountService;
        private Mock<IRepositoryWrapper> repositoryWrapper;

        [TestInitialize]
        public void Setup()
        {
            repositoryWrapper = new Mock<IRepositoryWrapper>();

            bankAccountService = new BankAccountService(repositoryWrapper.Object);
        }

        [TestMethod]
        public void GetBankAccounts_Returns_ListOfBankAccounts()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account1 = new BankAccount(1, "acc1", user);
            BankAccount account2 = new BankAccount(1, "acc1", user);


            List<BankAccount> bankAccounts = new List<BankAccount> { account1, account2 };

            repositoryWrapper.Setup(r => r.BankAccountRepository.FindAll()).Returns(bankAccounts.AsQueryable());

            var result = bankAccountService.GetBankAccountsOfUser(user.Id);

            Assert.IsTrue(result.SequenceEqual(bankAccounts));
        }


        [TestMethod]
        public void FindTransaction_True()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);           

            repositoryWrapper.Setup(r => r.BankAccountRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<BankAccount> { account }.AsQueryable());

            var result = bankAccountService.GetBankAccountById(account.Id);

            Assert.AreEqual(account, result);
        }

        [TestMethod]
        public void FindTransaction_False()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);

            repositoryWrapper.Setup(r => r.BankAccountRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<BankAccount> { }.AsQueryable());

            var result = bankAccountService.GetBankAccountById(account.Id);

            Assert.AreNotEqual(account, result);
        }
    }
}
