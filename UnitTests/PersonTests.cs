using Moq;
using PersonalTestDataGeneratorBackend;
using PersonalTestDataGeneratorBackend.Generators;
using PersonalTestDataGeneratorBackend.Models;
using PersonalTestDataGeneratorBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class PersonTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(10000)]
        public void GeneratePersons_ShouldSetAmountFromQuery(int amount)
        {
            //Arrange
            var query = new PersonQuery();

            //Act
            var people = PersonHelper.GeneratePersons(query, amount);

            //Assert
            Assert.True(people.Count == amount);
        }

        [Fact]
        public void GeneratePersons_HaveCprSetWhenQueryIsSet()
        {
            //Arrange
            var query = new PersonQuery() { Cpr = true};

            //Act
            var person = PersonHelper.GeneratePersons(query).First();

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(person.Cpr));
        }
        [Fact]
        public void GeneratePersons_HaveGenderSetWhenQueryIsSet()
        {
            //Arrange
            var query = new PersonQuery() { Gender = true };

            //Act
            var person = PersonHelper.GeneratePersons(query).First();

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(person.Gender));
        }

        [Fact]
        public void GeneratePersons_HaveBirthdaySetWhenQueryIsSet()
        {
            //Arrange
            var query = new PersonQuery() { Birthday = true };

            //Act
            var person = PersonHelper.GeneratePersons(query).First();

            //Assert
            Assert.True(person.Birthday.HasValue);
        }

        [Fact]
        public void GeneratePersons_HaveNameSetWhenQueryIsSet()
        {
            //Arrange
            var query = new PersonQuery() { Name = true };

            //Act
            var person = PersonHelper.GeneratePersons(query).First();

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(person.Name));
        }

        [Fact]
        public void GeneratePersons_HaveAddressSetWhenQueryIsSet()
        {
            //Arrange
            var _postalCodesRepoMock = new Mock<PostalCodesRepository>();
            var returnvalue = new List<PostalCode>() {
                    new PostalCode
                    {
                        PostCode = 1234,
                        TownName = "TestCity"
                    }};
            _postalCodesRepoMock.Setup(x => x.GetPostalCodes()).Returns(returnvalue);
            var _addressGenerator = new AddressGenerator(_postalCodesRepoMock.Object);
            var query = new PersonQuery() { Address = true };
            PersonHelper.Repository = _postalCodesRepoMock.Object;

            //Act
            var person = PersonHelper.GeneratePersons(query).First();

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(person.Address));
        }
        [Fact]
        public void GeneratePersons_HavePhoneNumberSetWhenQueryIsSet()
        {
            //Arrange
            var query = new PersonQuery() { PhoneNumber = true };

            //Act
            var person = PersonHelper.GeneratePersons(query).First();

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(person.PhoneNumber));
        }
        [Fact]
        public void GeneratePersons_HaveNoEntriesWhenAmountIsZero()
        {
            //Arrange
            var query = new PersonQuery();

            //Act
            var people = PersonHelper.GeneratePersons(query,0);

            //Assert
            Assert.True(people.Count == 0);
        }

        [Fact]
        public void GeneratePersons_InvalidStringForCPRFails()
        {
            //Assert
            Assert.Throws<Exception>(()=> PersonHelper.SetBirthdayFromCpr("Skibidi toilet"));
        }

        [Fact]
        public void GeneratePersons_HasIdWhenGotten()
        {
            //Arrange
            var query = new PersonQuery();

            //Act
            var person = PersonHelper.GeneratePersons(query, 1).First();

            //Assert
            Assert.True(person.Id > 0);
        }
    }
}
