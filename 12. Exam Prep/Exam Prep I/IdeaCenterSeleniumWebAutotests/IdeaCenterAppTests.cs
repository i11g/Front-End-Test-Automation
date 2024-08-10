using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace IdeaCenterSeleniumWebAutotests
{
    [TestFixture]
    public class IdeaCenterAppTests
    {
        private IWebDriver driver;
        private readonly string BASEURL = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83";

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl(BASEURL+ "/Users/Login");
            driver.FindElement(By.XPath("//input[@id='typeEmailX-2']")).SendKeys("iv1234@test.com");
            driver.FindElement(By.XPath("//input[@id='typePasswordX-2']")).SendKeys("123456");
            driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg btn-block']")).Click();
        }

        [OneTimeTearDown]

        public void OneTimetearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void Create_Idea_With_InvalidDataTest()
        {
            driver.Navigate().GoToUrl(BASEURL + "/Ideas/Create");
            string title = "";
            string description = "";

            var titleField = driver.FindElement(By.Id("form3Example1c"));
            titleField.SendKeys(title);
            var descriptionField = driver.FindElement(By.Id("form3Example4cd"));
            descriptionField.SendKeys(description);
           
            Assert.That(titleField.Text, Is.EqualTo(""));
            Assert.That(descriptionField.Text, Is.EqualTo(""));

            driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']")).Click();

            var pageUrl = driver.Url;

            Assert.That(pageUrl, Is.EqualTo(BASEURL + "/Ideas/Create"), "Page Url is not correct");

            var mainMessage=driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//li"));
            Assert.That(mainMessage.Text, Is.EqualTo("Unable to create new Idea!"), "Main message is not correct");

            var titleMessage = driver.FindElement(By.XPath("//div[@style='text-align:center']//span[contains(text(), 'The Title field')]"));
            Assert.That(titleMessage.Text, Is.EqualTo("The Title field is required."), "Title message is not correct");

            var descriptionMessage = driver.FindElement(By.XPath("//div[@style='text-align:center']//span[contains(text(),'The Description field')]"));
            Assert.That(descriptionMessage.Text, Is.EqualTo("The Description field is required."), "Description message is not correct");
        }
        [Test, Order(2)] 

        public void Create_Random_Idea_Test()
        {
            driver.Navigate().GoToUrl(BASEURL + "/Ideas/Create");
            var title = GenerateRandomString(6);
            var description ="Description " + GenerateRandomString(10); 

            driver.FindElement(By.Id("form3Example1c")).SendKeys(title);
            driver.FindElement(By.Id("form3Example4cd")).SendKeys(description);
            driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']")).Click();

            var pageUrl = driver.Url;
            Assert.That(pageUrl, Is.EqualTo(BASEURL + "/Ideas/MyIdeas"));

            ReadOnlyCollection<IWebElement> createdIdeas=driver.FindElements(By.CssSelector(".card.mb-4.box-shadow"));
            IWebElement lastIdea = createdIdeas.Last();
            var lastIdeaText=lastIdea.FindElement(By.XPath(".//div[@class='card-body']//p"));

            Assert.That(lastIdeaText.Text, Is.EqualTo(description));

        } 
        
        public string GenerateRandomString(int length)
        {
            const string chars = "hskjghskjhgdskjhgsjdkghdsjkghsdkjg";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).
                Select(s => s[random.Next(s.Length)]).ToArray());
        } 
    }
}