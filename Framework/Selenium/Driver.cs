using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Framework.Selenium
{

    public static class Driver
    {
        [ThreadStatic]
        public static IWebDriver _driver;

        public static void Init()
        {
            _driver = new ChromeDriver(FW.WORKSPACE_DIRECTORY + "_drivers");
        }

        public static void Goto(string url)
        {
            if (!url.StartsWith("http"))
            {
                url = $"http://{url}";
            }

            Debug.WriteLine(url);
            Current.Navigate().GoToUrl(url);
        }

        public static IWebElement FindElement(By by)
            {

                return Current.FindElement(by);
            }

        public static IList<IWebElement> FindElements(By by)
            {
                return Current.FindElements(by);
            }

         public static void Quit()
        {
            Current.Quit();
        }


        public static IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is null");
    }
}