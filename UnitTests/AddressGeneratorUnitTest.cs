using Moq;
using PersonalTestDataGeneratorBackend.Generators;
using PersonalTestDataGeneratorBackend.Models;
using PersonalTestDataGeneratorBackend.Repositories;
using System.Text.RegularExpressions;

namespace UnitTests
{
    public class AddressGeneratorUnitTest
    {
        private AddressGenerator _addressGenerator;
        private Mock<PostalCodesRepository> _postalCodesRepoMock;
        private Random _random;

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
        public void GenerateAddress_ShouldHaveStreet()
        {
            //Act
            var address = _addressGenerator.GenerateAdress();

            //Assert
            Assert.False(string.IsNullOrEmpty(address.Street));
        }
        [Fact]
        public void GenerateAddress_ShouldHaveNumber()
        {
            //Act
            var address = _addressGenerator.GenerateAdress();

            //Assert
            Assert.False(string.IsNullOrEmpty(address.Number));
        }
        [Fact]
        public void GenerateAddress_ShouldHaveFloor()
        {
            //Act
            var address = _addressGenerator.GenerateAdress();

            //Assert
            Assert.False(string.IsNullOrEmpty(address.Floor));
        }
        [Fact]
        public void GenerateAddress_ShouldHaveDoor()
        {
            //Act
            var address = _addressGenerator.GenerateAdress();

            //Assert
            Assert.False(string.IsNullOrEmpty(address.Door));
        }
        [Fact]
        public void GenerateAddress_ShouldHavePostalCode()
        {
            //Act
            var address = _addressGenerator.GenerateAdress();

            //Assert
            Assert.False(string.IsNullOrEmpty(address.PostalCode.FullName));
        }

        [Fact]
        public void GenerateFloor_ShouldHaveStIfFloorIsZero()
        {
            // Act
            var result = _addressGenerator.GenerateFloor(isGroundFloor :true);

            // Assert
            Assert.Equal(result, "st");
        }

        [Fact]
        public void GenerateFloor_ShouldHaveNumberIfFloorIsNotZero()
        {
            // Act
            var result = _addressGenerator.GenerateFloor(isGroundFloor :false); 

            // Assert
            Assert.True(int.TryParse(result, out _));
        }

        [Fact]
        public void GenerateFloor_GenerateSecificNumberIfParsed()
        {
            // Arrange
            var setNumber = 4;

            // Act
            var result = _addressGenerator.GenerateFloor(isGroundFloor: false, specificFloor: setNumber);

            // Assert
            Assert.True(int.TryParse(result, out int number) && number == setNumber);
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
        public void GenerateDoor_ShouldFailWithInvalidInput()
        {
            // Assert
            Assert.Throws<Exception>(() => _addressGenerator.GenerateDoor(40));
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

        [Fact]
        public void GeneratPostCodes_ShouldReturnErrorIfCountIsNull()
        {
            //Arrange
            _postalCodesRepoMock = new Mock<PostalCodesRepository>();
            var returnvalue = new List<PostalCode>();
            _postalCodesRepoMock.Setup(x => x.GetPostalCodes()).Returns(returnvalue);
            _addressGenerator = new AddressGenerator(_postalCodesRepoMock.Object);

            //Assert
            Assert.Throws<Exception>(() => _addressGenerator.GeneratePostalCode());
        }
    }
}
