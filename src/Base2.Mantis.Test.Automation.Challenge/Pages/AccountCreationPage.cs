using Selenium.QuickStart.Core;
using OpenQA.Selenium;

namespace Mantis_Warley.Pages
{
    public class AccountCreationPage
    {
        private IWebElement TxtUser => WebDriverHooks.Driver.FindElement(By.Id("username"));
        private IWebElement TxtEmail => WebDriverHooks.Driver.FindElement(By.Id("email-field"));
        private IWebElement TxtCaptcha => WebDriverHooks.Driver.FindElement(By.Id("captcha-field"));
        private IWebElement BtnCreateAccount => WebDriverHooks.Driver.FindElement(By.XPath("//input[@type='submit' and @value='Criar Conta']"));
        private IWebElement LinkPassForgotten => WebDriverHooks.Driver.FindElement(By.XPath("//a[text() = 'Perdeu a sua senha?']"));
        private IWebElement LinkLoginPage => WebDriverHooks.Driver.FindElement(By.XPath("//a[text() = 'Entrar']"));

        public bool IsOnAccountCreationPage()
        {
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//h4[text()[contains(.,'Criar Conta')]]")).Displayed;
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
            return WebDriverHooks.WaitForAndFindElement(By.XPath("//p[text()[contains(.,'"+message+"')]]")).Displayed;
        }

        public LostPassPage NavigateToLostPassPage()
        {
            LinkPassForgotten.Click();
            return new LostPassPage();
        }
    }
}