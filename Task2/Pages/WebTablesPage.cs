using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task2.Pages;

public class WebTablesPage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;

    private By _totalPages = By.XPath("//span[@class='-totalPages']");
    private By _currentPage = By.XPath("//div[@class='-pageJump']/input");
    private By _addButton = By.XPath("//button[@id='addNewRecordButton']");
    private By _nextButton = By.XPath("//div[@class='-next']/button");

    private By _firstName = By.XPath("//input[@id='firstName']");
    private By _lastName = By.XPath("//input[@id='lastName']");
    private By _email = By.XPath("//input[@id='userEmail']");
    private By _age = By.XPath("//input[@id='age']");
    private By _salary = By.XPath("//input[@id='salary']");
    private By _department = By.XPath("//input[@id='department']");
    private By _submitButton = By.XPath("//button[@id='submit']");
    
    private By _deleteIcon = By.XPath("//span[@id='delete-record-11']");

    public WebTablesPage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public void AddUntilSecondPageExists()
    {
        Wait.Until(d =>
        {
            if (SecondPageExists())
            {
                return true;
            }

            AddUser("Jonas", "Douuu", "jonas@gmail.com", "30", "3000", "5");
            return false;
        });
    }

    public void GoToSecondPage()
    {
        var nextButton = Wait.Until(ExpectedConditions.ElementToBeClickable(_nextButton));
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", nextButton);
        nextButton.Click();
    }

    public void DeleteRecordOnSecondPage()
    {
        var deleteIcon = Wait.Until(ExpectedConditions.ElementToBeClickable(_deleteIcon));
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", deleteIcon);
    }

    public bool SecondPageExists()
    {
        return Driver.FindElement(_totalPages).Text.Contains('2');
    }

    private void AddUser(string firstName, string lastName, string email, string age, string salary, string department)
    {
        Driver.FindElement(_addButton).Click();

        Wait.Until(ExpectedConditions.ElementIsVisible(_firstName)).SendKeys(firstName);
        Driver.FindElement(_lastName).SendKeys(lastName);
        Driver.FindElement(_email).SendKeys(email);
        Driver.FindElement(_age).SendKeys(age);
        Driver.FindElement(_salary).SendKeys(salary);
        Driver.FindElement(_department).SendKeys(department);

        Driver.FindElement(_submitButton).Click();
    }
    
    public string GetCurrentPageNumber()
    {
        var currentPageInput = Driver.FindElement(By.XPath("//div[@class='-pageJump']/input"));
        return currentPageInput.GetAttribute("value");
    }

    public string GetTotalPages()
    {
        return Wait.Until(ExpectedConditions.ElementIsVisible(_totalPages)).Text;
    }
}