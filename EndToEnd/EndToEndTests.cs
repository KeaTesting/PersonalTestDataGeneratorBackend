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

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);

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

            //assigning the element to a variable
            IWebElement cprElement = driver.FindElement(By.CssSelector(".cpr"));

            // Assert that the element with the id "cpr" is present
            Assert.NotNull(cprElement);
            driver.Close();
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

            //assigning the element to a variable
            IWebElement firstNameElement = driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = driver.FindElement(By.CssSelector(".lastName"));
            IWebElement dob = driver.FindElement(By.CssSelector(".dob"));

            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(dob);

            driver.Close();
        }

        [Fact]
        public void Return_a_fake_CPR_first_name_last_name_and_gender()
        {
            var driver = Driver();

            IWebElement partialGenerationElement = driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("cpr-name-gender");

            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            IWebElement firstNameElement = driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = driver.FindElement(By.CssSelector(".lastName"));
            IWebElement genderElement = driver.FindElement(By.CssSelector(".gender"));
            IWebElement cprElement = driver.FindElement(By.CssSelector(".cpr"));

            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(genderElement);
            Assert.NotNull(cprElement);

            driver.Close();
        }

        [Fact]
        public void Return_a_fake_CPR_first_name_last_name_and_gender_DOB()
        {

            var driver = Driver();

            IWebElement partialGenerationElement = driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("cpr-name-gender-dob");

            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            //assigning the element to a variable
            IWebElement cprElement = driver.FindElement(By.CssSelector(".cpr"));
            IWebElement firstNameElement = driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = driver.FindElement(By.CssSelector(".lastName"));
            IWebElement dob = driver.FindElement(By.CssSelector(".dob"));

            Assert.NotNull(cprElement);
            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(dob);


            driver.Close();
        }

        [Fact]
        public void Return_a_fake_Phone_Number()
        {
            var driver = Driver();

            IWebElement partialGenerationElement = driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("phone");

            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            //assigning the element to a variable
            IWebElement phoneElement = driver.FindElement(By.CssSelector(".phoneNumber"));

            Assert.NotNull(phoneElement);

            driver.Close();
        }

        [Fact]
        public void Return_Fake_Person()
        {
            var driver = Driver();

            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            IWebElement cprElement = driver.FindElement(By.CssSelector(".cpr"));
            IWebElement firstNameElement = driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = driver.FindElement(By.CssSelector(".lastName"));
            IWebElement genderElement = driver.FindElement(By.CssSelector(".gender"));
            IWebElement dob = driver.FindElement(By.CssSelector(".dob"));
            IWebElement phoneElement = driver.FindElement(By.CssSelector(".phoneNumber"));

            Assert.NotNull(cprElement);
            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(genderElement);
            Assert.NotNull(dob);
            Assert.NotNull(phoneElement);

            driver.Close();
        }

        [InlineData(2)]
        [InlineData(10)]
        [InlineData(100)]
        [Theory]
        public void Return_More_Than_One_Fake_Persons(int amount)
        {
            var driver = Driver();

            driver.FindElement(By.CssSelector("#txtNumberPersons")).Clear();
            driver.FindElement(By.CssSelector("#txtNumberPersons")).SendKeys(amount.ToString());

            IWebElement submitButton = driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            IReadOnlyCollection<IWebElement> cprElements = driver.FindElements(By.CssSelector(".cpr"));
            IReadOnlyCollection<IWebElement> firstNameElements = driver.FindElements(By.CssSelector(".firstName"));
            IReadOnlyCollection<IWebElement> lastNameElements = driver.FindElements(By.CssSelector(".lastName"));
            IReadOnlyCollection<IWebElement> genderElement = driver.FindElements(By.CssSelector(".gender"));
            IReadOnlyCollection<IWebElement> dob = driver.FindElements(By.CssSelector(".dob"));
            IReadOnlyCollection<IWebElement> phoneElement = driver.FindElements(By.CssSelector(".phoneNumber"));

            Assert.Equal(amount, cprElements.Count);
            Assert.Equal(amount, firstNameElements.Count);
            Assert.Equal(amount, lastNameElements.Count);
            Assert.Equal(amount, genderElement.Count);
            Assert.Equal(amount, dob.Count);
            Assert.Equal(amount, phoneElement.Count);

            driver.Close();
        }
    }
}