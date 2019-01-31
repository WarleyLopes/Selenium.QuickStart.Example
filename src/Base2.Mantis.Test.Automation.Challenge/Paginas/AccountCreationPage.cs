using Selenium.QuickStart.Nucleo;
using OpenQA.Selenium;

namespace Mantis_Warley.Paginas
{
    public class AccountCreationPage
    {
        private IWebElement TxtUser => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("username"));
        private IWebElement TxtEmail => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("email-field"));
        private IWebElement TxtCaptcha => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("captcha-field"));
        private IWebElement BtnCreateAccount => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//input[@type='submit' and @value='Criar Conta']"));
        private IWebElement LinkPassForgotten => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Perdeu a sua senha?']"));
        private IWebElement LinkLoginPage => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Entrar']"));

        public bool IsOnAccountCreationPage()
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//h4[text()[contains(.,'Criar Conta')]]")).Displayed;
        }

        public LoginPage NavigateToLoginPage()
        {
            LinkLoginPage.Click();
            return new LoginPage();
        }

        public void CreateAccountTypingWithTab(string user, string email, string captcha)
        {
            TxtUser.SendKeys(user + Keys.Tab);
            TxtEmail.SendKeys(email + Keys.Tab);
            TxtCaptcha.SendKeys(captcha);
            BtnCreateAccount.Click();
        }

        public bool IsSignUpAttemptMessageDisplayed(string message)
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//p[text()[contains(.,'"+message+"')]]")).Displayed;
        }

        public LostPassPage NavigateToLostPassPage()
        {
            LinkPassForgotten.Click();
            return new LostPassPage();
        }
    }
}