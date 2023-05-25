using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTests.PageObjects;

public class BankAccountsPage
{
    private IWebDriver webDriver;

    [FindsBy(How = How.XPath, Using = "/html/body/div/main/table/tbody")]
    private IWebElement bankAccountsPage;

    [FindsBy(How = How.XPath, Using = "/html/body/div/main/a")]
    private IWebElement addBankAccountButton;

    public BankAccountsPage(IWebDriver driver)
    {
        webDriver = driver;
        PageFactory.InitElements(driver, this);
    }

    public void GoToPage()
    {
        webDriver.Navigate().GoToUrl("https://localhost:7252/BankAccounts");
    }

    public AddBankAccountPage GoToAddBankAccountPage()
    {
        this.addBankAccountButton.Click();
        return new AddBankAccountPage(webDriver);
    }

    public bool BankAccountExists(string name)
    {
        var elements = bankAccountsPage.FindElements(By.TagName("tr"));
        return elements.Where(element => element.Text.Equals(name)).Count() > 0;

    }
}
