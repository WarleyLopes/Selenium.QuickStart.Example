using Selenium.QuickStart.Nucleo;
using OpenQA.Selenium;

namespace Mantis_Warley.Paginas
{
    /// <summary>
    /// <para>Classe para mapeamento de elementos da página (POM) de Recuperação de Senha</para>
    /// <para>e métodos de interações e validações enquanto estiver nela</para>
    /// </summary>
    public class RecuperarSenhaPage
    {
        #region ObjetosDePaginaMapeados
        private IWebElement Input_Usuario => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("username"));
        private IWebElement Input_Email => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("email-field"));
        private IWebElement Bto_Enviar => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//input[@type='submit' and @value='Enviar']"));
        private IWebElement Link_Para_Pagina_De_Login => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Entrar']"));
        private IWebElement Link_Para_Pagina_Criar_Conta => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'criar uma nova conta']"));
        #endregion

        #region MetodosParaInteracoesComApagina
        /// <summary>
        /// Verifica se está na página de recuperação de senha através de um texto h4 contendo 'Reajuste de Senha'
        /// </summary>
        /// <returns>Retorna um bool true ou false para validações</returns>
        public bool Valida_Se_Esta_Na_Pagina()
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//h4[text()[contains(.,'Reajuste de Senha')]]")).Displayed;
        }

        /// <summary>
        /// Método para clicar no link referente à pagina de login e navegar à ela de volta
        /// </summary>
        /// <returns>Retorna o objeto POM da página de login para continuar interações e/ou validações</returns>
        public LoginPage Navegar_Para_Pagina()
        {
            Link_Para_Pagina_De_Login.Click();
            return new LoginPage();
        }

        /// <summary>
        /// Método para clicar no link referente à pagina de criação de conta navegando até ela
        /// </summary>
        /// <returns>Retorna o objeto POM da página de criação de conta para continuar interações e/ou validações</returns>
        public CriacaoDeContaPage Navegar_Para_Pagina_De_Criacao_De_Conta()
        {
            Link_Para_Pagina_Criar_Conta.Click();
            return new CriacaoDeContaPage();
        }

        /// <summary>
        /// Método para enviar de fato o formulário de solicitação de recuperação de senha
        /// </summary>
        /// <param name="usuario">Usuário da conta a ter sua senha recuperada</param>
        /// <param name="senha">E-mail da conta a ter sua senha recuperada</param>
        public void Efetuar_Solicitacao_De_Recuperacao_De_Senha(string usuario, string senha)
        {
            Input_Usuario.Clear();
            Input_Usuario.SendKeys(usuario);
            Input_Email.SendKeys(senha);
            Bto_Enviar.Click();
        }

        /// <summary>
        /// Valida se existe algum parágrafo (html p tag) contendo uma mensagem
        /// </summary>
        /// <param name="mensagem">Mensagem a ser verificada a existência na página dentro de uma tag html de parágrafo</param>
        /// <returns>Retorna um bool true ou false para validações</returns>
        public bool Valida_Exibicao_De_Mensagem_De_Tentativa_De_Recuperacao_De_Senha(string mensagem)
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//p[text()[contains(.,'" + mensagem + "')]]")).Displayed;
        }
        #endregion
    }
}
