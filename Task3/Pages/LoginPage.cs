using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Task3.Base;

namespace Task3.Pages;

public class LoginPage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _registerButton = By.XPath("//input[@class='button-1 register-button']");

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
}