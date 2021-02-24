using Framework.Selenium;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class InboxPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly InboxPageMap Map;

        public InboxPage()
        {
            Map = new InboxPageMap();
        }

        public InboxPage openEmail(IWebElement email)
       {
           Driver.Wait.Until(WaitConditions.ElementDisplayed(email));
           email.Click();
           return this;
       }

         public InboxPage selectInbox(string inboxName)
       {
           Map.inboxName.Clear();
           Map.inboxName.SendKeys(inboxName);
           Map.inboxName.SendKeys(Keys.Enter);
           return this;
       }
    }

    //this class maps all the elements you need on this page
    public class InboxPageMap
    {
 
        public IWebElement emailWP => Driver.FindElement(By.XPath("//*[contains(text(),'WordPress')]"));

        public IWebElement emailSW => Driver.FindElement(By.XPath("//*[contains(text(),'TEST - Alert')]"));
        public IWebElement inboxName => Driver.FindElement(By.Id("inbox_field"));

    }
}
