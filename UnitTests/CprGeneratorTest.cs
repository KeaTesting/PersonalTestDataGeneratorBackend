using System;
using Xunit;
using PersonalTestDataGeneratorBackend;

namespace UnitTests
{
    public class CprGeneratorTests
    {
        // General structure tests with Equivalence Partitioning.

        // POSITIVE General structure test 1/3
        [Fact]
        public void GeneratedCpr_ShouldHaveCorrectFormat()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("male");

            // Assert
            Assert.Matches(@"^\d{2}/\d{2}/\d{4}-\d{4}$", cpr);
        }

        // POSITIVE General structure test 2/3
        [Fact]
        public void GeneratedCpr_ShouldContainValidDate()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("female");

            // Extract date part (dd/MM/yyyy)
            string datePart = cpr.Substring(0, 10);

            // Assert
            Assert.True(DateOnly.TryParse(datePart, out _), $"CPR contains an invalid date. Generated: {cpr}");
        }

        // POSITIVE General structure test 3/3
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
            Assert.Contains(femaleGenderDigit, new[] { '2', '4', '6', '8' });
        }

        // Boundary value tests for CPR years and running numbers.

        // POSITIVE Boundary test 1/4 - Valid boundary for year 1900
        [Fact]
        public void GeneratedCpr_ShouldHandleLowerBoundaryYear_1900()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("male");

            // Extract year part (yyyy)
            string yearPart = cpr.Substring(6, 4);

            // Assert
            int year = int.Parse(yearPart);
            Assert.InRange(year, 1900, 2023);
        }

        // POSITIVE Boundary test 2/4 - Valid boundary for year 2023
        [Fact]
        public void GeneratedCpr_ShouldHandleUpperBoundaryYear_2023()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("female");

            // Extract year part (yyyy)
            string yearPart = cpr.Substring(6, 4);

            // Assert
            int year = int.Parse(yearPart);
            Assert.InRange(year, 1900, 2023);
        }

        // NEGATIVE Boundary test 3/4 - Invalid boundary below year 1900
        [Fact]
        public void GeneratedCpr_ShouldNotGenerateYearBelow1900()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("male");

            // Extract year part (yyyy)
            string yearPart = cpr.Substring(6, 4);

            // Assert
            int year = int.Parse(yearPart);
            Assert.True(year >= 1900, $"Generated year {year} is below the valid range.");
        }

        // NEGATIVE Boundary test 4/4 - Invalid boundary above year 2023
        [Fact]
        public void GeneratedCpr_ShouldNotGenerateYearAbove2023()
        {
            // Act
            string cpr = PersonHelper.GenerateCprWithGender("female");

            // Extract year part (yyyy)
            string yearPart = cpr.Substring(6, 4);

            // Assert
            int year = int.Parse(yearPart);
            Assert.True(year <= 2023, $"Generated year {year} is above the valid range.");
        }
    }
}
