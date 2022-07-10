using OpenQA.Selenium;
using Framework;
using Framework.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class HomePage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly HomePageMap Map;

        public HomePage()
        {
            Map = new HomePageMap();
        }

/* this code comes from Carlos Kidmans course and is an example of how to get rid of an accept cookies prompt by waiting for it to not be visible anymore
       
        public void AcceptCookies()
        {
            Map.AcceptCookiesButton.Click();
            Driver.Wait.Until(drvr => !Map.AcceptCookiesButton.Displayed);
        }

*/

        public HomePage EnterBookingDetails( string phone, string subject, string description)
       {
  
           Map.Phone.SendKeys(phone);
           Map.Subject.SendKeys(subject);
           Map.Description.SendKeys(description);
           Map.SubmitButton.Click();
           return this;
       }

           public HomePage clickLoginButton()
       {
           Map.LoginButton.Click();
           return this;
       }

        public HomePage Logout()
       {
           Map.LogoutButton.Click();
           return this;
       }
    }
   
     //this class maps all the elements you need on this page
    public class HomePageMap
    {
 
        public IWebElement Phone => Driver.FindElement(By.Id("phone"));

        public IWebElement Subject => Driver.FindElement(By.Id("subject"));

        public IWebElement SubmitButton => Driver.FindElement(By.Id("submitContact"));

        public IWebElement Description => Driver.FindElement(By.Id("description"));

        public IWebElement LoginButton => Driver.FindElement(By.PartialLinkText("Login"));

        public IWebElement LogoutButton => Driver.FindElement(By.PartialLinkText("Logout"));

        //relative locator example
        //public IWebElement LogoutButton => Driver.FindElement(RelativeBy.WithTagName("li").RightOf(By.ClassName("dropdown")));
        /*
        /the same command in Java would be:-
        / public IWebElement LogoutButtonJava => Driver.FindElement(withTagName("li").toRightOf(By.ClassName("dropdown")));
        */
    }
}