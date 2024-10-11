using System;
using Xunit;
using PersonalTestDataGeneratorBackend.Generators;
namespace UnitTests
{
    public class PhoneNumberGeneratorTests
    {
        
        [Fact]
        public void GeneratedPhoneNumber_ShouldReturn8Digits()
        {
            // Act
            string phoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            Assert.Equal(8, phoneNumber.Length);
        }

        [Fact]
        public void GeneratedPhoneNumber_ShouldStartWithValidPrefix()
        {
            string[] validPrefixes = PhoneNumberGenerator.validPrefixes;

            // Act
            string phoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

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
            string phoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            Assert.Matches(@"^\d+$", phoneNumber);
        }
    }
}