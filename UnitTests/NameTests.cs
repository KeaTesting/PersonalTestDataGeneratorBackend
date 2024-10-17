using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PersonalTestDataGeneratorBackend;
using PersonalTestDataGeneratorBackend.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UnitTests
{
    public class NameTests
    {
        private readonly Person _person;

        public NameTests()
        {
            var query = new PersonQuery()
            {
                Name = true,
                Surname = true,
            };
            _person = PersonHelper.GeneratePersons(query,1).First();
        }

        //tests a name does not contain a number
        [Fact]
        public void Firstname_Should_Not_Contain_Number()
        {
            //Arrange
            var firstName = _person.Name;
            
            //Assert
            Assert.False(firstName.Any(char.IsDigit));
        }

        [Fact]
        public void Lastname_Should_Not_Contain_Number()
        {
            //Arrange
            var lastName = _person.Surname;
            
            //Assert
            Assert.False(lastName.Any(char.IsDigit));
        }

        [Fact]
        public void Firstname_Is_Not_Empty()
        {
            //Arrange
            var firstName = _person.Name;
           
            //Assert
            Assert.False(firstName.Length == 0);
        }

        [Fact]
        public void Lastname_Is_Not_Empty()
        {
            //Arrange
            var LastName = _person.Surname;
            
            //Assert
            Assert.False(LastName.Length == 0);
        }

        [Fact]
        public void Has_Both_Firstname_And_Lastname()
        {
            //Arrange
            var firstName = _person.Name;
            var lastName = _person.Surname;

            //Assert
            Assert.True(firstName.Length > 0 && lastName.Length > 0);
        }

        [Fact]
        public void Firstname_Does_Not_Contain_Special_Characters()
        {
            //Arrange
            var firstName = _person.Name;

            //Assert
            Assert.Matches(@"^[a-zA-ZæøåÆØÅ\s[.]+$", firstName);
        }

        [Fact]
        public void Lastname_Does_Not_Contain_Special_Characters()
        {
            //Arrange
            var lastName = _person.Surname;

            //Assert
            Assert.Matches(@"^[a-zA-ZæøåÆØÅ\s]+$", lastName);
        }

        [Fact]
        public void Whole_Name_Is_Valid_Lenght()
        {
            //Arrange
            var firstName = _person.Name;
            var lastName = _person.Surname;

            //Act
            var wholeName = firstName + " " + lastName;

            //Assert
            Assert.InRange(wholeName.Length, 2, 50);
        }
    }
}