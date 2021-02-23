using System.IO;
using Framework.Selenium;
using OpenQA.Selenium;


namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class CreateAccountPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly CreateAccountPageMap Map;

        public CreateAccountPage()
        {
            Map = new CreateAccountPageMap();
        }

        public CreateAccountPage CreateAccount(string FirstName, string LastName, string Email, string Password)
       {
           Map.FirstName.SendKeys(FirstName);
           Map.LastName.SendKeys(LastName);
           Map.Email.SendKeys(Email);
           Map.Password.SendKeys(Password);
           Map.CreateAccountButton.Click();
           return this;
       }

        public CreateAccountPage ClickCreateAccountButton (IWebDriver driver)
       {
           Map.CreateAccountButton.Click();
           return this;
       }


    }

    //this class maps all the elements you need on this page
    public class CreateAccountPageMap
    {
        
        public IWebElement FirstName => Driver.FindElement(By.Id("first-name-su"));

        public IWebElement LastName => Driver.FindElement(By.Id("last-name-su"));

         public IWebElement Email => Driver.FindElement(By.Id("email-su"));

         public IWebElement Password => Driver.FindElement(By.Id("password-su"));

         public IWebElement CreateAccountButton => Driver.FindElements(By.XPath("//button[contains(text(),'Create account')]"))[1];

    }
}