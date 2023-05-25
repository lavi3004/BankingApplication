using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTests.PageObjects;

public class HomePage
{
    private IWebDriver webDriver;

    [FindsBy(How = How.XPath, Using = "/html/body/header/nav/div/div/ul/li[4]")]
    private IWebElement addTransactionButton;

    [FindsBy(How = How.XPath, Using = "/html/body/header/nav/div/div/ul/li[5]")]
    private IWebElement addTransactionToUtilityButton;

    public HomePage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
        PageFactory.InitElements(webDriver, this);
    }

    [FindsBy(How = How.LinkText, Using = "Login")]
    private IWebElement loginButton;

    public LoginPage GoToLoginPage()
    {
        loginButton.Click();
        return new LoginPage(webDriver);
    }

    public void GoToPage()
    {
        webDriver.Navigate().GoToUrl("https://localhost:7252/");
    }

    public AddTransactionPage GoToAddTransactionPage()
    {
        this.addTransactionButton.Click();
       return new AddTransactionPage(webDriver);
    }

    public AddTransactionPage GoToAddTransactionToUtilityPage()
    {
        this.addTransactionToUtilityButton.Click();
        return new AddTransactionPage(webDriver);
    }
}

