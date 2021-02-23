using OpenQA.Selenium;
using Framework;
using Framework.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class LoginPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly LoginPageMap Map;

        public LoginPage()
        {
            Map = new LoginPageMap();
        }

        public LoginPage Login(string username, string password)
       {
           FW.Log.Step("Logging in");
           Map.Email.SendKeys(username);
           Map.Password.SendKeys(password);
           Map.LoginButton.Click();
           return this;
       }

    }

    //this class maps all the elements you need on this page
    public class LoginPageMap
    {
        public IWebElement Email => Driver.FindElement(By.Id("many_login_email"));

        public IWebElement Password => Driver.FindElement(By.Id("many_login_password"));

        public IWebElement LoginButton => Driver.FindElement(By.XPath("//*[contains(@aria-label,'Login link')]"));
        
        public IWebElement LoginPageTitle => Driver.FindElement(By.XPath("//*[h1]//*[contains(text(), 'Please enter your login details.')]"));
    }
}