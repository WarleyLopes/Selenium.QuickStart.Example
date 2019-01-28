using System;
using System.Configuration;
using Mantis_Warley.Pages;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using Selenium.QuickStart.Atributos;

namespace Mantis_Warley.Tests
{
    public class AccountCreationTests : BaseDeTeste
    {
        #pragma warning disable CS0649
        [PaginaEmPageObjectModel] readonly AccountCreationPage _AccountCreationPage;
        #pragma warning restore CS0649
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string pass = ConfigurationManager.AppSettings["PASSWORD"];

        [Test]
        public void Test_CheckNavigationToLoginPage()
        {
            new LoginPage().NavigateToAccountCreationPageFromLoginPage();
            Assert.That(_AccountCreationPage.NavigateToLoginPage().IsOnLoginPage());
        }

        [Test]
        public void Test_CheckAccountCreationPageWithoutResolvingCaptcha()
        {
            new LoginPage().NavigateToAccountCreationPageFromLoginPage();
            _AccountCreationPage.CreateAccountTypingWithTab(
                new Guid().ToString(),
                new Guid() + "@" + new Guid() + ".com.br",
                new Random().Next(000000, 999999).ToString("000000")
                );
            Assert.That(_AccountCreationPage.IsSignUpAttemptMessageDisplayed("código de confirmação não combina"));
        }

        [Test]
        public void Test_CheckNavigationToLostPassPage()
        {
            new LoginPage().NavigateToAccountCreationPageFromLoginPage();
            Assert.That(_AccountCreationPage.NavigateToLostPassPage().IsOnLostPassPage());
        }
    }
}
