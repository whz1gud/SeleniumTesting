using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task1._2;

class Program
{
    public static void Main(string[] args)
    {
        var options = new ChromeOptions();
        options.AddArgument("ignore-certificate-errors");
        var driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

        // Gift Card Flow
        ProcessGiftCards(driver, wait);
        /**
         * homePage.openGiftCardsView()
         *  .addToCard(100)
         *  Pasiziuret Page Object Model (POM) in Selenium - design pattern
         * 
         */

        // Jewelry Flow
        ProcessJewelry(driver, wait);

        // Validate Subtotal
        ValidateSubtotal(driver, wait);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static void ProcessGiftCards(IWebDriver driver, WebDriverWait wait)
    {
        driver.FindElement(By.XPath("//li[contains(@class, 'inactive')]/a[normalize-space()='Gift Cards']")).Click();
        driver.FindElement(By.XPath(
                "//div[contains(@class, 'product-grid')]//div[contains(@class, 'item-box') and number(.//div[contains(@class, 'prices')]/span) > 99][1]//input[contains(@class, 'product-box-add-to-cart-button')]\n"))
            .Click();

        // Wait and fill in gift card details
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='giftcard_4_RecipientName']"))).SendKeys("Gavejas");
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='giftcard_4_SenderName']"))).SendKeys("Siuntejas");
        var qtyInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='addtocart_4_EnteredQuantity']")));
        qtyInput.Clear();
        qtyInput.SendKeys("5000");
        
        driver.FindElement(By.XPath("//input[@id='add-to-cart-button-4']")).Click();

        // Wait until the ajax loading overlay is no longer visible and click add to wishlist button
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='ajax-loading-block-window']")));
        driver.FindElement(By.XPath("//input[@id='add-to-wishlist-button-4']")).Click();
    }

    private static void ProcessJewelry(IWebDriver driver, WebDriverWait wait)
    {
        driver.FindElement(By.XPath("//li[@class='inactive']/a[normalize-space()='Jewelry']")).Click();
        driver.FindElement(By.XPath("//h2[@class='product-title']//a[contains(text(), 'Create Your Own Jewelry')]"))
            .Click();

        // Jewelry selections:
        new SelectElement(driver.FindElement(By.XPath("//select[@id='product_attribute_71_9_15']"))).SelectByValue("47");
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='product_attribute_71_10_16']"))).SendKeys("80");
        
        //star shape
        driver.FindElement(By.XPath("//input[@id='product_attribute_71_11_17_50']")).Click();

        var qtyInput = driver.FindElement(By.XPath("//input[@id='addtocart_71_EnteredQuantity']"));
        qtyInput.Clear();
        qtyInput.SendKeys("26");

        driver.FindElement(By.XPath("//input[@id='add-to-cart-button-71']")).Click();

        // // Wait until the ajax loading overlay is no longer visible and click add to wishlist button
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='ajax-loading-block-window']")));
        driver.FindElement(By.XPath("//input[@id='add-to-wishlist-button-71']")).Click();

        // Navigate to the wishlist page and add all items to cart
        driver.FindElement(By.XPath("//a[@class='ico-wishlist']")).Click();
        var addToCartButtons = driver.FindElements(By.XPath("//td[@class='add-to-cart']/input"));
        foreach (var button in addToCartButtons)
        {
            button.Click();
        }

        driver.FindElement(By.XPath("//div[@class='common-buttons']/input[@name='addtocartbutton']")).Click();
    }

    private static void ValidateSubtotal(IWebDriver driver, WebDriverWait wait)
    {
        // XPath for the <tr> element containing 'Sub-Total' row.
        string subTotalTr = "//table[@class='cart-total']//tr[" +
                            "td[@class='cart-total-left' and contains(normalize-space(.), 'Sub-Total:')]" +
                            " and td[@class='cart-total-right']]";

        var trElement = wait.Until(ExpectedConditions.ElementExists(By.XPath(subTotalTr)));

        // XPath for subtotal price span element
        var productPriceSpan =
            trElement.FindElement(By.XPath("//td[@class='cart-total-right']//span[@class='product-price']"));
        var productSubtotal = productPriceSpan.Text;

        var expectedSubtotal = "1002600.00";

        if (productSubtotal == expectedSubtotal)
        {
            Console.WriteLine("Subtotal is correct: " + productSubtotal);
        }
        else
        {
            Console.WriteLine($"Subtotal is incorrect. Expected {expectedSubtotal} but got {productSubtotal}");
        }
    }
}