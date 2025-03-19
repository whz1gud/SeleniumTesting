using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task4.Base;

public class BaseTest
{
    protected IWebDriver Driver;
    protected WebDriverWait Wait;

    [SetUp]
    public void GlobalSetup()
    {
        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        // options.AddArgument("--headless");
        // options.AddArgument("--no-sandbox");
        // options.AddArgument("--disable-dev-shm-usage");
        Driver = new ChromeDriver(options);
        Driver.Manage().Window.Maximize();
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
    }

    [TearDown]
    public void GlobalTeardown()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}