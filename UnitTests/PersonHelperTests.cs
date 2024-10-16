using PersonalTestDataGeneratorBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class PersonHelperTests
    {
        [Fact]
        public void SetBirthdayFromCpr_ValidCpr_ShouldReturnCorrectDate()
        {
            // Arrange
            string validCpr = "01/01/2000-1234";
            DateOnly expectedDate = new DateOnly(2000, 1, 1);

            // Act
            DateOnly result = PersonHelper.SetBirthdayFromCpr(validCpr);

            // Assert
            Assert.Equal(expectedDate, result);
        }

        [Fact]
        public void SetBirthdayFromCpr_InvalidCpr_ShouldThrowException()
        {
            // Arrange
            string invalidCpr = "invalid-cpr";

            // Act & Assert
            Assert.Throws<Exception>(() => PersonHelper.SetBirthdayFromCpr(invalidCpr));
        }

        [Fact]
        public void GenerateCprWithGender_MaleGender_ShouldReturnValidCpr()
        {
            // Arrange
            string gender = "male";

            // Act
            string cpr = PersonHelper.GenerateCprWithGender(gender);

            // Assert
            Assert.NotNull(cpr);
            Assert.Contains("/", cpr);
            Assert.Contains("-", cpr);
            Assert.True(cpr.EndsWith("1") || cpr.EndsWith("3") || cpr.EndsWith("5") || cpr.EndsWith("7") || cpr.EndsWith("9"));
        }

        [Fact]
        public void GenerateCprWithGender_FemaleGender_ShouldReturnValidCpr()
        {
            // Arrange
            string gender = "female";

            // Act
            string cpr = PersonHelper.GenerateCprWithGender(gender);

            // Assert
            Assert.NotNull(cpr);
            Assert.Contains("/", cpr);
            Assert.Contains("-", cpr);
            Assert.True(cpr.EndsWith("0") || cpr.EndsWith("2") || cpr.EndsWith("4") || cpr.EndsWith("6") || cpr.EndsWith("8"));
        }


        [Fact]
        public void SetBirthdayFromCpr_EmptyCpr_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string emptyCpr = "";

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => PersonHelper.SetBirthdayFromCpr(emptyCpr));
        }

        [Fact]
        public void SetBirthdayFromCpr_ShortCpr_ShouldThrowException()
        {
            // Arrange
            string shortCpr = "01/01/20";

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => PersonHelper.SetBirthdayFromCpr(shortCpr));
        }

        [Fact]
        public void SetBirthdayFromCpr_InvalidDateCpr_ShouldThrowException()
        {
            // Arrange
            string invalidDateCpr = "32/13/2000-1234";

            // Act & Assert
            Assert.Throws<Exception>(() => PersonHelper.SetBirthdayFromCpr(invalidDateCpr));
        }

        [Fact]
        public void GenerateCprWithGender_InvalidGender_ShouldReturnNull()
        {
            // Arrange
            string gender = "unknown";

            // Act
            var result = PersonHelper.GenerateCprWithGender(gender);

            // Assert
            Assert.Null(result);
        }

        // POSITIVE: Valid format test
        [Fact]
        public void GeneratedCpr_ShouldHaveCorrectFormat()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("male");

            // Assert
            Assert.Matches(@"^\d{2}/\d{2}/\d{4}-\d{4}$", cpr);
        }

        // POSITIVE: Valid date test with explicit format
        [Fact]
        public void GeneratedCpr_ShouldContainValidDate()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("female");

            // Extract date part (dd/MM/yyyy)
            string datePart = cpr.Substring(0, 10);

            // Assert using TryParseExact
            Assert.True(
                DateOnly.TryParseExact(datePart, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
                $"CPR contains an invalid date. Generated: {cpr}"
            );
        }

        // POSITIVE: Gender-specific digit test
        [Fact]
        public void GeneratedCpr_ShouldEndWithGenderSpecificDigit()
        {
            // Act
            string maleCpr = PersonHelper.GenerateCprWithGender("male");
            string femaleCpr = PersonHelper.GenerateCprWithGender("female");

            // Extract the last digit of the CPR
            char maleGenderDigit = maleCpr[^1];
            char femaleGenderDigit = femaleCpr[^1];

            // Assert
            Assert.Contains(maleGenderDigit, new[] { '1', '3', '5', '7', '9' });
            Assert.Contains(femaleGenderDigit, new[] { '0', '2', '4', '6', '8' });
        }

        // POSITIVE: Year within valid range
        [Fact]
        public void GeneratedCpr_ShouldGenerateYearWithinValidRange()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("male");

            // Extract year part (yyyy)
            string yearPart = cpr.Substring(6, 4);

            // Assert
            int year = int.Parse(yearPart);
            Assert.InRange(year, 1900, DateTime.Now.Year);
        }

        // POSITIVE: Valid day and month
        [Fact]
        public void GeneratedCpr_ShouldHaveValidDayAndMonth()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("female");

            // Extract day and month parts
            string dayPart = cpr.Substring(0, 2);
            string monthPart = cpr.Substring(3, 2);

            // Convert to integers
            int day = int.Parse(dayPart);
            int month = int.Parse(monthPart);

            // Assert valid day and month
            Assert.InRange(day, 1, 31);
            Assert.InRange(month, 1, 12);
        }

        // NEGATIVE: Invalid gender input
        [Fact]
        public void GeneratedCpr_WithInvalidGender_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => PersonHelper.GenerateCprWithGender("unknown"));
        }

        // POSITIVE: Multiple CPR generations produce unique numbers
        [Fact]
        public void GeneratedCpr_MultipleCalls_ShouldProduceUniqueNumbers()
        {
            // Act
            var cprSet = new HashSet<string>();
            for (int i = 0; i < 100; i++)
            {
                string cpr = PersonHelper.GenerateCprWithGender("male");
                cprSet.Add(cpr);
            }

            // Assert
            Assert.Equal(100, cprSet.Count);
        }
    }
}

