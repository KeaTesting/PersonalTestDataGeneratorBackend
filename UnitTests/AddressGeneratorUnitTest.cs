
using PersonalTestDataGeneratorBackend.PersonalTestDataGeneratorBackend;
using System.Text.RegularExpressions;

namespace UnitTests
{
    public class AddressGeneratorUnitTest
    {
        private readonly AddressGenerator _addressGenerator = new AddressGenerator();

        #region Positve Tests

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

        #endregion

    }
}
