using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task2;

public class BaseTest
{
    protected IWebDriver Driver;
    protected WebDriverWait Wait;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        Driver = new ChromeDriver(options);
        Driver.Manage().Window.Maximize();
        
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
        Driver.Navigate().GoToUrl("https://web.archive.org/web/20231223055945mp_/https://demoqa.com/");
    }

    [TearDown]
    public void Close()
    {
        if (Driver != null)
        {
            Driver.Quit();
            Driver.Dispose();   
        }
    }
}