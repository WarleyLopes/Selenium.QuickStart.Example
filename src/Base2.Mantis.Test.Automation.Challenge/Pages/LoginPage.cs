using Selenium.QuickStart.Core;
using OpenQA.Selenium;
using System.Configuration;

namespace Mantis_Warley.Pages
{
    public class LoginPage
    {
        private IWebElement TxtUser => WebDriverHooks.Driver.FindElement(By.Id("username"));
        private IWebElement BtnLogin => WebDriverHooks.Driver.FindElement(By.XPath("//input[@type='submit' and @value='Entrar']"));
        private IWebElement TxtPass => WebDriverHooks.Driver.FindElement(By.Id("password"));
        private IWebElement LinkCreateAccount => WebDriverHooks.Driver.FindElement(By.XPath("//a[text() = 'criar uma nova conta']"));

        public void NavigateToPage()
        {
            if (!WebDriverHooks.Driver.Url.Contains("login"))
            {
                WebDriverHooks.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL_BASE"]);
            }
        }

        public bool IsOnLoginPage()
        {
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//*[@placeholder = 'Nome de usuário']")).Displayed;
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
            WebDriverHooks.WaitForAndFindElement(By.Id("project-name"));
            WebDriverHooks.ExecuteJavaScript("document.getElementById('project-name').value = 'teste'");
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//span[@class='user-info' and text()='"+ ConfigurationManager.AppSettings["USERNAME"] + "']")).Displayed;
        }

        public AccountCreationPage NavigateToAccountCreationPageFromLoginPage()
        {
            LinkCreateAccount.Click();
            return new AccountCreationPage();
        }

        public bool IsErrorMessageCaseInsensitiveVisibleAnywherePartiallyOrNot(string message)
        {
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//p[text()[contains(., '" + message + "')]]")).Displayed;
        }

        public LostPassPage TypeUserAndClickToRecoverForgottenPassword(string user)
        {
            TxtUser.SendKeys(user);
            BtnLogin.Click();
            WebDriverHooks.WaitForAndFindElement(By.XPath("//a[text() = 'Perdeu a sua senha?']")).Click();
            return new LostPassPage();
        }
    }
}
