using Selenium.QuickStart.Nucleo;
using OpenQA.Selenium;
using System.Configuration;

namespace Mantis_Warley.Paginas
{
    /// <summary>
    /// <para>Classe para mapeamento de elementos da página (POM) de Login</para>
    /// <para>e métodos de interações e validações enquanto estiver nela</para>
    /// </summary>
    public class LoginPage
    {
        #region ObjetosDePaginaMapeados
        private IWebElement Input_Usuario => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("username"));
        private IWebElement Bto_Login => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//input[@type='submit' and @value='Entrar']"));
        private IWebElement Input_Senha => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("password"));
        private IWebElement Link_Para_Pagina_Criar_Conta => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'criar uma nova conta']"));
        #endregion

        #region MetodosParaInteracoesComApagina
        /// <summary>
        /// <para>Caso não estiver na página de Login (contendo 'login' na URL), navega até</para>
        /// <para>ela que nesse caso, é a URL definida na configuração do projeto</para>
        /// </summary>
        public void Navegar_Para_Pagina()
        {
            if (!GerenciadorDoWebDriver.Driver.Url.Contains("login"))
            {
                GerenciadorDoWebDriver.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL_BASE"]);
            }
        }

        /// <summary>
        /// <para>Verifica se está de fato na página de login do sistema através da</para>
        /// <para>existência de qualquer campo que tenha placeholder = 'Nome de usuário'</para>
        /// </summary>
        /// <returns>Retornando um bool true ou false para validações</returns>
        public bool Valida_Se_Esta_Na_Pagina()
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//*[@placeholder = 'Nome de usuário']")).Displayed;
        }

        /// <summary>
        /// Método para interações na página para digitar usuário, senha e clicar no botão de login
        /// </summary>
        /// <param name="usuario">Usuário a ser digitado na caixa de texto usuário da página</param>
        /// <param name="senha">Senha a ser digitada na caixa de texto de senha da página</param>
        public void Logar(string usuario, string senha)
        {
            Input_Usuario.SendKeys(usuario);
            Bto_Login.Click();
            Input_Senha.SendKeys(senha);
            Bto_Login.Click();
        }

        /// <summary>
        /// Verifica através de alguns elementos específicos da página para validar se o usuário está logado
        /// </summary>
        /// <returns>Retornando um bool true ou false para validações</returns>
        public bool Valida_Que_Esta_Logado()
        {
            GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("project-name"));
            GerenciadorDoWebDriver.ExecutaJavaScript("document.getElementById('project-name').value = 'teste'");
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//span[@class='user-info' and text()='"+ ConfigurationManager.AppSettings["USERNAME"] + "']")).Displayed;
        }

        /// <summary>
        /// Método para navegar ao cadastro de nova conta a partir da página de Login
        /// </summary>
        /// <returns>Retorna o objeto POM da página de criação de nova conta para continuar interações e/ou validações</returns>
        public CriacaoDeContaPage Navegar_Para_Pagina_De_Criacao_De_Conta()
        {
            Link_Para_Pagina_Criar_Conta.Click();
            return new CriacaoDeContaPage();
        }

        /// <summary>
        /// Verifica a existência de um texto dentro de uma tag html p na página
        /// </summary>
        /// <param name="texto">Texto a ser procurado dentro de um tag html p (parágrafo)</param>
        /// <returns>Retorna um bool true ou false para validações</returns>
        public bool Valida_Exibicao_De_Texto_Na_Pagina_Com_Case_Sensitive(string texto)
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//p[text()[contains(., '" + texto + "')]]")).Displayed;
        }

        /// <summary>
        /// Método para simular a digitação de um usuário e a navegação para o fluxo de recuperação de senha
        /// </summary>
        /// <param name="user">Parâmetro string com o usuário a ser digitado na caixa de texto para usuário na página de login</param>
        /// <returns>Retorna o objeto POM da página de recuperação de senha para continuar interações e/ou validações</returns>
        public RecuperarSenhaPage Digita_Usuario_E_Clica_Para_Navegar_Para_Recuperar_Senha(string user)
        {
            Input_Usuario.SendKeys(user);
            Bto_Login.Click();
            GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Perdeu a sua senha?']")).Click();
            return new RecuperarSenhaPage();
        }
        #endregion
    }
}
