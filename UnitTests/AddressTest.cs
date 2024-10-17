using Moq;
using PersonalTestDataGeneratorBackend.Generators;
using PersonalTestDataGeneratorBackend.Models;
using PersonalTestDataGeneratorBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class AddressTest
    {

        [Fact]
        public void ToStringMethod_ShouldReturnSpecificFormat()
        {
            // Arrange
            var postalCode = new PostalCode
            {
                PostCode = 2200,
                TownName = "København N"

            };

            var address = new Address
            {
                Street = "Guldbergsgade",
                Number = "29N",
                Floor = "2",
                Door = "E212",
                PostalCode = postalCode 
            };

            var expected = "Guldbergsgade 29N, 2. E212, 2200 København N";

            // Act
            var result = address.ToString();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
