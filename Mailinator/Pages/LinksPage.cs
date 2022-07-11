using Framework.Selenium;
using OpenQA.Selenium;

namespace Mailinator.Pages
{
    //this class uses : to inherit the classes within the abstract PageBase class
    public class LinksPage : PageBase
    {
        //this class does things with the mapped elements listed below
        public readonly LinksPageMap Map;

        public LinksPage()
        {
            Map = new LinksPageMap();
        }

        public LinksPage viewLinks(IWebDriver driver)
       {
           Map.links.ToString();
           return this;
       }
    }

    //this class maps all the elements you need on this page
    public class LinksPageMap
    {

        public IWebElement email => Driver.FindElement(By.XPath("//*[contains(text(),subject)]"));

        public IWebElement links => Driver.FindElement(By.Id("pills-links-tab"));

    }
}
