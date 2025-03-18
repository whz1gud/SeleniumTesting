using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task4.Pages;

public class HomePage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _loginItem = By.XPath("//a[@href='/login']");
    private By _registerItem = By.XPath("//a[@href='/register']");
    private By _pollItem = By.XPath("//span[@class='poll-total-votes']");
    private By _pollOption1 = By.XPath("//input[@id='pollanswers-1']");
    private By _voteButton = By.XPath("//input[@id='vote-poll-1']");

    public HomePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public RegisterPage GoToRegisterPage()
    {
        var registerItem = Wait.Until(ExpectedConditions.ElementToBeClickable(_registerItem));
        registerItem.Click();
        return new RegisterPage(Driver, Wait);
    }

    public LoginPage GoToLoginPage()
    {
        var loginItem = Driver.FindElement(_loginItem);
        loginItem.Click();
        return new LoginPage(Driver, Wait);
    }

    public int GetPollVotesNumber()
    {
        var pollText = Wait.Until(ExpectedConditions.ElementIsVisible(_pollItem)).Text;
        
        var splitted = pollText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        var votes = int.Parse(splitted[0]);
        return votes;
    }

    public void VoteOnPoll()
    {
        Wait.Until(ExpectedConditions.ElementIsVisible(_pollOption1)).Click();
        Driver.FindElement(_voteButton).Click();
    }
}