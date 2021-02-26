using System.Threading;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class WordPressAdminPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly WordPressAdminPageMap Map;

        public WordPressAdminPage()
        {
            Map = new WordPressAdminPageMap();
        }

        public WordPressAdminPage clickResetPasswordButton(IWebDriver driver)
       {
           Map.button.Click();
           return this;
       }

    }

    //this class maps all the elements you need on this page
    public class WordPressAdminPageMap
    {

        public IWebElement button => Driver.FindElement(By.Id("wp-submit"));

        public IWebElement passStrengthResult => Driver.FindElement(By.XPath("//*[@id='pass-strength-result'][text()='Strong']"));
    }
}
