﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Exercise_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebDriver driver = new ChromeDriver();

            //WebDriver driver = new FirefoxDriver();

            driver.Url = "https://www.wikipedia.org/";

            //Find element

            //var searchInput = driver.FindElement(By.Id("searchInput")); 

            //Click on the element

            //searchInput.Click();
            // Console.WriteLine(searchInput.GetCssValue("background-color"));
            //Console.WriteLine(searchInput.GetDomProperty("size"));


            //Type Quality Assurance

            //searchInput.SendKeys("Quality Assurance" + Keys.Enter);

            //Find element by cSSlocator

            //driver.FindElement(By.CssSelector("#js-link-box-en")).Click();
            //driver.FindElement(By.LinkText("Alan Wace"));

            driver.FindElement(By.XPath("//*[@id='js-link-box-es']/strong")).Click();
            driver.FindElement(By.XPath("//*[@id='Primer_franquismo']/a")).Click();

            //Find pageTitle
            var pageTitle = driver.Title;

            Console.WriteLine(pageTitle); 

            //Quit

            driver.Quit();


        }
    }
}
