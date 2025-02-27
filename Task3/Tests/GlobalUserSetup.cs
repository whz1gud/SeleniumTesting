using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Task3.Base;
using Task3.Pages;

namespace Task3.Tests;

[SetUpFixture]
public class GlobalUserSetup
{
    [OneTimeSetUp]
    public void CreateUserIfNeeded()
    {
        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        options.AddArgument("--headless");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        
        var driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        var homePage = new HomePage(driver, wait);
        var loginPage = homePage.GoToLoginPage();
        var registerPage = loginPage.GoToRegisterPage();
        
        var randomEmailPart = Guid.NewGuid().ToString().Substring(0, 16);
        var randomEmail = $"{randomEmailPart}@gmail.com";

        var credentials = registerPage.RegisterUser(
            "Antanas", "Bosas", randomEmail,
            "ComplexPassword123*", "ComplexPassword123*");
        
        UserConfigManager.SaveCredentials(credentials);

        driver.Quit();
        driver.Dispose();
    }
}