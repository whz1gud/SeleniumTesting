using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.google.com");
        
        var title = driver.Title;
        Console.WriteLine(title);
        
        Thread.Sleep(3000);
        
        driver.Quit();
    }
}