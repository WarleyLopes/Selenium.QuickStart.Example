using Mantis_Warley.Paginas;
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
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);
            Assert.That(_LostPassPage.NavigateToLoginPage().Valida_Que_Esta_Na_Pagina());
        }

        [Test]
        public void Test_CheckNavigationToAccountCreationPage()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);
            Assert.That(_LostPassPage.NavigateToAccountCreationPage().IsOnAccountCreationPage());
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPassword()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);
            _LostPassPage.RequestPassRecovery(user, email);
            Assert.That(GerenciadorDoWebDriver.Driver.Url.Contains("login_page.php?return=lost_pwd.php"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordOfUnexistentUser()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);
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
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);
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
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);
            _LostPassPage.RequestPassRecovery(
                new Guid().ToString(),
                email
                );
            Assert.That(_LostPassPage.IsRequestPassRecoveryAttemptMessageDisplayed(
                "informação fornecida não combina com nenhuma conta registrada"));
        }
    }
}
