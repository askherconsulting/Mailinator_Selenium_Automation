using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Framework.Selenium
{
    public static class DriverFactory
    {
        public static IWebDriver Build(string browserName)
        {
            FW.Log.Info($"Browser: {browserName}");

            switch (browserName)
            {
                case "chrome":
                    return new ChromeDriver(FW.WORKSPACE_DIRECTORY + "_drivers");

                case "firefox":
                    return new FirefoxDriver(FW.WORKSPACE_DIRECTORY + "_drivers");
                case "edge":
                    return new FirefoxDriver(FW.WORKSPACE_DIRECTORY + "_drivers");
                default:
                    throw new System.ArgumentException($"{browserName} not supported.");
            }
        }
    }
}