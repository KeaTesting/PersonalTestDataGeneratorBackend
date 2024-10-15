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
        List<Person> people = PersonHelper.GenerateData(1);

        //tests a name does not contain a number
        [Fact]
        public void Firstname_Should_Not_Contain_Number()
        {
            //Arrange
            var firstName = people[0].Name;
            bool result;

            //Act
            if (firstName.Any(char.IsDigit))
            {
                result = true;
            }else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Lastname_Should_Not_Contain_Number()
        {
            //Arrange
            var firstName = people[0].Surname;
            bool result;

            //Act
            if (firstName.Any(char.IsDigit))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void Name_Is_Not_Empty()
        {
            //Arrange
            var firstName = people[0].Name;
            bool result;

            //Act
            if (firstName.Length == 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Surname_Is_Not_Empty()
        {
            //Arrange
            var firstName = people[0].Surname;
            bool result;

            //Act
            if (firstName.Length == 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Has_Both_Firstname_And_Lastname()
        {
            //Arrange
            var firstName = people[0].Name;
            var lastName = people[0].Surname;
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
        public void Name_Does_Not_Contain_Special_Characters()
        {
            //Arrange
            var firstName = people[0].Name;
            bool result;

            //Act
            result = Regex.IsMatch(firstName, @"^[a-zA-Z\s]+$"); 

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Surname_Does_Not_Contain_Special_Characters()
        {
            //Arrange
            var firstName = people[0].Surname;
            bool result;

            //Act
            if (firstName.Any(char.IsSymbol) || firstName.Any(char.IsPunctuation))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Whole_Name_Is_Valid_Lenght()
        {
            //Arrange
            var firstName = people[0].Name;
            var lastName = people[0].Surname;

            //Act
            var wholeName = firstName + " " + lastName;

            //Assert
            Assert.InRange(wholeName.Length, 2, 50);
        }
    }
}
