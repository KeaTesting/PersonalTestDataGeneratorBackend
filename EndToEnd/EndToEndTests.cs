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

        private readonly IWebDriver _driver;

        public EndToEndTests()
        {
            // Initialize the Chrome WebDriver
            _driver = new ChromeDriver();

            // Open the website
            _driver.Navigate().GoToUrl("http://127.0.0.1:5500");

            // Set window size
            _driver.Manage().Window.Size = new System.Drawing.Size(949, 743);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);
        }


        [Fact]
        public void ReturnAFakeCPR()
        {
            //change to partial generation
            IWebElement partialGenerationElement = _driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            // Find the element by CSS selector and click
            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            //assigning the element to a variable
            IWebElement cprElement = _driver.FindElement(By.CssSelector(".cpr"));

            // Assert that the element with the id "cpr" is present
            Assert.NotNull(cprElement);
            _driver.Close();
        }

        [Fact]
        public void Return_a_fake_first_name_last_name_and_gender()
        {

            IWebElement partialGenerationElement = _driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("name-gender");

            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            //assigning the element to a variable
            IWebElement firstNameElement = _driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = _driver.FindElement(By.CssSelector(".lastName"));
            IWebElement dob = _driver.FindElement(By.CssSelector(".dob"));

            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(dob);

            _driver.Close();
        }

        [Fact]
        public void Return_a_fake_CPR_first_name_last_name_and_gender()
        {
            IWebElement partialGenerationElement = _driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("cpr-name-gender");

            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            IWebElement firstNameElement = _driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = _driver.FindElement(By.CssSelector(".lastName"));
            IWebElement genderElement = _driver.FindElement(By.CssSelector(".gender"));
            IWebElement cprElement = _driver.FindElement(By.CssSelector(".cpr"));

            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(genderElement);
            Assert.NotNull(cprElement);

            _driver.Close();
        }

        [Fact]
        public void Return_a_fake_CPR_first_name_last_name_and_gender_DOB()
        {

            IWebElement partialGenerationElement = _driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("cpr-name-gender-dob");

            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            //assigning the element to a variable
            IWebElement cprElement = _driver.FindElement(By.CssSelector(".cpr"));
            IWebElement firstNameElement = _driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = _driver.FindElement(By.CssSelector(".lastName"));
            IWebElement dob = _driver.FindElement(By.CssSelector(".dob"));

            Assert.NotNull(cprElement);
            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(dob);

            _driver.Close();
        }

        [Fact]
        public void Return_a_fake_Phone_Number()
        {
            IWebElement partialGenerationElement = _driver.FindElement(By.CssSelector("#partialOptions > input"));
            partialGenerationElement.Click();

            //Selecting a value from the dropdown
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.CssSelector("#cmbPartialOptions")));
            selectElement.SelectByValue("phone");

            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            //assigning the element to a variable
            IWebElement phoneElement = _driver.FindElement(By.CssSelector(".phoneNumber"));

            Assert.NotNull(phoneElement);

            _driver.Close();
        }

        [Fact]
        public void Return_Fake_Person()
        {
            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            IWebElement cprElement = _driver.FindElement(By.CssSelector(".cpr"));
            IWebElement firstNameElement = _driver.FindElement(By.CssSelector(".firstName"));
            IWebElement lastNameElement = _driver.FindElement(By.CssSelector(".lastName"));
            IWebElement genderElement = _driver.FindElement(By.CssSelector(".gender"));
            IWebElement dob = _driver.FindElement(By.CssSelector(".dob"));
            IWebElement phoneElement = _driver.FindElement(By.CssSelector(".phoneNumber"));

            Assert.NotNull(cprElement);
            Assert.NotNull(firstNameElement);
            Assert.NotNull(lastNameElement);
            Assert.NotNull(genderElement);
            Assert.NotNull(dob);
            Assert.NotNull(phoneElement);

            _driver.Close();
        }

        [InlineData(2)]
        [InlineData(10)]
        [InlineData(100)]
        [Theory]
        public void Return_More_Than_One_Fake_Persons(int amount)
        {
            _driver.FindElement(By.CssSelector("#txtNumberPersons")).Clear();
            _driver.FindElement(By.CssSelector("#txtNumberPersons")).SendKeys(amount.ToString());

            IWebElement submitButton = _driver.FindElement(By.CssSelector("#submit > input"));
            submitButton.Click();

            IReadOnlyCollection<IWebElement> cprElements = _driver.FindElements(By.CssSelector(".cpr"));
            IReadOnlyCollection<IWebElement> firstNameElements = _driver.FindElements(By.CssSelector(".firstName"));
            IReadOnlyCollection<IWebElement> lastNameElements = _driver.FindElements(By.CssSelector(".lastName"));
            IReadOnlyCollection<IWebElement> genderElement = _driver.FindElements(By.CssSelector(".gender"));
            IReadOnlyCollection<IWebElement> dob = _driver.FindElements(By.CssSelector(".dob"));
            IReadOnlyCollection<IWebElement> phoneElement = _driver.FindElements(By.CssSelector(".phoneNumber"));

            Assert.Equal(amount, cprElements.Count);
            Assert.Equal(amount, firstNameElements.Count);
            Assert.Equal(amount, lastNameElements.Count);
            Assert.Equal(amount, genderElement.Count);
            Assert.Equal(amount, dob.Count);
            Assert.Equal(amount, phoneElement.Count);

            _driver.Close();
        }
    }
}