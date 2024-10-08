using System;
using Xunit;
using PersonalTestDataGeneratorBackend.Generators;
namespace UnitTests
{
    public class PhoneNumberGeneratorTests
    {
        private readonly PhoneNumberGenerator _generator = new PhoneNumberGenerator();

        [Fact]
        public void GeneratedPhoneNumber_ShouldReturn8Digits()
        {
            // Act
            string phoneNumber = _generator.GeneratePhoneNumber();

            // Assert
            Assert.Equal(8, phoneNumber.Length);
        }

        [Fact]
        public void GeneratedPhoneNumber_ShouldStartWithValidPrefix()
        {
            string[] validPrefixes = PhoneNumberGenerator.validPrefixes;

            // Act
            string phoneNumber = _generator.GeneratePhoneNumber();

            // Assert
            bool startsWithValidPrefix = false;
            foreach (var prefix in validPrefixes)
            {
                if (phoneNumber.StartsWith(prefix))
                {
                    startsWithValidPrefix = true;
                    break;
                }
            }

            Assert.True(startsWithValidPrefix, $"Phone number does not start with a valid prefix. Generated: {phoneNumber}");
        }

        [Fact]
        public void GeneratedPhoneNumber_ShouldContainOnlyDigits()
        {
            // Act
            string phoneNumber = _generator.GeneratePhoneNumber();

            // Assert
            Assert.Matches(@"^\d+$", phoneNumber);
        }
    }
}