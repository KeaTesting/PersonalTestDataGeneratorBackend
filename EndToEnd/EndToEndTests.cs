using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace EndToEnd
{
    public class EndToEndTests
    {
        [Fact]
        public void ReturnAFakeCPR()
        {
            // Initialize the Chrome WebDriver
            IWebDriver driver = new ChromeDriver();

            // Open the website
            driver.Navigate().GoToUrl("http://127.0.0.1:5500");

            // Set window size
            driver.Manage().Window.Size = new System.Drawing.Size(949, 743);

            // Find the element by CSS selector and click
            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            //submitButton.Click();

            // Wait for the element with the id "cpr" to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));


            //assigning the element to a variable
            IWebElement cprElement = wait.Until(driver => driver.FindElement(By.CssSelector(".cpr")));

            // Assert that the element with the id "cpr" is present
            Assert.NotNull(cprElement);

        }
    }
}