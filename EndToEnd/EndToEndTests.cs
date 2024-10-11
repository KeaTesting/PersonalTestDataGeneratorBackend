using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EndToEnd
{
    public class EndToEndTests
    {

        private IWebDriver Driver()
        {

            // Initialize the Chrome WebDriver
            IWebDriver driver = new ChromeDriver();

            // Open the website
            driver.Navigate().GoToUrl("http://127.0.0.1:5500");

            // Set window size
            driver.Manage().Window.Size = new System.Drawing.Size(949, 743);

            return driver;
        }


        [Fact]
        public void ReturnAFakeCPR()
        {
            IWebDriver driver = Driver();

            //change to partial generation
            IWebElement partialGenerationElement = driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            // Find the element by CSS selector and click
            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            // Wait for the element with the id "cpr" to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));


            //assigning the element to a variable
            IWebElement cprElement = wait.Until(driver => driver.FindElement(By.CssSelector(".cpr")));

            // Assert that the element with the id "cpr" is present
            Assert.NotNull(cprElement);
        }

        [Fact]
        public void Return_a_fake_first_name_last_name_and_gender()
        {

            var driver = Driver();

            IWebElement partialGenerationElement = driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("name-gender");

            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();


            // Wait for the element with the id "cpr" to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            //assigning the element to a variable
            IWebElement firstNameElement = wait.Until(driver => driver.FindElement(By.CssSelector(".firstName")));
            IWebElement lastNameElement = wait.Until(driver => driver.FindElement(By.CssSelector(".lastName")));
            IWebElement dob = wait.Until(driver => driver.FindElement(By.CssSelector(".dob")));

            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(dob);



        }
    }
}