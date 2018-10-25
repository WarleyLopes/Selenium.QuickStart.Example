using Selenium.QuickStart.Core;
using OpenQA.Selenium;

namespace Mantis_Warley.Pages
{
    public class LostPassPage
    {
        private IWebElement TxtUser => WebDriverHooks.Driver.FindElement(By.Id("username"));
        private IWebElement TxtEmail => WebDriverHooks.Driver.FindElement(By.Id("email-field"));
        private IWebElement BtnSend => WebDriverHooks.Driver.FindElement(By.XPath("//input[@type='submit' and @value='Enviar']"));
        private IWebElement LinkLoginPage => WebDriverHooks.Driver.FindElement(By.XPath("//a[text() = 'Entrar']"));
        private IWebElement LinkCreateAccount => WebDriverHooks.Driver.FindElement(By.XPath("//a[text() = 'criar uma nova conta']"));

        public bool IsOnLostPassPage()
        {
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//h4[text()[contains(.,'Reajuste de Senha')]]")).Displayed;
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
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//p[text()[contains(.,'" + message + "')]]")).Displayed;
        }
    }
}
