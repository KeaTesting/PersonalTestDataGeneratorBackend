using System;
using System.Globalization;
using Xunit;
using PersonalTestDataGeneratorBackend;

namespace UnitTests
{
    public class CprGeneratorTests
    {
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
    public static class PersonHelper
    {
        private static Random _random = new Random();

        public static string GenerateCprWithGender(string gender)
        {
            // Validate gender input
            if (gender != "male" && gender != "female")
                throw new ArgumentException("Invalid gender specified. Allowed values are 'male' or 'female'.");

            // Generate random date between 1900 and today
            DateTime startDate = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            DateTime randomDate = startDate.AddDays(_random.Next(range));

            string datePart = randomDate.ToString("dd/MM/yyyy");

            // Generate random serial number
            int serialNumber = _random.Next(0, 9999);

            // Adjust last digit based on gender
            int lastDigit = serialNumber % 10;
            if (gender == "male" && lastDigit % 2 == 0)
                serialNumber += 1; // Ensure last digit is odd
            else if (gender == "female" && lastDigit % 2 != 0)
                serialNumber += 1; // Ensure last digit is even

            string serialPart = serialNumber.ToString("D4");

            return $"{datePart}-{serialPart}";
        }
    }

}
