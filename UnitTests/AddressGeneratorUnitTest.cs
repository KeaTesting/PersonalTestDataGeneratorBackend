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
        private readonly Mock<IRandomGenerator> _randomGeneratorMock;

        #region Positve Tests

        public AddressGeneratorUnitTest()
        {
            _postalCodesRepoMock = new Mock<PostalCodesRepo>();
            _randomGeneratorMock = new Mock<IRandomGenerator>();
            var returnvalue = new List<PostalCode>() {
                    new PostalCode
                    {
                        PostCode = 1234,
                        TownName = "TestCity"
                    }};
            _postalCodesRepoMock.Setup(x => x.GetPostalCodes()).Returns(returnvalue);
            _addressGenerator = new AddressGenerator(_postalCodesRepoMock.Object, _randomGeneratorMock.Object);
        }

        [Theory]
        [InlineData(5)] // Lower limit
        [InlineData(10)] // middle value
        [InlineData(15)] // Upper limit
        public void GenerateStreet_ShouldReturnValidStreetNameWithinLength(int length)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5, 15)).Returns(length);

            //Act
            string street = _addressGenerator.GenerateStreet();

            //Assert
            Assert.InRange(street.Length, 5, 15);
        }

        [Theory]
        [InlineData(1)]     // Lower limit
        [InlineData(500)]   // Middle value
        [InlineData(999)]   // Upper limit
        public void GenerateNumber_ShouldReturnValidNumberWithoutLetter(int number)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(1, 1000)).Returns(number);

            _randomGeneratorMock.Setup(r => r.Next(0, 2)).Returns(0);

            // Act
            string result = _addressGenerator.GenerateNumber();

            // Assert
            Assert.Equal($"{number}", result);
        }

        [Theory]
        [InlineData(1, "A")]     // Lower limit with a letter
        [InlineData(999, "Z")]   // Upper limit with a letter
        public void GenerateNumber_ShouldReturnValidNumberWithLetter(int number, string optionalLetter)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(1, 1000)).Returns(number);

            _randomGeneratorMock.Setup(r => r.Next(0, 2)).Returns(1);
            _randomGeneratorMock.Setup(r => r.Next('A', 'Z' + 1)).Returns(optionalLetter[0]);

            // Act
            string result = _addressGenerator.GenerateNumber();

            // Assert
            Assert.Equal($"{number}{optionalLetter}", result);
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
        public void GenerateDoor_ShouldReturnTh_WhenChoiceIs0()
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5)).Returns(0);

            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.Equal("th", result);
        }

        [Fact]
        public void GenerateDoor_ShouldReturnMf_WhenChoiceIs1()
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5)).Returns(1);

            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.Equal("mf", result);
        }

        [Fact]
        public void GenerateDoor_ShouldReturnTv_WhenChoiceIs2()
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5)).Returns(2);

            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.Equal("tv", result);
        }

        [Theory]
        [InlineData(1)] // Lower limit
        [InlineData(25)] // Middle value
        [InlineData(50)] // Upper limit
        public void GenerateDoor_ShouldReturnNumberBetween1And50_WhenChoiceIs3(int number)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5)).Returns(3); // Returns 3 for the first call to Next(5).

            _randomGeneratorMock.Setup(r => r.Next(1, 51)).Returns(number); // Returns 24 for the call to Next(1, 51).

            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.Equal(number.ToString(), result);
        }

        [Theory]
        [InlineData(1)] // Lower limit
        [InlineData(999)] // Upper limit
        public void GenerateDoor_ShouldReturnLetterWithDashAndValidNumber_WhenChoiceIs4(int number)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5)).Returns(4);
            _randomGeneratorMock.Setup(r => r.Next('a', 'z' + 1)).Returns('c');
            _randomGeneratorMock.Setup(r => r.Next(1, 1000)).Returns(number);

            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.Equal($"c-{number}", result);
        }

        [Theory]
        [InlineData('a')] // Lower limit
        [InlineData('z')] // Upper limit
        public void GenerateDoor_ShouldReturnValidLetterWithDashAndNumber_WhenChoiceIs4(char letter)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5)).Returns(4);
            _randomGeneratorMock.Setup(r => r.Next('a', 'z' + 1)).Returns(letter);
            _randomGeneratorMock.Setup(r => r.Next(1, 1000)).Returns(666);

            // Act
            string result = _addressGenerator.GenerateDoor();

            // Assert
            Assert.Equal($"{letter}-666", result);
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
        #region Negative Tests

        [Theory]
        [InlineData(4)] // Lower limit
        [InlineData(0)] // Zero value
        [InlineData(16)] // Upper limit
        public void GenerateStreet_ShouldReturnINValidStreetNameWithinLength(int length)
        {
            // Arrange
            _randomGeneratorMock.Setup(r => r.Next(5, 15)).Returns(length);

            //Act
            string street = _addressGenerator.GenerateStreet();

            //Assert
            Assert.NotInRange(street.Length, 5, 15);
        }

        #endregion

    }
}
