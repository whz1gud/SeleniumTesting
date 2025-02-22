using Task2.Pages;

namespace Task2.Tests;

public class WebTableTests : BaseTest
{
    [Test]
    public void AddElements_AndDeleteElementFromSecondPage_ReturnsToFirstPage()
    {
        var homePage = new HomePage(Driver, Wait);
        homePage.ClickElementsTab();
        
        var webTablesPage = homePage.NavigateToWebTablesPage();
        webTablesPage.AddUntilSecondPageExists();
        webTablesPage.GoToSecondPage();
        webTablesPage.DeleteRecordOnSecondPage();
        
        Assert.Multiple(() =>
        {
            Assert.That(webTablesPage.GetCurrentPageNumber, Is.EqualTo("1"),
                "The current page should be 1 after deletion.");
            Assert.That(webTablesPage.GetTotalPages(), Is.EqualTo("1"),
                "There should only be one page after deletion.");
        });
    }
}