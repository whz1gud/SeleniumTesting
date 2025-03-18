using Task4.Base;
using Task4.Config;
using Task4.Pages;

namespace Task4.Tests;

[TestFixture]
public class PollVoteTests : BaseTest
{
    [Test]
    public void PollVote_IncrementsCount()
    {
        Driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        var homePage = new HomePage(Driver, Wait);

        var loginPage = homePage.GoToLoginPage();
        loginPage.Login();

        var pollVotesBefore = homePage.GetPollVotesNumber();
        loginPage.Logout();

        var registerPage = homePage.GoToRegisterPage();

        var randomEmailPart = Guid.NewGuid().ToString().Substring(0, 16);
        var randomEmail = $"{randomEmailPart}@gmail.com";
        
        registerPage.RegisterUser(
            "Antanas", "Bosas", randomEmail,
            "ComplexPassword123*", "ComplexPassword123*");
        
        
        Assert.That(registerPage.GetRegisterMessage(), Is.EqualTo("Your registration completed"),
            "Registration was unsuccessful");
        
        registerPage.ClickContinue();
        
        homePage.VoteOnPoll();
        var pollVotesAfter = homePage.GetPollVotesNumber();
    }
}