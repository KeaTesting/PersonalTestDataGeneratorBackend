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


       

       

    }
}
