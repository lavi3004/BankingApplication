using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTests.PageObjects;

public class AddBankAccountPage
{
    private IWebDriver webDriver;

    [FindsBy(How = How.Id, Using = "Name")]
    private IWebElement Name;

    [FindsBy(How = How.Id, Using = "IBAN")]
    private IWebElement IBAN;

    [FindsBy(How = How.Id, Using = "Balance")]
    private IWebElement Balance;

    [FindsBy(How = How.Id, Using = "Currency")]
    private IWebElement Currency;

    [FindsBy(How = How.XPath, Using = "/html/body/div/main/a")]
    private IWebElement saveButton;

    public AddBankAccountPage(IWebDriver driver)
    {
        this.webDriver = driver;
        PageFactory.InitElements(driver, this);
    }

    public void Save(string name, string IBAN, string balance, string currency)
    {
        this.Name.Clear();
        this.Name.SendKeys(name);

        this.IBAN.Clear();
        this.IBAN.SendKeys(IBAN);

        this.Balance.Clear();
        this.Balance.SendKeys(balance);

       this.Currency.Clear();
        this.Currency.SendKeys(currency);

        this.saveButton.Click();
    }

}
