using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTests.PageObjects;
using System.Diagnostics;

namespace AutomatedTests;

[TestClass]

public class TransactionsTest
{
    private IWebDriver webDriver;

    [TestInitialize]
    public void InitTests()
    {
        webDriver = new ChromeDriver();
    }

    [TestMethod]
    public void AddTransaction_FromNav_Creates_TransactionWithGivenInfo()
    {
        int ammount = 100;
        int senderId = 1;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);

        AddTransactionPage addTransactionPage = homePage.GoToAddTransactionPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsTrue(transactionsPage.TransactionExists(ammount));
    }

    [TestMethod]
    public void AddTransactionToUtility_FromNav_Creates_TransactionWithGivenInfo()
    {
        int ammount = 100;
        int senderId = 1;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);

        AddTransactionPage addTransactionPage = homePage.GoToAddTransactionToUtilityPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsTrue(transactionsPage.TransactionExists(ammount));
    }

    [TestMethod]
    public void AddTransactionToUtility_FromNav_DoesNotCreate_TransactionWhenAmountIsNotGiven()
    {
        int? ammount = null;
        int senderId = 1;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);

        AddTransactionPage addTransactionPage = homePage.GoToAddTransactionToUtilityPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(senderId));
    }

    [TestMethod]
    public void AddTransactionToUtility_FromNav_DoesNotCreate_TransactionWhenSenderIdIsNotGiven()
    {
        int ammount = 100;
        int? senderId = null;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);

        AddTransactionPage addTransactionPage = homePage.GoToAddTransactionToUtilityPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(ammount));
    }

    [TestMethod]
    public void AddTransactionToUtility_FromNav_DoesNotCreate_TransactionWhenReciverIdIsNotGiven()
    {
        int ammount = 100;
        int senderId = 1;
        int? reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);

        AddTransactionPage addTransactionPage = homePage.GoToAddTransactionToUtilityPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(ammount));
    }


    [TestMethod]
    public void AddTransaction_Creates_TransactionWithGivenInfo()
    {
        int ammount = 100;
        int senderId = 1;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);
        transactionsPage.GoToPage();

        AddTransactionPage addTransactionPage = transactionsPage.GoToAddTransactionPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsTrue(transactionsPage.TransactionExists(ammount));
    }

    [TestMethod]
    public void AddTransaction_DoesNotCreate_WhenAmmountIsNotGiven()
    {
        int? ammount = null;
        int senderId = 1;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);
        transactionsPage.GoToPage();

        AddTransactionPage addTransactionPage = transactionsPage.GoToAddTransactionPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(senderId));
    }

    [TestMethod]
    public void AddTransaction_DoesNotCreate_WhenSenderIdIsNotGiven()
    {
        int ammount = 100;
        int? senderId = null;
        int reciverId = 2;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);
        transactionsPage.GoToPage();

        AddTransactionPage addTransactionPage = transactionsPage.GoToAddTransactionPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(ammount));
    }

    [TestMethod]
    public void AddTransaction_DoesNotCreate_WhenReciverIdIsNotGiven()
    {
        int ammount = 100;
        int senderId = 1;
        int? reciverId = null;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);
        transactionsPage.GoToPage();

        AddTransactionPage addTransactionPage = transactionsPage.GoToAddTransactionPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(ammount));
    }

    [TestMethod]
    public void AddTransaction_DoesNotCreate_WhenNoInfoGiven()
    {
        int ammount = 100;
        int? senderId = null;
        int? reciverId = null;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        TransactionsPage transactionsPage = new TransactionsPage(webDriver);
        transactionsPage.GoToPage();

        AddTransactionPage addTransactionPage = transactionsPage.GoToAddTransactionPage();
        addTransactionPage.Save(ammount, senderId, reciverId);

        Assert.IsFalse(transactionsPage.TransactionExists(ammount));
    }


    [TestCleanup]
    public void Cleanup()
    {
        webDriver.Close();
    }
}
