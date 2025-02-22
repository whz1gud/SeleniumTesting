using Task3.Base;
using Task3.Pages;

namespace Task3.Tests;

public class UserRegistrationTests : BaseTest
{
    [OneTimeSetUp]
    public void RegisterUserIfNeeded()
    {
        if (UserConfigManager.CredentialsExist())
        {
            TestContext.Out.WriteLine("User credentials already exist. Skipping registration.");
            return;
        }

        var homePage = new HomePage(Driver, Wait);
        var loginPage = homePage.GoToLoginPage();
        var registerPage = loginPage.GoToRegisterPage();

        var credentials = registerPage.RegisterUser("Antanas", "Bosas", "NepanaudotasEmailPls800@gmail.com",
            "ComplexPassword123*", "ComplexPassword123*");

        var credentialsToSave = new UserCredentials
        {
            Email = credentials.Email,
            Password = credentials.Password
        };

        UserConfigManager.SaveCredentials(credentialsToSave);
    }

    [Test]
    public void DummyTestForRegistration()
    {
        Assert.That(UserConfigManager.CredentialsExist(), Is.True,
            "User credentials do not exist in UserCredentials.json file.");
    }
}