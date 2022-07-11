using System.Threading;
using System.ComponentModel.Design;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class RegisterPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly RegisterPageMap Map;

        public RegisterPage() 
        {
            Map = new RegisterPageMap();
        }


         public RegisterPage CreateAccount(string Email, string Username, string Password)
       {   
           Map.Email.SendKeys(Email);
           Map.Username.SendKeys(Username);
           Map.Password.SendKeys(Password);
           Map.Button.Click();
           return this;
       }

         public RegisterPage AcceptCookies()
       {   
           Map.CookieBanner.Click();
           return this;
       }

          public RegisterPage Login(string Email, string Password)
       {   
           Map.Username.SendKeys(Email);
           Map.Password.SendKeys(Password);
           Map.Button.Click();
           Thread.Sleep(2000);
           return this;
       }

    }

    //this class maps all the elements you need on this page
    public class RegisterPageMap
    {
        //todo - use a better element (note no ID present, spaces in classname so won't pick up by  default)
        public IWebElement Button => Driver.FindElement(By.CssSelector("#primary > div > div.signup__steps > div > div > div > div.step-wrapper__content > div.signup-form.is-horizontal > div.card.logged-out-form > form > div.card.logged-out-form__footer.is-blended > button"));

        public IWebElement Username => Driver.FindElement(By.Id("username"));

        public IWebElement Email => Driver.FindElement(By.Id("email"));

        public IWebElement CookieBanner => Driver.FindElement(By.CssSelector("#wpcom > div > div.card.gdpr-banner.is-compact > div.gdpr-banner__buttons > button"));
        public IWebElement Password => Driver.FindElement(By.Id("password"));
    }
}