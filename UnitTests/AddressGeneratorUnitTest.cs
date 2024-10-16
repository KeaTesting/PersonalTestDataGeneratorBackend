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
        private readonly Mock<PostalCodesRepo> _postalCodesRepoMock;
        private readonly Random _random;

        public AddressGeneratorUnitTest()
        {
            _postalCodesRepoMock = new Mock<PostalCodesRepo>();
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
        public void GenerateFloor_ShouldReturnStOrNumber()
        {
            // Act
            string result = _addressGenerator.GenerateFloor();

            // Assert
            bool isValid = result == "st" || int.TryParse(result, out int number) && number >= 1 && number <= 29;
            Assert.True(isValid);
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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GenerateDoor_ShouldReturnValidDoorValues(int val)
        {
            // Arrange 
            var validResults = new List<string>() { "mf","th","tv"};
            for (int i = 0; i <= 50; i++)
            {
                validResults.Add(i.ToString());
            }
            // Act
            string result = _addressGenerator.GenerateDoor(val);

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
