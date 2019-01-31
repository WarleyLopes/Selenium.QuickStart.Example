using System;
using System.Configuration;
using Mantis_Warley.Paginas;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using Selenium.QuickStart.Atributos;
using Selenium.QuickStart.Utilidades;
using Base2.Mantis.Test.Automation.Challenge.Resources;

namespace Mantis_Warley.Testes
{
    /// <summary>
    /// <para>Classe com herança da classe BaseDeTeste com rotinas comuns para realização de testes e</para>
    /// <para>com métodos de testes a serem executados para realizar de testes na tela de Login</para>
    /// </summary>
    public class LoginTests : BaseDeTeste
    {
        //O padrão de instanciação do objeto POM da página a ser testada gera um warning sobre criação
        //de um novo objeto e não estar assinalando valor à ele portanto tendo um valor nulo o que nesse
        //caso não é válido de fato pois suas propriedades (elementos) são privates e já têm seus valores
        #pragma warning disable CS0649

        /// <summary>
        /// Objeto da classe onde os objetos e métodos de interação da página de login foram mapeados
        /// </summary>
        [PaginaEmPageObjectModel] LoginPage Pagina_De_Login;

        #pragma warning restore CS0649

        //Variáveis que podem ser utilizadas entre os testes desse contexto
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string pass = ConfigurationManager.AppSettings["PASSWORD"];

        [Test, Description("Valida o fluxo correto de navegação para a página de login" +
            " a qual está definida como URL de início do projeto de testes")]
        public void Teste_Navegacao_Para_Pagina_De_Login()
        {
            ConectorBancoDeDadosSQLite.PreparaArquivoDoBancoDeDadosSeNecessario();
            var a = ConectorBancoDeDadosSQL.ExecutarConsulta("SELECT * FROM BUGS");

            Pagina_De_Login.Navegar_Para_Pagina();
            Assert.That(Pagina_De_Login.Valida_Se_Esta_Na_Pagina());
        }

        [Test, TestCaseSource(typeof(RepositorioDataDriven), "TarefasParaReportarDoCsvMyDataSource"),
            Description("Valida o sucesso da realização do login ao informar usuário e senhas corretas")]
        public void Teste_Efetuar_Login_Com_Credenciais_Validas(string[] data)
        {
            string guid = data[0];
            string issueName = data[1];
            string issueDescription = data[2];

            Pagina_De_Login.Efetuar_Login(user, pass);

            Assert.That(Pagina_De_Login.Valida_Que_Esta_Logado());
        }

        [Test, Description("Valida a correta validação ao tentar realizar login com credenciais inválidas")]
        public void Teste_Tentar_Efetuar_Login_Com_Credenciais_Invalidas()
        {
            Pagina_De_Login.Efetuar_Login(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            Assert.That(Pagina_De_Login.
                Valida_Exibicao_De_Texto_Com_Case_Sensitive(
                "conta pode estar desativada ou bloqueada ou o nome de usuário " +
                "e a senha que você digitou não estão corretos"));
        }

        [Test, Description("Valida o fluxo correto de navegação para a página" +
            " de criação de conta a partir da página de login")]
        public void Teste_Navegacao_Para_Pagina_De_Criacao_De_Conta()
        {
            Assert.That(Pagina_De_Login.Navegar_Para_Pagina_De_Criacao_De_Conta().Valida_Se_Esta_Na_Pagina());
        }

        [Test, Description("Valida o fluxo correto de navegação para a página" +
            " de recuperação de senha de uma conta na página de login")]
        public void Teste_Navegacao_Para_Recuperacao_De_Senha_Digitando_Usuario_Antes()
        {
            Assert.That(Pagina_De_Login.Digita_Usuario_E_Clica_Para_Recuperar_Senha(user).Valida_Se_Esta_Na_Pagina());
        }
    }
}
