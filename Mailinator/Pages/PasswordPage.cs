using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class PasswordPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly PasswordPageMap Map;

        public PasswordPage()
        {
            Map = new PasswordPageMap();
        }

        public PasswordPage enterPassword(string password)
       {          
           Map.passwordField.Clear();
           Map.passwordField.SendKeys(password);
           return this;
       }

       
        public PasswordPage clickResetPasswordButton(IWebDriver driver)
       {
           Map.button.Click();
           return this;
       }

    }

    //this class maps all the elements you need on this page
    public class PasswordPageMap
    {

        public IWebElement passwordField => Driver.FindElement(By.XPath("//*[@id='pass1']"));

        public IWebElement button => Driver.FindElement(By.Id("wp-submit"));

        public IWebElement passStrengthResult => Driver.FindElement(By.XPath("//*[@id='pass-strength-result'][text()='Strong']"));


    }
}
