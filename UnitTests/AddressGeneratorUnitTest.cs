using Moq;
using PersonalTestDataGeneratorBackend.Generators;
using PersonalTestDataGeneratorBackend.Models;
using PersonalTestDataGeneratorBackend.Repositories;
using System.Text.RegularExpressions;

namespace UnitTests
{
    public class AddressGeneratorUnitTest
    {
        private readonly AddressGenerator _addressGenerator;
        private readonly Mock<PostalCodesRepository> _postalCodesRepoMock;
        private readonly Random _random;

        public AddressGeneratorUnitTest()
        {
            _postalCodesRepoMock = new Mock<PostalCodesRepository>();
            var returnvalue = new List<PostalCode>() {
                    new PostalCode
                    {
                        PostCode = 1234,
                        TownName = "TestCity"
                    }};
            _postalCodesRepoMock.Setup(x => x.GetPostalCodes()).Returns(returnvalue);
            _addressGenerator = new AddressGenerator(_postalCodesRepoMock.Object);
            _random = new Random();
        }

        [Fact]
        public void GenerateStreet_ShouldReturnValidStreetNameWithinLength()
        {
            //Act
            string street = _addressGenerator.GenerateStreet();

            //Assert
            Assert.InRange(street.Length, 5, 15);
        }


        [Fact]
        public void GenerateFloor_ShouldHaveStIfFloorIsZero()
        {
            // Act
            var result = _addressGenerator.GenerateFloor(true);

            // Assert
            Assert.Equal(result, "st");
        }

        [Fact]
        public void GenerateFloor_ShouldHaveNumberIfFloorIsNotZero()
        {
            // Act
            var result = _addressGenerator.GenerateFloor(false);

            // Assert
            Assert.True(int.TryParse(result, out _));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GenerateDoor_ShouldReturnValidDoorValuesSides(int val)
        {
            // Arrange 
            var validResults = new List<string>() { "mf", "th", "tv" };

            // Act
            string result = _addressGenerator.GenerateDoor(val);

            // Assert

            Assert.Contains(result, validResults);
        }
        [Fact]
        public void GenerateDoor_ShouldReturnValidDoorValuesNumbers()
        {
            // Arrange 
            var validResults = new List<string>();
            for (int i = 0; i <= 50; i++)
            {
                validResults.Add(i.ToString());
            }

            // Act
            string result = _addressGenerator.GenerateDoor(3);

            // Assert

            Assert.Contains(result, validResults);
        }

        [Fact]
        public void GenerateDoor_ShouldReturnValidDoorValues_WhenChoiceIs4()
        {
            // Act
            string result = _addressGenerator.GenerateDoor(4);

            // Assert
            Assert.True(Regex.IsMatch(result, @"^[a-z]-\d{1,999}$"));
        }

        [Fact]
        public void GeneratePostal_ShouldReturnValidPostalCodeLength()
        {
            // Act
            var postal = _addressGenerator.GeneratePostalCode();

            // Assert 
            Assert.True(postal.PostCode.ToString().Length == 4);
        }

        [Fact]
        public void GeneratePostal_ShouldReturnValidTownName()
        {
            // Act
            var postal = _addressGenerator.GeneratePostalCode();

            // Assert 
            Assert.Matches(@"^[a-zA-ZæøåÆØÅ\s]*$", postal.TownName);
        }


    }
}
