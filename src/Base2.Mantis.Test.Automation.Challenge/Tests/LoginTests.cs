using System;
using System.Configuration;
using Mantis_Warley.Pages;
using Selenium.QuickStart.Core;
using NUnit.Framework;
using Selenium.QuickStart.Attributes;

namespace Mantis_Warley.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        #pragma warning disable CS0649
        [PageObject] LoginPage _LoginPage;
        #pragma warning restore CS0649
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string pass = ConfigurationManager.AppSettings["PASSWORD"];

        [Test]
        public void Test_CheckNavigationToLoginPage()
        {
            _LoginPage.NavigateToPage();
            Assert.That(_LoginPage.IsOnLoginPage());
        }

        [Test]
        public void Test_CheckValidLoginAttempt()
        {
            _LoginPage.Login(user, pass);
            Assert.That(_LoginPage.IsLoggedIn());
        }

        [Test]
        public void Test_CheckInvalidLoginAttempt()
        {
            _LoginPage.Login(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.That(_LoginPage.
                IsErrorMessageCaseInsensitiveVisibleAnywherePartiallyOrNot(
                "conta pode estar desativada ou bloqueada ou o nome de usuário " +
                "e a senha que você digitou não estão corretos"));
        }

        [Test]
        public void Test_CheckNavigationToAccountCreationPageFromLoginPage()
        {
            Assert.That(_LoginPage.NavigateToAccountCreationPageFromLoginPage().IsOnAccountCreationPage());
        }

        [Test]
        public void Test_CheckNavigationToLostPassPageUponTypingInUsernameFirstly()
        {
            Assert.That(_LoginPage.TypeUserAndClickToRecoverForgottenPassword(user).IsOnLostPassPage());
        }
    }
}
