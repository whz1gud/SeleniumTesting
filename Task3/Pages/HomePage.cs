using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Task3.Pages;

public class HomePage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _loginItem = By.XPath("//a[@href='/login']");

    public HomePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public LoginPage GoToLoginPage()
    {
        var loginItem = Driver.FindElement(_loginItem);
        loginItem.Click();
        return new LoginPage(Driver, Wait);
    }
}