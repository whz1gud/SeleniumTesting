using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task2.Pages;

public class ProgressBarPage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _startButton = By.XPath("//button[@id='startStopButton']");
    private By _resetButton = By.XPath("//button[@id='resetButton']");
    private By _progressBar = By.XPath("//div[@id='progressBar']/div");

    public ProgressBarPage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public void ClickStart() 
    {
        ((IJavaScriptExecutor)Driver).ExecuteScript("document.getElementById('wm-ipp-base').remove();");
        ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0);");
        
        var startButtonElement = Driver.FindElement(_startButton);
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", startButtonElement);
    }
    public void ClickReset() => Driver.FindElement(_resetButton).Click();
    public string GetProgress() => Driver.FindElement(_progressBar).Text;
    public void WaitUntilProgressCompletes()
    {
        Wait.Until(d =>
        {
            var progressText = d.FindElement(_progressBar).Text;
            return progressText.Contains("100%");
        });
    }
}