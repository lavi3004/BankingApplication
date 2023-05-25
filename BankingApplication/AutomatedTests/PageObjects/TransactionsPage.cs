using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTests.PageObjects;

public class TransactionsPage
{

    private IWebDriver webDriver;

    [FindsBy(How = How.XPath, Using = "/html/body/div/main/table/tbody")]
    private IWebElement transactionsList;

    [FindsBy(How = How.XPath, Using = "/html/body/div/main/a")]
    private IWebElement addTransactionButton;

    public TransactionsPage(IWebDriver driver)
    {
        webDriver = driver;
        PageFactory.InitElements(driver, this);
    }

    public void GoToPage()
    {
        webDriver.Navigate().GoToUrl("https://localhost:7252/Transactions");
    }

    public AddTransactionPage GoToAddTransactionPage()
    {
        this.addTransactionButton.Click();
        return new AddTransactionPage(webDriver);
    }

    public bool TransactionExists(int ammount)
    {
        var elements = transactionsList.FindElements(By.TagName("tr"));
        return elements.Where(element => element.Text.Equals(ammount)).Count() > 0;

    }
}
