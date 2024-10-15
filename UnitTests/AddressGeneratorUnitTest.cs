
using Moq;
using PersonalTestDataGeneratorBackend;
using PersonalTestDataGeneratorBackend.Generators;
using PersonalTestDataGeneratorBackend.Repositories;
using System.Text.RegularExpressions;

namespace UnitTests
{
    public class AddressGeneratorUnitTest
    {
        private readonly AddressGenerator _addressGenerator;

        #region Positve Tests

        public AddressGeneratorUnitTest()
        {
            var moq = new Mock<PostalCodesRepo>();
            var returnvalue = new List<PostalCode>() {
                new PostalCode
                {
                    PostCode = 1234,
                    TownName = "TestCity"
                }};
            moq.Setup(x => x.GetPostalCodes()).Returns(returnvalue);
            _addressGenerator = new AddressGenerator(moq.Object);
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
        public void GenerateNumber_ShouldReturnNumberAndOrLetter()
        {
            // Act
            string number = _addressGenerator.GenerateNumber();

            // Assert
            Assert.Matches(@"^\d{1,1000}[A-Z]?$", number);
        }

        [Fact]
        public void GenerateFloor_ShouldReturnStOrNumber()
        {
            // Act
            string result = _addressGenerator.GenerateFloor();

            // Assert
            bool isValid = result == "st" || int.TryParse(result, out int number) && number >= 1 && number <= 29;
            Assert.True(isValid);
        }

        [Fact]
        public void GenerateFloor_ShouldReturnStSometimes()
        {
            // Act
            var results = Enumerable.Range(0, 10).Select(_ => _addressGenerator.GenerateFloor()).ToList();

            // Assert 
            Assert.Contains("st", results);
        }

        [Fact]
        public void GenerateFloor_ShouldReturnNumberBetween1And29()
        {
            // Act
            var results = Enumerable.Range(0, 100).Select(_ => _addressGenerator.GenerateFloor()).Where(floor => floor != "st").ToList();

            // Assert
            foreach (var result in results)
            {
                int number;
                Assert.True(int.TryParse(result, out number));
                Assert.InRange(number, 1, 29);
            }
        }

        [Fact]
        public void GenerateDoor_ShouldReturnValidDoorOption()
        {
            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.True(
                result == "th" ||
                result == "mf" ||
                result == "tv" ||
                (int.TryParse(result, out int number) && number >= 1 && number <= 50) ||
                Regex.IsMatch(result, @"^[a-z]-\d{1,999}$")
            );
        }

        [Fact]
        public void GeneratePostal_ShouldReturnValidPostalCode()
        {
            // Act
            var postal = _addressGenerator.GeneratePostalCode();
            var splitPostal = postal.Split(" ", 2);
            var postalCode = splitPostal[0];

            // Assert 
            Assert.True(int.TryParse(postalCode, out _));
        }

        [Fact]
        public void GeneratePostal_ShouldReturnValidTownName()
        {
            // Act
            var postal = _addressGenerator.GeneratePostalCode();
            var splitPostal = postal.Split(" ", 2);
            var townName = splitPostal[1];

            // Assert 
            Assert.Matches(@"^[a-zA-ZæøåÆØÅ\s]*$", townName);
        }

        #endregion

    }
}
