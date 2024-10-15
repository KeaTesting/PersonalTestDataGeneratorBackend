using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PersonalTestDataGeneratorBackend;

namespace UnitTests
{
    public class NameTests
    {
        private Person person;

        public NameTests()
        {
            person = PersonHelper.GenerateData(1).First();
        }

        //tests a name does not contain a number
        [Fact]
        public void Firstname_Should_Not_Contain_Number()
        {
            //Arrange
            var firstName = person.Name;
            
            //Assert
            Assert.False(firstName.Any(char.IsDigit));
        }

        [Fact]
        public void Lastname_Should_Not_Contain_Number()
        {
            //Arrange
            var lastName = person.Surname;
            
            //Assert
            Assert.False(lastName.Any(char.IsDigit));
        }

        [Fact]
        public void Firstname_Is_Not_Empty()
        {
            //Arrange
            var firstName = person.Name;
           
            //Assert
            Assert.False(firstName.Length == 0);
        }

        [Fact]
        public void Lastname_Is_Not_Empty()
        {
            //Arrange
            var LastName = person.Surname;
            
            //Assert
            Assert.False(LastName.Length == 0);
        }

        [Fact]
        public void Has_Both_Firstname_And_Lastname()
        {
            //Arrange
            var firstName = person.Name;
            var lastName = person.Surname;
            bool result;

            //Act
            if (firstName.Length > 0 && lastName.Length > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Firstname_Does_Not_Contain_Special_Characters()
        {
            //Arrange
            var firstName = person.Name;
           
            //Assert
            Assert.Matches(@"^[a-zA-Z\s[.]+$", firstName);
        }

        [Fact]
        public void Lastname_Does_Not_Contain_Special_Characters()
        {
            //Arrange
            var lastName = person.Surname;

            //Assert
            Assert.Matches(@"^[a-zA-Z\s]+$", lastName);
        }

        [Fact]
        public void Whole_Name_Is_Valid_Lenght()
        {
            //Arrange
            var firstName = person.Name;
            var lastName = person.Surname;

            //Act
            var wholeName = firstName + " " + lastName;

            //Assert
            Assert.InRange(wholeName.Length, 2, 50);
        }
    }
}