using Framework;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Framework.Selenium;
using Config.Base;
using OpenQA.Selenium.Chrome;


namespace Tests.Base
{
    public abstract class TestBase : ConfigBase
    {
 

        [OneTimeSetUp]
        public virtual void BeforeAll()
        {
            FW.CreateTestResultsDirectory();
        }

        [SetUp]
        public virtual void BeforeEach(){
        
            FW.SetLogger();
            Driver.Init();
            Driver.Current.Manage().Window.Maximize();
        }

        [TearDown]
        public virtual void AfterEach()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;

            if (outcome == TestStatus.Passed)
            {
                FW.Log.Info("Outcome: Passed");
            }
            else if (outcome == TestStatus.Failed)
            {
                FW.Log.Info("Outcome: Failed");
            }
            else
            {
                FW.Log.Warning("Outcome: " + outcome);
            }

            Driver.Current.Quit();
        }
    }
}