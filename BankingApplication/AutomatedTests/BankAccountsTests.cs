using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTests.PageObjects;

namespace AutomatedTests;

public class BankAccountsTests
{

    private IWebDriver webDriver;

    [TestInitialize]
    public void InitTests()
    {
        webDriver = new ChromeDriver();
    }

    [TestMethod]
    public void AddBankAccount_Creates_BankAccountWithGivenInfo()
    {
        string name = "acc1";
        string IBAN = "123";
        string balance = "100";
        string currency = "RON";

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        BankAccountsPage bankAccountsPage = new BankAccountsPage(webDriver);
        bankAccountsPage.GoToPage();

        AddBankAccountPage addBankAccountPage = bankAccountsPage.GoToAddBankAccountPage();
        addBankAccountPage.Save(name, IBAN, balance, currency);

        Assert.IsTrue(bankAccountsPage.BankAccountExists(name));
    }

    [TestMethod]
    public void AddBankAccount_DoesNotCreate_BankAccount_WhenNotBeingGivenName()
    {
        string name = null;
        string IBAN = "123";
        string balance = "100";
        string currency = "RON";

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        BankAccountsPage bankAccountsPage = new BankAccountsPage(webDriver);
        bankAccountsPage.GoToPage();

        AddBankAccountPage addBankAccountPage = bankAccountsPage.GoToAddBankAccountPage();
        addBankAccountPage.Save(name, IBAN, balance, currency);

        Assert.IsFalse(bankAccountsPage.BankAccountExists(name));
    }

    [TestMethod]
    public void AddBankAccount_DoesNotCreate_BankAccount_WhenNotBeingGivenIBAN()
    {
        string name = "name";
        string IBAN = null;
        string balance = "100";
        string currency = "RON";

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        BankAccountsPage bankAccountsPage = new BankAccountsPage(webDriver);
        bankAccountsPage.GoToPage();

        AddBankAccountPage addBankAccountPage = bankAccountsPage.GoToAddBankAccountPage();
        addBankAccountPage.Save(name, IBAN, balance, currency);

        Assert.IsFalse(bankAccountsPage.BankAccountExists(name));
    }

    [TestMethod]
    public void AddBankAccount_DoesNotCreate_BankAccount_WhenNotBeingGivenBalance()
    {
        string name = "name";
        string IBAN = "123";
        string balance = null;
        string currency = "RON";

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        BankAccountsPage bankAccountsPage = new BankAccountsPage(webDriver);
        bankAccountsPage.GoToPage();

        AddBankAccountPage addBankAccountPage = bankAccountsPage.GoToAddBankAccountPage();
        addBankAccountPage.Save(name, IBAN, balance, currency);

        Assert.IsFalse(bankAccountsPage.BankAccountExists(name));
    }

    [TestMethod]
    public void AddBankAccount_DoesNotCreate_BankAccount_WhenNotBeingGivenCurrency()
    {
        string name = "name";
        string IBAN = "123";
        string balance = "100";
        string currency = null;

        HomePage homePage = new HomePage(webDriver);
        homePage.GoToPage();

        LoginPage loginPage = homePage.GoToLoginPage();
        loginPage.Login("admin@gmail.com", "Admin1!");

        BankAccountsPage bankAccountsPage = new BankAccountsPage(webDriver);
        bankAccountsPage.GoToPage();

        AddBankAccountPage addBankAccountPage = bankAccountsPage.GoToAddBankAccountPage();
        addBankAccountPage.Save(name, IBAN, balance, currency);

        Assert.IsFalse(bankAccountsPage.BankAccountExists(name));
    }

    [TestCleanup]
    public void Cleanup()
    {
        webDriver.Close();
    }
}
