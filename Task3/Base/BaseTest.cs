using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task3.Base;

public class BaseTest
{
    protected IWebDriver Driver;
    protected WebDriverWait Wait;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        Driver = new ChromeDriver(options);
        Driver.Manage().Window.Maximize();
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
        Driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}