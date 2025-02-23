using Task3.Base;
using Task3.Pages;

namespace Task3.Tests;

[TestFixture]
public class OrderPlacementTest_Data1 : BaseTest
{
    [Test]
    public void OrderPlacement_FromData1Txt_Success()
    {
        Driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        var homePage = new HomePage(Driver, Wait);
        
        var loginPage = homePage.GoToLoginPage();
        loginPage.Login();
        
        homePage.ClickDigitalDownloadsItem();
        homePage.ReadTxtFileAndAddToCart("data1.txt");
        
        var shoppingCartPage = homePage.GoToShoppingCartPage();
        shoppingCartPage.ClickTermsOfServiceAndContinue();
        shoppingCartPage.FillOrSelectBillingAddress();
        shoppingCartPage.ClickContinueAndConfirm();
        
        var confirmationMessage = shoppingCartPage.GetConfirmationMessage();
        Assert.That(confirmationMessage, Does.Contain("Your order has been successfully processed!"), 
            "The order did not complete successfully.");
    }
    
    [Test]
    public void OrderPlacement_FromData2Txt_Success()
    {
        Driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        var homePage = new HomePage(Driver, Wait);
        
        var loginPage = homePage.GoToLoginPage();
        loginPage.Login();
        
        homePage.ClickDigitalDownloadsItem();
        homePage.ReadTxtFileAndAddToCart("data2.txt");
        
        var shoppingCartPage = homePage.GoToShoppingCartPage();
        shoppingCartPage.ClickTermsOfServiceAndContinue();
        shoppingCartPage.FillOrSelectBillingAddress();
        shoppingCartPage.ClickContinueAndConfirm();
        
        var confirmationMessage = shoppingCartPage.GetConfirmationMessage();
        Assert.That(confirmationMessage, Does.Contain("Your order has been successfully processed!"), 
            "The order did not complete successfully.");
    }
}