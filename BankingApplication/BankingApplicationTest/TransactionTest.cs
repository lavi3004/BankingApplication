using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services;
using BankingApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;

namespace BankingApplicationTest
{
    [TestClass]
    public class TransactionTest
    {
        private TransactionService transactionService;
        private BankAccountService bankAccountService;
        private Mock<IRepositoryWrapper> repositoryWrapper;

        [TestInitialize]
        public void Setup()
        {
            repositoryWrapper= new Mock<IRepositoryWrapper>();

            transactionService= new TransactionService(repositoryWrapper.Object);

            bankAccountService= new BankAccountService(repositoryWrapper.Object);
        }

        [TestMethod]
        public void GetTransactions_Returns_ListOfTransactions()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);

            Transaction t1 = new Transaction(1, 100, DateTime.Now, 1, 2);
            Transaction t2 = new Transaction(2, 100, DateTime.Now, 1, 2);

            t1.Sender = account;
            t2.Sender = account;

            List<Transaction> transactions = new List<Transaction> { t1, t2 };

            repositoryWrapper.Setup(r => r.TransactionRepository.FindAll()).Returns(transactions.AsQueryable());

            var result = transactionService.GetTransactions(user.Id);

            Assert.IsTrue(result.SequenceEqual(transactions));
        }


        [TestMethod]
        public void FindTransaction_True()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);

            Transaction t1 = new Transaction(1, 100, DateTime.Now, 1, 2);

            repositoryWrapper.Setup(r => r.TransactionRepository.FindByCondition(x=>x.Id== 1))
                .Returns(new List<Transaction> { t1 }.AsQueryable());

            var result = transactionService.GetTransactionById(t1.Id);

            Assert.AreEqual(t1, result);
        }

        [TestMethod]
        public void FindTransaction_False()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);

            Transaction t1 = new Transaction(1, 100, DateTime.Now, 1, 2);

            repositoryWrapper.Setup(r => r.TransactionRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Transaction> { }.AsQueryable());

            var result = transactionService.GetTransactionById(t1.Id);

            Assert.AreNotEqual(t1, result);
        }

        [TestMethod]
        public void Update_Transaction()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);

            Transaction t1 = new Transaction(1, 100, DateTime.Now, 1, 2);

            repositoryWrapper.Setup(r => r.TransactionRepository.Update(It.IsAny<Transaction>()))
                .Verifiable();

            repositoryWrapper.Setup(r => r.TransactionRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Transaction> { t1 }.AsQueryable());

            transactionService.Update(t1);

            var result = transactionService.GetTransactionById(t1.Id);

            Assert.AreEqual(t1, result);
        }

        [TestMethod]
        public void Delete_Transaction()
        {
            IdentityUser user = new IdentityUser("user");

            BankAccount account = new BankAccount(1, "acc1", user);

            Transaction t1 = new Transaction(1, 100, DateTime.Now, 1, 2);

            repositoryWrapper.Setup(r => r.TransactionRepository.Delete(It.IsAny<Transaction>()));

            repositoryWrapper.Setup(r => r.TransactionRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Transaction> { }.AsQueryable());

            transactionService.Delete(t1.Id);

            var result = transactionService.GetTransactionById(t1.Id);

            Assert.AreEqual(null, result);
        }


        //[TestMethod]
        //public void Make_A_Payment()
        //{
        //    IdentityUser user = new IdentityUser("user");

        //    BankAccount account1 = new BankAccount(1, "acc1", 100, user);
        //    BankAccount account2 = new BankAccount(2, "acc2",100, user);

        //    int ammount = 100;

        //    Transaction t1 = new Transaction(1, 100, DateTime.Now, 1, 2);

        //    repositoryWrapper.Setup(r => r.BankAccountRepository.FindByCondition(x => x.Id == 1))
        //        .Returns(new List<BankAccount> { account1 }.AsQueryable());

        //    repositoryWrapper.Setup(r => r.BankAccountRepository.FindByCondition(x => x.Id == 2))
        //      .Returns(new List<BankAccount> { account2 }.AsQueryable());

        //    repositoryWrapper.Setup(r => r.BankAccountRepository.Update(It.IsAny<BankAccount>()))
        //         .Verifiable();

        //    transactionService.PerformTransaction(account1.Id, account2.Id, ammount);

        //    var result1= bankAccountService.GetBankAccountById(account1.Id);
        //    var result2= bankAccountService.GetBankAccountById(account2.Id);


        //     Assert.AreEqual(result1.Balance, account1.Balance-ammount);
        //}


    }
}