using Selenium.QuickStart.Nucleo;
using OpenQA.Selenium;

namespace Desafio.Base2.Mantis.Paginas
{
    /// <summary>
    /// <para>Classe para mapeamento de elementos da página (POM) de Criação de Conta</para>
    /// <para>e métodos de interações e validações enquanto estiver nela</para>
    /// </summary>
    public class CriacaoDeContaPage
    {
        #region ObjetosDePaginaMapeados
        private IWebElement Input_Usuario => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("username"));
        private IWebElement Input_Email => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("email-field"));
        private IWebElement Input_Captcha => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("captcha-field"));
        private IWebElement Bto_Criar_Conta => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//input[@type='submit' and @value='Criar Conta']"));
        private IWebElement Link_Para_Pagina_De_Recuperacao_De_Senha => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Perdeu a sua senha?']"));
        private IWebElement Link_Para_Pagina_De_Login => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Entrar']"));
        #endregion

        #region MetodosParaInteracoesComApagina
        /// <summary>
        /// <para>Valida se de fato está na página de criação de conta</para>
        /// <para>através de um elemento html h4 contendo 'Criar Conta'</para>
        /// </summary>
        /// <returns>Retorna um bool true ou false para validações</returns>
        public bool Valida_Se_Esta_Na_Pagina()
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//h4[text()[contains(.,'Criar Conta')]]")).Displayed;
        }

        /// <summary>
        /// Método para navegar para a página de login através da página de criação de conta
        /// </summary>
        /// <returns>Retorna o objeto POM da página de login para continuar interações e/ou validações</returns>
        public LoginPage Navegar_Para_Pagina_De_Login()
        {
            Link_Para_Pagina_De_Login.Click();
            return new LoginPage();
        }

        /// <summary>
        /// Método para enviar a solicitação de criação de senha utilizando tab a cada input
        /// </summary>
        /// <param name="usuario">Usuário da nova conta a ser criada</param>
        /// <param name="email">E-mail para a nova conta a ser criada</param>
        /// <param name="captcha">Captcha a ser digitado no formulário</param>
        public void Efetuar_Criacao_De_Conta_Digitando_Com_Tab(string usuario, string email, string captcha)
        {
            Input_Usuario.SendKeys(usuario + Keys.Tab);
            Input_Email.SendKeys(email + Keys.Tab);
            Input_Captcha.SendKeys(captcha);
            Bto_Criar_Conta.Click();
        }

        /// <summary>
        /// Verifica a exibição de uma mensagem na página dentro de uma tag html de parágrafo
        /// </summary>
        /// <param name="mensagem">Mensagem a ser verificada sua exibição</param>
        /// <returns>Retorna um bool true ou false para validações</returns>
        public bool Valida_Exibicao_De_Mensagem_De_Tentativa_De_Criacao_De_Conta(string mensagem)
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//p[text()[contains(.,'"+mensagem+"')]]")).Displayed;
        }

        /// <summary>
        /// Método para clicar no link referente à pagina de recuperação de senha navegando até ela
        /// </summary>
        /// <returns>Retorna o objeto POM da página de recuperação de senha para continuar interações e/ou validações</returns>
        public RecuperarSenhaPage Navegar_Para_Pagina_De_Recuperacao_De_Senha()
        {
            Link_Para_Pagina_De_Recuperacao_De_Senha.Click();
            return new RecuperarSenhaPage();
        }
        #endregion
    }
}