using Selenium.QuickStart.Nucleo;
using OpenQA.Selenium;

namespace Mantis_Warley.Paginas
{
    public class LostPassPage
    {
        private IWebElement TxtUser => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("username"));
        private IWebElement TxtEmail => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("email-field"));
        private IWebElement BtnSend => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//input[@type='submit' and @value='Enviar']"));
        private IWebElement LinkLoginPage => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Entrar']"));
        private IWebElement LinkCreateAccount => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'criar uma nova conta']"));

        public bool IsOnLostPassPage()
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//h4[text()[contains(.,'Reajuste de Senha')]]")).Displayed;
        }

        public LoginPage NavigateToLoginPage()
        {
            LinkLoginPage.Click();
            return new LoginPage();
        }

        public AccountCreationPage NavigateToAccountCreationPage()
        {
            LinkCreateAccount.Click();
            return new AccountCreationPage();
        }

        public void RequestPassRecovery(string user, string email)
        {
            TxtUser.Clear();
            TxtUser.SendKeys(user);
            TxtEmail.SendKeys(email);
            BtnSend.Click();
        }

        public bool IsRequestPassRecoveryAttemptMessageDisplayed(string message)
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//p[text()[contains(.,'" + message + "')]]")).Displayed;
        }
    }
}
