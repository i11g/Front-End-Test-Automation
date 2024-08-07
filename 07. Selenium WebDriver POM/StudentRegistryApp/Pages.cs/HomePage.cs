﻿using OpenQA.Selenium;


namespace StudentRegistryApp.Pages.cs
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base (driver)
        {
            
        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/";

        public IWebElement ElementStudentCount => driver.FindElement(By.XPath("//b"));
        //driver.FindElement(By.Csslocator("body>p>b")

        public int StudentsCount()
        {
            string studentsCount = ElementStudentCount.Text;
            return int.Parse(studentsCount);
        }
    }
}
