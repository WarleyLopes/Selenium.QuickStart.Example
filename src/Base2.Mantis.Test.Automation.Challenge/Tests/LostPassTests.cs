using Mantis_Warley.Pages;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using System;
using System.Configuration;
using Selenium.QuickStart.Atributos;
using Base2.Mantis.Test.Automation.Challenge.Resources;

namespace Mantis_Warley.Tests
{
    public class LostPassTests : BaseDeTeste
    {
        #pragma warning disable CS0649
        [PaginaEmPageObjectModel] LostPassPage _LostPassPage;
        #pragma warning restore CS0649
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string email = ConfigurationManager.AppSettings["EMAIL"];

        [Test]
        public void Test_CheckNavigationToLoginPage()
        {
            new LoginPage().TypeUserAndClickToRecoverForgottenPassword(user);
            Assert.That(_LostPassPage.NavigateToLoginPage().IsOnLoginPage());
        }

        [Test]
        public void Test_CheckNavigationToAccountCreationPage()
        {
            new LoginPage().TypeUserAndClickToRecoverForgottenPassword(user);
            Assert.That(_LostPassPage.NavigateToAccountCreationPage().IsOnAccountCreationPage());
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPassword()
        {
            new LoginPage().TypeUserAndClickToRecoverForgottenPassword(user);
            _LostPassPage.RequestPassRecovery(user, email);
            Assert.That(GerenciadorDoWebDriver.Driver.Url.Contains("login_page.php?return=lost_pwd.php"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordOfUnexistentUser()
        {
            new LoginPage().TypeUserAndClickToRecoverForgottenPassword(user);
            _LostPassPage.RequestPassRecovery(
                new Guid().ToString(),
                new Guid()+"@"+new Guid()+".com.br"
                );
            Assert.That(_LostPassPage.
                IsRequestPassRecoveryAttemptMessageDisplayed(
                "informação fornecida não combina com nenhuma conta registrada"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordWithoutMatchingEmail()
        {
            new LoginPage().TypeUserAndClickToRecoverForgottenPassword(user);
            _LostPassPage.RequestPassRecovery(
                user,
                new Guid() + "@" + new Guid() + ".com.br"
                );
            Assert.That(_LostPassPage.
                IsRequestPassRecoveryAttemptMessageDisplayed(
                "informação fornecida não combina com nenhuma conta registrada"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordWithoutMatchingUsername()
        {
            new LoginPage().TypeUserAndClickToRecoverForgottenPassword(user);
            _LostPassPage.RequestPassRecovery(
                new Guid().ToString(),
                email
                );
            Assert.That(_LostPassPage.IsRequestPassRecoveryAttemptMessageDisplayed(
                "informação fornecida não combina com nenhuma conta registrada"));
        }
    }
}
