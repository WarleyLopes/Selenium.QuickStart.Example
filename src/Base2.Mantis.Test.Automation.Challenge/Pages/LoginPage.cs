using Selenium.QuickStart.Nucleo;
using OpenQA.Selenium;
using System.Configuration;

namespace Mantis_Warley.Pages
{
    public class LoginPage
    {
        private IWebElement TxtUser => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("username"));
        private IWebElement BtnLogin => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//input[@type='submit' and @value='Entrar']"));
        private IWebElement TxtPass => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("password"));
        private IWebElement LinkCreateAccount => GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'criar uma nova conta']"));

        public void NavigateToPage()
        {
            if (!GerenciadorDoWebDriver.Driver.Url.Contains("login"))
            {
                GerenciadorDoWebDriver.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL_BASE"]);
            }
        }

        public bool IsOnLoginPage()
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//*[@placeholder = 'Nome de usuário']")).Displayed;
        }

        public void Login(string user, string pass)
        {
            TxtUser.SendKeys(user);
            BtnLogin.Click();
            TxtPass.SendKeys(pass);
            BtnLogin.Click();
        }

        public bool IsLoggedIn()
        {
            GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.Id("project-name"));
            GerenciadorDoWebDriver.ExecutaJavaScript("document.getElementById('project-name').value = 'teste'");
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//span[@class='user-info' and text()='"+ ConfigurationManager.AppSettings["USERNAME"] + "']")).Displayed;
        }

        public AccountCreationPage NavigateToAccountCreationPageFromLoginPage()
        {
            LinkCreateAccount.Click();
            return new AccountCreationPage();
        }

        public bool IsErrorMessageCaseInsensitiveVisibleAnywherePartiallyOrNot(string message)
        {
            return GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//p[text()[contains(., '" + message + "')]]")).Displayed;
        }

        public LostPassPage TypeUserAndClickToRecoverForgottenPassword(string user)
        {
            TxtUser.SendKeys(user);
            BtnLogin.Click();
            GerenciadorDoWebDriver.ProcuraElementoAguardandoAparecer(By.XPath("//a[text() = 'Perdeu a sua senha?']")).Click();
            return new LostPassPage();
        }
    }
}
