using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Task3.Base;

namespace Task3.Pages;

public class RegisterPage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _gender = By.XPath("//input[@id='gender-male']");
    private By _firstName = By.XPath("//input[@id='FirstName']");
    private By _lastName = By.XPath("//input[@id='LastName']");
    private By _email = By.XPath("//input[@id='Email']");
    private By _password = By.XPath("//input[@id='Password']");
    private By _confirmPassword = By.XPath("//input[@id='ConfirmPassword']");
    private By _registerButton = By.XPath("//input[@id='register-button']");

    public RegisterPage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public UserCredentials RegisterUser(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_firstName)).SendKeys(firstName);
        Driver.FindElement(_lastName).SendKeys(lastName);
        Driver.FindElement(_gender).Click();
        Driver.FindElement(_email).SendKeys(email);
        Driver.FindElement(_password).SendKeys(password);
        Driver.FindElement(_confirmPassword).SendKeys(confirmPassword);
        Driver.FindElement(_registerButton).Click();
        
        var loginCredentials = new UserCredentials
        {
            Email = email,
            Password = password
        };

        return loginCredentials;
    }
}