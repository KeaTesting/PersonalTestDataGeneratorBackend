using System;
using Xunit;
using PersonalTestDataGeneratorBackend;
namespace UnitTests
{
    public class PhoneNumberGeneratorTests
    {

        //For the tests here, Black box test design technique has been used.
        //First up there are tests that make sure the general structure should work.

        //Then comes the Boundary Value Analysis tests.
        //There has been used 3 boundary values test cases for the single prefix entries, such as "441".  These have been ordered by Equivalence Partitioning.
        //There has been used 2 boundary values test cases for the sequence/intervals of prefixies, such as "344-349". These have been ordered by Equivalence Partitioning.

        string[] validPrefixes =
        {
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "40", "41", "42", "50", "51", "52", "53", "60", "61", "71", "81", "91", "92", "93",
        "342", "344", "345", "346", "347", "348", "349", "356", "357", "359", "362", "365", "366", "389", "398",
        "431", "441", "462", "466", "468", "472", "474", "476", "478", "485", "486", "488", "489", "493", "494",
        "495", "496", "498", "499", "542", "543", "545", "551", "552", "556", "571", "572", "573", "574", "577",
        "579", "584", "586", "587", "589", "597", "598", "627", "629", "641", "649", "658", "662", "663", "664",
        "665", "667", "692", "693", "694", "697", "771", "772", "782", "783", "785", "786", "788", "789", "826",
        "827", "829"
    };


        //*****************************************************
        //General structure test with Equivalence Partitioning.
        //*****************************************************

        //POSITIVE General structure test
        [Fact]
        public void GeneratedPhoneNumber_ShouldReturn8Digits()
        {
            // Act
            string PhoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            Assert.Equal(8, PhoneNumber.Length);
        }

        //POSITIVE General structure test
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

        //POSITIVE General structure test
        [Fact]
        public void GeneratedPhoneNumber_ShouldContainOnlyDigits()
        {
            // Act
            string PhoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();

            // Assert
            Assert.Matches(@"^\d+$", PhoneNumber);
        }


        //*********************************************************
        //3-Boundary tests på individuelle entries i valid prefixes
        //*********************************************************

        //POSITIVE 3-Boundary inline test for valid prefixes
        [Theory]
        [InlineData("71")]
        [InlineData("462")]
        [InlineData("466")]
        [InlineData("468")]
        [InlineData("577")]
        [InlineData("579")]
        public void ValidPhoneNumber_ShouldStartWithValidPrefix(string validPrefix)
        {
            // Act & Assert
            Assert.Contains(validPrefix, validPrefixes);
        }

        //NEGATIVE 3-Boundary inline test for invalid prefixes below valid range
        [Theory]
        [InlineData("70")]  // Below "71"
        [InlineData("461")]  // Below "462"
        [InlineData("465")]  // Below "466"
        [InlineData("467")]  // Below "468"
        [InlineData("576")]  // Below "577"
        [InlineData("578")]  // Below "579"
        public void InvalidPhoneNumber_WithPrefixBelowValidRange_ShouldFail(string invalidPrefix)
        {
            // Act & Assert
            Assert.DoesNotContain(invalidPrefix, validPrefixes);
        }

        //NEGATIVE 3-Boundary inline test for invalid prefixes above valid range
        [Theory]
        [InlineData("72")]  // Above "71"
        [InlineData("463")]  // Above "462"
        [InlineData("467")]  // Above "466"
        [InlineData("469")]  // Above "468"
        [InlineData("578")]  // Above "577"
        [InlineData("580")]  // Above "579"
        public void InvalidPhoneNumber_WithPrefixAboveValidRange_ShouldFail(string invalidPrefix)
        {
            // Act & Assert
            Assert.DoesNotContain(invalidPrefix, validPrefixes);
        }



        //***********************************************
        // 2-Boundary tests for intervals in valid prefixes
        //***********************************************

        //POSITIVE 2-Boundary inline test for valid prefixes (lower, upper, and middle values)
        [Theory]
        [InlineData("20")]  // Lower boundary for interval 20-31
        [InlineData("31")]  // Upper boundary for interval 20-31
        [InlineData("26")]  // Middle value for interval 20-31
        [InlineData("344")] // Lower boundary for interval 344-349
        [InlineData("349")] // Upper boundary for interval 344-349
        [InlineData("346")] // Middle value for interval 344-349
        [InlineData("485")] // Lower boundary for interval 485-486
        [InlineData("486")] // Upper boundary for interval 485-486
        [InlineData("488")] // Lower boundary for interval 488-489
        [InlineData("489")] // Upper boundary for interval 488-489
        [InlineData("826")] // Lower boundary for interval 826-827
        [InlineData("827")] // Upper boundary for interval 826-827
        public void ValidPhoneNumber_WithValidBoundary_ShouldStartWithValidPrefix(string validPrefix)
        {
            // Act & Assert
            Assert.Contains(validPrefix, validPrefixes);
        }

        //NEGATIVE 2-Boundary inline test for invalid prefixes (below and above valid range)
        [Theory]
        [InlineData("19")]  // Below 20-31
        [InlineData("32")]  // Above 20-31
        [InlineData("343")] // Below 344-349
        [InlineData("350")] // Above 344-349
        [InlineData("484")] // Below 485-486
        [InlineData("487")] // Above 485-486
        [InlineData("490")] // Above 488-489
        [InlineData("825")] // Below 826-827
        [InlineData("828")] // Above 826-827
        public void InvalidPhoneNumber_WithInvalidBoundary_ShouldFail(string invalidPrefix)
        {
            // Act & Assert
            Assert.DoesNotContain(invalidPrefix, validPrefixes);
        }



    }
}












