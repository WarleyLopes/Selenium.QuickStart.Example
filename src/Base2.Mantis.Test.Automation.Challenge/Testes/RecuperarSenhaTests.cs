using Mantis_Warley.Paginas;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using System;
using System.Configuration;
using Selenium.QuickStart.Atributos;
using Base2.Mantis.Test.Automation.Challenge.Resources;

namespace Mantis_Warley.Testes
{
    public class RecuperarSenhaTests : BaseDeTeste
    {
        #pragma warning disable CS0649
        [PaginaEmPageObjectModel] RecuperarSenhaPage _LostPassPage;
        #pragma warning restore CS0649
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string email = ConfigurationManager.AppSettings["EMAIL"];

        [Test]
        public void Test_CheckNavigationToLoginPage()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(user);
            Assert.That(_LostPassPage.Navegar_Para_Pagina().Valida_Se_Esta_Na_Pagina());
        }

        [Test]
        public void Test_CheckNavigationToAccountCreationPage()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(user);
            Assert.That(_LostPassPage.Navegar_Para_Pagina_De_Criacao_De_Conta().Valida_Se_Esta_Na_Pagina());
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPassword()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(user);
            _LostPassPage.Efetuar_Solicitacao_De_Recuperacao_De_Senha(user, email);
            Assert.That(GerenciadorDoWebDriver.Driver.Url.Contains("login_page.php?return=lost_pwd.php"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordOfUnexistentUser()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(user);
            _LostPassPage.Efetuar_Solicitacao_De_Recuperacao_De_Senha(
                new Guid().ToString(),
                new Guid()+"@"+new Guid()+".com.br"
                );
            Assert.That(_LostPassPage.
                Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(
                "informação fornecida não combina com nenhuma conta registrada"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordWithoutMatchingEmail()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(user);
            _LostPassPage.Efetuar_Solicitacao_De_Recuperacao_De_Senha(
                user,
                new Guid() + "@" + new Guid() + ".com.br"
                );
            Assert.That(_LostPassPage.
                Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(
                "informação fornecida não combina com nenhuma conta registrada"));
        }

        [Test]
        public void Test_CheckRequestToRecoverForgottenPasswordWithoutMatchingUsername()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(user);
            _LostPassPage.Efetuar_Solicitacao_De_Recuperacao_De_Senha(
                new Guid().ToString(),
                email
                );
            Assert.That(_LostPassPage.Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(
                "informação fornecida não combina com nenhuma conta registrada"));
        }
    }
}
