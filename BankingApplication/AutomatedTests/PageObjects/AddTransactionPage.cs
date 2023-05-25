using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomatedTests.PageObjects;

public class AddTransactionPage
{
    private IWebDriver webDriver;

    [FindsBy(How = How.Id, Using = "Amount")]
    private IWebElement Amount;

    [FindsBy(How = How.Id, Using = "SenderId")]
    private IWebElement SenderId;

    [FindsBy(How = How.Id, Using = "ReciverId")]
    private IWebElement ReciverId;

    [FindsBy(How = How.XPath, Using = "/html/body/div/main/div[2]/div/form/div[4]/input")]
    private IWebElement saveButton;

    public AddTransactionPage(IWebDriver driver)
    {
        this.webDriver = driver;
        PageFactory.InitElements(driver, this);
    }


    public void Save(int? Amount, int? SenderId, int? ReciverId)
    {
        this.Amount.Clear();
        this.Amount.SendKeys(Amount.ToString());

        this.SenderId.Clear();
        this.SenderId.SendKeys(SenderId.ToString());

        this.ReciverId.Clear();
        this.ReciverId.SendKeys(ReciverId.ToString());

        saveButton.Click();
    }




}
