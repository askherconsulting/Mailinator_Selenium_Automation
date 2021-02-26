using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class ResetPasswordPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly ResetPasswordPageMap Map;

        public ResetPasswordPage()
        {
            Map = new ResetPasswordPageMap();
        }

        public ResetPasswordPage clickLogin(IWebDriver driver)
       {
           Map.loginLink.Click();
           return this;
       }

    }

    //this class maps all the elements you need on this page
    public class ResetPasswordPageMap
    {

        public IWebElement passwordField => Driver.FindElement(By.Id("pass1"));

        public IWebElement loginLink => Driver.FindElement(By.XPath("//*[contains(text(), 'Log in')]"));


    }
}
