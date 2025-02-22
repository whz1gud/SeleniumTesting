using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task2.Pages;

public class HomePage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;

    public HomePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public void ClickWidgetsTab()
    {
        var widgetsTab =
            Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[@class='card-body']/h5[text()='Widgets']")));
        widgetsTab.Click();
    }

    public void ClickElementsTab()
    {
        var elementsTab =
            Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[@class='card-body']/h5[text()='Elements']")));
        elementsTab.Click();
    }

    public ProgressBarPage NavigateToProgressBarPage()
    {
        var progressBarItem =
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Progress Bar']")));
        // Scroll into view, to not click on the footer
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", progressBarItem);
        progressBarItem.Click();
        return new ProgressBarPage(Driver, Wait);
    }
    
    public WebTablesPage NavigateToWebTablesPage()
    {
        var WebTablesItem =
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Web Tables']")));
        // don't need to scroll into view, because already in view
        WebTablesItem.Click();
        return new WebTablesPage(Driver, Wait);
    }
}