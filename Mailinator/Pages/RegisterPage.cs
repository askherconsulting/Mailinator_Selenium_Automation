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


         public RegisterPage CreateAccount(string Username, string Email)
       {   
           Map.Username.SendKeys(Username);
           Map.Email.SendKeys(Email);
           Map.Button.Click();
           return this;
       }

          public RegisterPage Login(string Email, string Password)
       {   
           Map.Username.SendKeys(Email);
           Map.Password.SendKeys(Password);
           Map.Button.Click();
           return this;
       }

    }

    //this class maps all the elements you need on this page
    public class RegisterPageMap
    {
 
        public IWebElement Button => Driver.FindElement(By.Id("wp-submit"));

        public IWebElement Username => Driver.FindElement(By.Id("user_login"));

        public IWebElement Email => Driver.FindElement(By.Id("user_email"));
        public IWebElement Password => Driver.FindElement(By.Id("user_pass"));
    }
}