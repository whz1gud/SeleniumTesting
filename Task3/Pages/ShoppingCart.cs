using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Task3.Pages;

public class ShoppingCart
{
    private IWebDriver Driver;
    private WebDriverWait Wait;

    private By _termsOfService = By.XPath("//input[@id='termsofservice']");
    private By _checkoutButton = By.XPath("//button[@id='checkout']");
    private By _countrySelect = By.XPath("//select[@id='BillingNewAddress_CountryId']");
    private By _city = By.XPath("//input[@id='BillingNewAddress_City']");
    private By _address1 = By.XPath("//input[@id='BillingNewAddress_Address1']");
    private By _zip = By.XPath("//input[@id='BillingNewAddress_ZipPostalCode']");
    private By _phoneNumber = By.XPath("//input[@id='BillingNewAddress_PhoneNumber']");
    private By _continueBillingAddress = By.XPath("//input[@class='button-1 new-address-next-step-button']");
    private By _continuePaymentMethod = By.XPath("//input[@class='button-1 payment-method-next-step-button']");
    private By _continuePaymentInfo = By.XPath("//input[@class='button-1 payment-info-next-step-button']");
    private By _confirmButton = By.XPath("//input[@class='button-1 confirm-order-next-step-button']");

    public ShoppingCart(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    public void ClickTermsOfServiceAndContinue()
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_termsOfService)).Click();
        Driver.FindElement(_checkoutButton).Click();
    }

    public void FillOrSelectBillingAddress()
    {
        var addressSelectElements = Driver.FindElements(By.Id("billing-address-select"));
        if (addressSelectElements.Count > 0)
        {
            var addressSelectElement = new SelectElement(addressSelectElements[0]);
            
            if (addressSelectElement.Options.Count > 1)
            {
                addressSelectElement.SelectByIndex(0);
                Driver.FindElement(By.CssSelector("input.button-1.new-address-next-step-button")).Click();
            }
        }
        else
        {
            new SelectElement(Wait.Until(ExpectedConditions.ElementToBeClickable(_countrySelect))).SelectByValue("3");
            Driver.FindElement(_city).SendKeys("Afgaaan");
            Driver.FindElement(_address1).SendKeys("LondonStreet");
            Driver.FindElement(_zip).SendKeys("USA123");
            Driver.FindElement(_phoneNumber).SendKeys("555-555-5555");

            Driver.FindElement(_continueBillingAddress).Click();
        }
    }

    public void ClickContinueAndConfirm()
    {
        Wait.Until(ExpectedConditions.ElementToBeClickable(_continuePaymentMethod)).Click();
        Wait.Until(ExpectedConditions.ElementToBeClickable(_continuePaymentInfo)).Click();
        Wait.Until(ExpectedConditions.ElementToBeClickable(_confirmButton)).Click();
    }

    public string GetConfirmationMessage()
    {
        return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//strong"))).Text;
    }
}