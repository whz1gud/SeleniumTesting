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
        Assert.That(Driver.Title, Does.Contain("Demo Web Shop"), "Not on Home Page as expected.");

        var loginPage = homePage.GoToLoginPage();
        Assert.That(Driver.Title, Does.Contain("Demo Web Shop. Login"), "Not on Login page as expected.");

        loginPage.Login();
        Assert.That(Driver.Title, Does.Contain("Demo Web Shop"), "Not logged in succesfully.");

        var pollVotesBefore = homePage.GetPollVotesNumber();
        Assert.That(pollVotesBefore, Is.GreaterThanOrEqualTo(0), "Poll votes cannot be negative.");

        loginPage.Logout();
        Assert.That(homePage.LoginLinkExists(), Is.True, "User is still logged in after logout.");

        var registerPage = homePage.GoToRegisterPage();
        Assert.That(Driver.Title, Does.Contain("Demo Web Shop. Register"), "Not on Register Page as expected.");

        var randomEmailPart = Guid.NewGuid().ToString().Substring(0, 16);
        var randomEmail = $"{randomEmailPart}@gmail.com";
        registerPage.RegisterUser(
            "Antanas", "Bosas", randomEmail,
            "ComplexPassword123*", "ComplexPassword123*");
        Assert.That(registerPage.GetRegisterMessage(), Is.EqualTo("Your registration completed"),
            "Registration was unsuccessful");

        registerPage.ClickContinue();
        Assert.That(Driver.Title, Does.Contain("Demo Web Shop"), "Not on Home Page as expected.");

        homePage.VoteOnPoll();
        Assert.That(homePage.PollVoteSuccess(), Is.True,
            "No success indication after voting, or poll not updated immediately.");

        var pollVotesAfter = homePage.GetPollVotesNumber();
        Assert.That(pollVotesAfter, Is.GreaterThan(pollVotesBefore),
            $"Poll votes did not increase. Before: {pollVotesBefore}, After: {pollVotesAfter}");
    }
}