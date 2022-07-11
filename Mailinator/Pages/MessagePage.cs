using System.Threading;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class MessagePage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly MessagePageMap Map;

        public MessagePage()
        {
            Map = new MessagePageMap();
        }

        public MessagePage clickButton(IWebDriver driver)
       {
           Map.button.Click();
           return this;
       }

        public MessagePage clickViewEvent(IWebDriver driver)
       {
           Map.viewEventButton.Click();
           return this;
       }

        public MessagePage clickTextLink(IWebDriver driver)
       {
           Map.textLink.Click();
           return this;
       }
    }

    //this class maps all the elements you need on this page
    public class MessagePageMap
    {
 
        public IWebElement button => Driver.FindElement(By.XPath("//*[contains(text(),'login=')]"));

        public IWebElement viewEventButton => Driver.FindElement(By.PartialLinkText("View event"));

         public IWebElement textLink => Driver.FindElement(By.XPath("//*[contains(text(),'Confirm Now')]"));
        

    }
}
