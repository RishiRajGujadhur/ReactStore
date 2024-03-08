using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Store.SpecflowBDD.AutomationTest.StepDefinitions
{
    [Binding]
    public sealed class HomepageStepDefinitions
    {
        private IWebDriver driver;

        [Given(@"Open the browser")]
        public void GivenOpenTheBrowser()
        {
           driver = new ChromeDriver();
           driver.Manage().Window.Maximize();
        }

        [When(@"Enter the URL")]
        public void WhenEnterTheURL()
        {
            driver.Url = "http://localhost:3000/";
            Thread.Sleep(5000);
        }

        [Then(@"Verify that homepage title matched")]
        public void ThenVerifyThatHomepageShows()
        {
            string pageTitle = driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div")).Text;
            Assert.That(pageTitle, Is.EqualTo("Welcome to the store"));
            driver.Quit();
        }
    }
}
