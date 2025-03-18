using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Task4.Base;

namespace Task4.Pages;

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
    private By _registerMessage = By.XPath("//div[@class='result']");
    private By _continueButton = By.XPath("//input[contains(@class, 'button-1') and contains(@class, 'register-continue-button')]");

    public RegisterPage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public void RegisterUser(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_firstName)).SendKeys(firstName);
        Driver.FindElement(_lastName).SendKeys(lastName);
        Driver.FindElement(_gender).Click();
        Driver.FindElement(_email).SendKeys(email);
        Driver.FindElement(_password).SendKeys(password);
        Driver.FindElement(_confirmPassword).SendKeys(confirmPassword);
        Driver.FindElement(_registerButton).Click();
    }

    public string GetRegisterMessage()
    {
        var messageElement = Wait.Until(ExpectedConditions.ElementIsVisible(_registerMessage));
        return messageElement.Text.Trim();
    }

    public void ClickContinue()
    {
        Wait.Until(ExpectedConditions.ElementExists(_continueButton)).Click();
    }
}