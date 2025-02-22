using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task3.Pages;

public class HomePage
{
    private IWebDriver Driver;
    private WebDriverWait Wait;
    
    private By _loginItem = By.XPath("//a[@href='/login']");
    private By _digitalDownloadsItem = By.XPath("//a[@href='/digital-downloads']");
    private By _shoppingCartItem = By.XPath("//a[@href='/cart']");

    public HomePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public LoginPage GoToLoginPage()
    {
        var loginItem = Driver.FindElement(_loginItem);
        loginItem.Click();
        return new LoginPage(Driver, Wait);
    }

    public ShoppingCart GoToShoppingCartPage()
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_shoppingCartItem)).Click();
        return new ShoppingCart(Driver, Wait);
    }
    
    public void ClickDigitalDownloadsItem()
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_digitalDownloadsItem)).Click();
    }
    
    public void ReadTxtFileAndAddToCart(string fileName)
    {
        var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);
        Console.WriteLine(fullPath);
        
        var productNames = File.ReadAllLines(fullPath);
        
        foreach (var productName in productNames)
        {
            AddProductToCart(productName);
        }
    }

    private void AddProductToCart(string productName)
    {
        Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='ajax-loading-block-window']")));
        
        By addToCartButton = By.XPath($@"
        //h2[@class='product-title']/a[contains(text(), '{productName}')]
          /ancestor::div[@class='item-box']
          //input[@value='Add to cart']");
        
        var button = Wait.Until(ExpectedConditions.ElementToBeClickable(addToCartButton));
        button.Click();
    }
}