using System;
using Xunit;
using PersonalTestDataGeneratorBackend;
namespace UnitTests
{
    public class PhoneNumberGeneratorTests
    {

        //For the tests here, Black box test design technique has been used.
        //First up there are tests that make sure the general structure should work. These has been ordered by Equivalence Partitioning.

        //Then lastly the Boundary Value Analysis tests.
        //There has been used 3 boundary values test cases for the single prefix entries, such as "441".
        //We have run 3 boundary tests on 6 entries in the list and have a final of 18 tests.
        //There has been used 2 boundary values test cases for the sequence of prexies, such as "344-349".
        //We have run 2 boundary tests on 5 entries in the list and have a final of 20 tests.

        string[] validPrefixes = PhoneNumberGenerator.validPrefixes;

        //General structure test with Equivalence Partitioning.

        //POSITIVE General structure test 1/3
        [Fact]
        public void GeneratedPhoneNumber_ShouldReturn8Digits()
        {
            // Act
            string PhoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            Assert.Equal(8, PhoneNumber.Length);
        }

        //POSITIVE General structure test 2/3
        [Fact]
        public void GeneratedPhoneNumber_ShouldStartWithValidPrefix()
        {

            // Act
            string PhoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            bool startsWithValidPrefix = false;
            foreach (var prefix in validPrefixes)
            {
                if (PhoneNumber.StartsWith(prefix))
                {
                    startsWithValidPrefix = true;
                    break;
                }
            }

            Assert.True(startsWithValidPrefix, $"Phone number does not start with a valid prefix. Generated: {PhoneNumber}");
        }

        //POSITIVE General structure test 3/3
        [Fact]
        public void GeneratedPhoneNumber_ShouldContainOnlyDigits()
        {
            // Act
            string PhoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            Assert.Matches(@"^\d+$", PhoneNumber);
        }

        //NEGATIVE General structure test 1/10
        [Fact]
        public void InvalidPhoneNumber_WithAlphabeticalCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30AB5678";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 2/10
        [Fact]
        public void InvalidPhoneNumber_WithFloat_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30.45678";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 3/10
        [Fact]
        public void InvalidPhoneNumber_WithSpecialCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30@#5678";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 4/10
        [Fact]
        public void InvalidPhoneNumber_WithKoreanCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30안녕하세요56";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 5/10
        [Fact]
        public void InvalidPhoneNumber_WithJapaneseCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30カタカナ56";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 6/10
        [Fact]
        public void InvalidPhoneNumber_WithChineseCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30你好567";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 7/10
        [Fact]
        public void InvalidPhoneNumber_WithArabicCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30سلام567";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 8/10
        [Fact]
        public void InvalidPhoneNumber_WithHindiCharacters_ShouldFailDigitCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30नमस्ते56";

            // Act & Assert
            Assert.DoesNotMatch(@"^\d+$", invalidPhoneNumber);
        }

        //NEGATIVE General structure test 9/10
        [Fact]
        public void InvalidPhoneNumber_WithTooManyDigits_ShouldFailLengthCheck()
        {
            // Arrange
            string invalidPhoneNumber = "305678901";

            // Act & Assert
            Assert.NotEqual(8, invalidPhoneNumber.Length);
        }

        //NEGATIVE General structure test 10/10
        [Fact]
        public void InvalidPhoneNumber_WithTooFewDigits_ShouldFailLengthCheck()
        {
            // Arrange
            string invalidPhoneNumber = "30567";

            // Act & Assert
            Assert.NotEqual(8, invalidPhoneNumber.Length);
        }



        //3-Boundary tests


