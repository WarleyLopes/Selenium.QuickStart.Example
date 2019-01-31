using System;
using System.Configuration;
using Mantis_Warley.Paginas;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using Selenium.QuickStart.Atributos;
using Selenium.QuickStart.Utilidades;
using Base2.Mantis.Test.Automation.Challenge.Resources;

namespace Mantis_Warley.Tests
{
    public class LoginTests : BaseDeTeste
    {
        #pragma warning disable CS0649
        [PaginaEmPageObjectModel] LoginPage _LoginPage;
        #pragma warning restore CS0649
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string pass = ConfigurationManager.AppSettings["PASSWORD"];

        [Test]
        public void Test_CheckNavigationToLoginPage()
        {
            ConectorBancoDeDadosSQLite.PreparaArquivoDoBancoDeDadosSeNecessario();
            var a = ConectorBancoDeDadosSQL.ExecutarConsulta("SELECT * FROM BUGS");
            _LoginPage.Navegar_Para_Pagina();
            Assert.That(_LoginPage.Valida_Que_Esta_Na_Pagina());
        }

        [Test, TestCaseSource(typeof(RepositorioDataDriven), "TarefasParaReportarDoCsvMyDataSource")]
        public void Test_CheckValidLoginAttempt(string[] data)
        {
            string guid = data[0];
            string issueName = data[1];
            string issueDescription = data[2];
            _LoginPage.Logar(user, pass);
            Assert.That(_LoginPage.Valida_Que_Esta_Logado());
        }

        [Test]
        public void Test_CheckInvalidLoginAttempt()
        {
            _LoginPage.Logar(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.That(_LoginPage.
                Valida_Se_Existe_Texto_Na_Pagina_Com_Case_Sensitive(
                "conta pode estar desativada ou bloqueada ou o nome de usuário " +
                "e a senha que você digitou não estão corretos"));
        }

        [Test]
        public void Test_CheckNavigationToAccountCreationPageFromLoginPage()
        {
            Assert.That(_LoginPage.Navegar_Para_Pagina_De_Criacao_De_Conta_Pela_Pagina_De_Login().IsOnAccountCreationPage());
        }

        [Test]
        public void Test_CheckNavigationToLostPassPageUponTypingInUsernameFirstly()
        {
            Assert.That(_LoginPage.Digita_Usuario_E_Clica_Para_Recuperar_Senha(user).IsOnLostPassPage());
        }
    }
}
