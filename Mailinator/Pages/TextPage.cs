using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class TextPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly TextPageMap Map;

        public TextPage()
        {
            Map = new TextPageMap();
        }

        //note raised issue that text link cannot be navigated to with Mailinator directly
        public TextPage clickLink(string linkText)
       {
           Map.Text.Click();
           return this;
       }
    }

    //this class maps all the elements you need on this page
    public class TextPageMap
    {

        public IWebElement Text => Driver.FindElement(By.XPath("//*[contains(text(),linkText)]"));

    }
}