        //POSITIVE 3-Boundary test 1/18 - Valid boundary for prefix '71'
        [Fact]
        public void ValidPhoneNumber_WithPrefix71_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("71", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 2/18 - Invalid boundary below '71'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow71_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("70", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 3/18 - Invalid boundary above '71'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove71_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("72", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 3-Boundary test 4/18 - Valid boundary for prefix '462'
        [Fact]
        public void ValidPhoneNumber_WithPrefix462_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("462", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 5/18 - Invalid boundary below '462'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow462_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("461", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 6/18 - Invalid boundary above '462'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove462_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("463", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 3-Boundary test 7/18 - Valid boundary for prefix '466'
        [Fact]
        public void ValidPhoneNumber_WithPrefix466_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("466", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 8/18 - Invalid boundary below '466'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow466_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("465", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 9/18 - Invalid boundary above '466'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove466_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("467", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 3-Boundary test 10/18 - Valid boundary for prefix '468'
        [Fact]
        public void ValidPhoneNumber_WithPrefix468_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("468", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 11/18 - Invalid boundary below '468'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow468_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("467", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 12/18 - Invalid boundary above '468'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove468_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("469", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 3-Boundary test 13/18 - Valid boundary for prefix '577'
        [Fact]
        public void ValidPhoneNumber_WithPrefix577_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("577", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 14/18 - Invalid boundary below '577'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow577_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("576", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 15/18 - Invalid boundary above '577'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove577_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("578", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 3-Boundary test 16/18 - Valid boundary for prefix '579'
        [Fact]
        public void ValidPhoneNumber_WithPrefix579_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("579", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 17/18 - Invalid boundary below '579'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow579_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("578", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 3-Boundary test 18/18 - Invalid boundary above '579'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove579_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("580", PhoneNumberGenerator.validPrefixes);
        }




        //2-Boundary test

        // Interval: 20-31
        //POSITIVE 2-Boundary test 1/20 - Valid lower boundary for interval '20-31'
        [Fact]
        public void ValidPhoneNumber_WithPrefix20_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("20", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 2-Boundary test 2/20 - Valid upper boundary for interval '20-31'
        [Fact]
        public void ValidPhoneNumber_WithPrefix31_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("31", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 3/20 - Invalid boundary below '20'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow20_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("19", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 4/20 - Invalid boundary above '31'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove31_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("32", PhoneNumberGenerator.validPrefixes);
        }

        // Interval: 344-349
        //POSITIVE 2-Boundary test 5/20 - Valid lower boundary for interval '344-349'
        [Fact]
        public void ValidPhoneNumber_WithPrefix344_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("344", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 2-Boundary test 6/20 - Valid upper boundary for interval '344-349'
        [Fact]
        public void ValidPhoneNumber_WithPrefix349_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("349", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 7/20 - Invalid boundary below '344'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow344_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("343", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 8/20 - Invalid boundary above '349'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove349_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("350", PhoneNumberGenerator.validPrefixes);
        }

        // Interval: 485-486
        //POSITIVE 2-Boundary test 9/20 - Valid lower boundary for interval '485-486'
        [Fact]
        public void ValidPhoneNumber_WithPrefix485_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("485", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 2-Boundary test 10/20 - Valid upper boundary for interval '485-486'
        [Fact]
        public void ValidPhoneNumber_WithPrefix486_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("486", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 11/20 - Invalid boundary below '485'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow485_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("484", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 12/20 - Invalid boundary above '486'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove486_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("487", PhoneNumberGenerator.validPrefixes);
        }

        // Interval: 488-489
        //POSITIVE 2-Boundary test 13/20 - Valid lower boundary for interval '488-489'
        [Fact]
        public void ValidPhoneNumber_WithPrefix488_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("488", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 2-Boundary test 14/20 - Valid upper boundary for interval '488-489'
        [Fact]
        public void ValidPhoneNumber_WithPrefix489_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("489", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 15/20 - Invalid boundary below '488'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow488_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("487", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 16/20 - Invalid boundary above '489'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove489_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("490", PhoneNumberGenerator.validPrefixes);
        }

        // Interval: 826-827
        //POSITIVE 2-Boundary test 17/20 - Valid lower boundary for interval '826-827'
        [Fact]
        public void ValidPhoneNumber_WithPrefix826_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("826", PhoneNumberGenerator.validPrefixes);
        }

        //POSITIVE 2-Boundary test 18/20 - Valid upper boundary for interval '826-827'
        [Fact]
        public void ValidPhoneNumber_WithPrefix827_ShouldStartWithValidPrefix()
        {
            // Act & Assert
            Assert.Contains("827", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 19/20 - Invalid boundary below '826'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixBelow826_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("825", PhoneNumberGenerator.validPrefixes);
        }

        //NEGATIVE 2-Boundary test 20/20 - Invalid boundary above '827'
        [Fact]
        public void InvalidPhoneNumber_WithPrefixAbove827_ShouldFail()
        {
            // Act & Assert
            Assert.DoesNotContain("828", PhoneNumberGenerator.validPrefixes);
        }


    }
}












