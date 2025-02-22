using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Task3.Base;

namespace Task3.Pages;

public class LoginPage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _registerButton = By.XPath("//input[@class='button-1 register-button']");
    private By _emailInput = By.XPath("//input[@id='Email']");
    private By _passwordInput = By.XPath("//input[@id='Password']");
    private By _loginButton = By.XPath("//input[@class='button-1 login-button']");

    public LoginPage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public RegisterPage GoToRegisterPage()
    {
        var registerButton = Driver.FindElement(_registerButton);
        registerButton.Click();
        return new RegisterPage(Driver, Wait);
    }

    public void Login()
    {
        var loginCredentials = UserConfigManager.ReadCredentials();
        
        Wait.Until(ExpectedConditions.ElementToBeClickable(_emailInput)).SendKeys(loginCredentials.Email);
        Driver.FindElement(_passwordInput).SendKeys(loginCredentials.Password);
        Driver.FindElement(_loginButton).Click();
    }
}