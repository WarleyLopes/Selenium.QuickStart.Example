using Desafio.Base2.Mantis.Paginas;
using Selenium.QuickStart.Nucleo;
using NUnit.Framework;
using System;
using System.Configuration;
using Selenium.QuickStart.Atributos;

namespace Desafio.Base2.Mantis.Testes
{
    /// <summary>
    /// <para>Classe com herança da classe BaseDeTeste com rotinas comuns para realização de testes e com</para>
    /// <para>métodos de testes a serem executados para realizar de testes na tela de Recuperação de Senha</para>
    /// </summary>
    public class RecuperarSenhaTests : BaseDeTeste
    {
        #region InstanciacaoDoPOM
        //O padrão de instanciação do objeto POM da página a ser testada gera um warning sobre criação
        //de um novo objeto e não estar assinalando valor à ele portanto tendo um valor nulo o que nesse
        //caso não é válido de fato pois suas propriedades (elementos) são privates e já têm seus valores
        #pragma warning disable CS0649

        /// <summary>
        /// Objeto da classe onde os objetos e métodos de interação da página de recuperação de senha foram mapeados
        /// </summary>
        [PaginaEmPageObjectModel] RecuperarSenhaPage Pagina_De_Recuperacao_De_Senha;

        #pragma warning restore CS0649
        #endregion

        #region VariaveisReutilizadasPelaClasseDeTestes
        //Variáveis que podem ser utilizadas entre os testes desse contexto
        public string user = ConfigurationManager.AppSettings["USERNAME"];
        public string email = ConfigurationManager.AppSettings["EMAIL"];
        #endregion

        #region MetodosParaRealizarTestesDaPaginaDeLogin
        [Test, Description("Valida o fluxo correto de navegação para página de login" +
            " a partir da página de recuperação de senha")]
        public void Teste_Navegacao_Para_Pagina_De_Login()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);

            Assert.That(Pagina_De_Recuperacao_De_Senha.Navegar_Para_Pagina().Valida_Se_Esta_Na_Pagina());
        }

        [Test, Description("Valida o fluxo correto de navegação para página de criação" +
            " de conta a partir da página de recuperação de senha")]
        public void Teste_Navegacao_Para_Pagina_De_Criacao_De_Conta()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);

            Assert.That(Pagina_De_Recuperacao_De_Senha.
                Navegar_Para_Pagina_De_Criacao_De_Conta().Valida_Se_Esta_Na_Pagina());
        }

        [Test, Description("Valida o sucesso da solicitação de recuperação de senha e" +
            " posterior redirecionamento de volta para página de login")]
        public void Teste_Efetuar_Solicitacao_De_Recuperacao_De_Senha_De_Conta_Existente()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);

            Pagina_De_Recuperacao_De_Senha.Efetuar_Solicitacao_De_Recuperacao_De_Senha(user, email);

            Assert.That(GerenciadorDoWebDriver.Driver.Url.Contains("login_page.php?return=lost_pwd.php"));
        }

        [Test, Description("Valida exibição de mensagem ao tentar recuperar senha de conta inexistente")]
        public void Teste_Tentar_Solicitacao_De_Recuperacao_De_Senha_De_Conta_Inexistente()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);

            Pagina_De_Recuperacao_De_Senha.Efetuar_Solicitacao_De_Recuperacao_De_Senha(
                new Guid().ToString(),
                new Guid()+"@"+new Guid()+".com.br"
                );

            Assert.That(Pagina_De_Recuperacao_De_Senha.
                Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(
                "informação fornecida não combina com nenhuma conta registrada"));
        }

        [Test, Description("Valida exibição de mensagem ao tentar recuperação" +
            " senha de conta com e-mail divergente do usuário")]
        public void Teste_Tentar_Solicitacao_De_Recuperacao_De_Senha_Com_Email_Divergente()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);

            Pagina_De_Recuperacao_De_Senha.Efetuar_Solicitacao_De_Recuperacao_De_Senha(
                user,
                new Guid() + "@" + new Guid() + ".com.br"
                );

            Assert.That(Pagina_De_Recuperacao_De_Senha.
                Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(
                "informação fornecida não combina com nenhuma conta registrada"));
        }

        [Test, Description("Valida exibição de mensagem ao tentar recuperação" +
            " de conta com usuário divergente do e-mail")]
        public void Teste_Tentar_Solicitacao_De_Recuperacao_De_Senha_Com_Usuario_Divergente()
        {
            new LoginPage().Digita_Usuario_E_Clica_Para_Recuperar_Senha(user);

            Pagina_De_Recuperacao_De_Senha.Efetuar_Solicitacao_De_Recuperacao_De_Senha(
                new Guid().ToString(),
                email
                );

            Assert.That(Pagina_De_Recuperacao_De_Senha.Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(
                "informação fornecida não combina com nenhuma conta registrada"));
        }
        #endregion
    }
}
