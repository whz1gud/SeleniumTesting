using Task2.Pages;

namespace Task2.Tests;

public class ProgressBarTests : BaseTest
{
    [Test]
    public void ProgressBar_Reaches100AndResetsToZero()
    {
        var homePage = new HomePage(Driver, Wait);
        homePage.ClickWidgetsTab();

        var progressBarPage = homePage.NavigateToProgressBarPage();
        progressBarPage.ClickStart();
        progressBarPage.WaitUntilProgressCompletes();
        progressBarPage.ClickReset();
        
        Assert.That(progressBarPage.GetProgress(), Is.EqualTo("0%"), "The progress bar did not reset to 0%");
    }
}