using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Task4.Config;

namespace Task4.Pages;

public class LoginPage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _emailInput = By.XPath("//input[@id='Email']");
    private By _passwordInput = By.XPath("//input[@id='Password']");
    private By _loginButton = By.XPath("//input[@class='button-1 login-button']");
    private By _logoutButton = By.XPath("//a[@href='/logout']");

    public LoginPage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public void Login()
    {
        var loginCredentials = UserConfigManager.ReadCredentials();
        
        Wait.Until(ExpectedConditions.ElementToBeClickable(_emailInput)).SendKeys(loginCredentials.Email);
        Driver.FindElement(_passwordInput).SendKeys(loginCredentials.Password);
        Driver.FindElement(_loginButton).Click();
    }
    
    public void Logout()
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_logoutButton)).Click();
    }
}