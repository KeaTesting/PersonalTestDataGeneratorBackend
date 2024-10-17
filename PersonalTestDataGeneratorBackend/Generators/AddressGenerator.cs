using Microsoft.EntityFrameworkCore;
using PersonalTestDataGeneratorBackend.Models;
using PersonalTestDataGeneratorBackend.Repositories;

namespace PersonalTestDataGeneratorBackend.Generators
{
    public class AddressGenerator
    {
        private readonly PostalCodesRepository _context;
        private readonly Random _random;
        public AddressGenerator(PostalCodesRepository postalCodesRepo)
        {
            _random = new Random();
            _context = postalCodesRepo;
        }

        public Address GenerateAdress()
        {
            var address = new Address();
            address.Street = GenerateStreet();
            address.Number = GenerateNumber();
            address.Floor = GenerateFloor();
            address.Door = GenerateDoor();
            address.PostalCode = GeneratePostalCode();
            return address;
        }

        //Street. A random assortment of alphabetic characters
        public string GenerateStreet()
        {
            int length = _random.Next(5, 15);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        //Number. A number from 1 to 999 optionally followed by an uppercase letter (e.g., 43B)
        public string GenerateNumber()
        {
            int number = _random.Next(1, 1000);
            string letter = _random.Next(0, 2) == 1 ? ((char)_random.Next('A', 'Z' + 1)).ToString() : "";
            return $"{number}{letter}";
        }

        //Floor. Either “st” or a number from 1 to 29
        public string GenerateFloor(bool? isGroundFloor = null, int? specificFloor = null)
        {
            // Determine the value for the first decision
            var decision = isGroundFloor ?? (_random.Next(0, 2) == 0);

            // If decision is true, return "st"
            if (decision == true)
            {
                return "st";
            }

            // Determine the specific floor number if the decision was true
            int floorNumber = specificFloor ?? _random.Next(1, 30);
            return floorNumber.ToString();
        }

        //Door. “th”, “mf”, “tv”, a number from 1 to 50, or a lowercase letter optionally followed by a dash, then followed by one to three numeric digits (e.g., c3, d-14)
        public string GenerateDoor(int? choice = null)
        {
            if(choice == null)
            {
                choice = _random.Next(5);
            }
            switch (choice)
            {
                case 0: return "th";
                case 1: return "mf";
                case 2: return "tv";
                case 3: return _random.Next(1, 51).ToString();
                case 4:
                    char letter = (char)_random.Next('a', 'z' + 1);
                    string number = _random.Next(1, 200).ToString();
                    return $"{letter}-{number}";
                default: return "";
            }
        }

        //Postal code and town. Randomly extracted from the provided database addresses.sql
        public PostalCode GeneratePostalCode()
        {
            var postalCodes = _context.GetPostalCodes();

            if (postalCodes.Count == 0)
            {
                throw new Exception("No postal codes found in the database");
            }

            int randomIndex = _random.Next(postalCodes.Count);
            var postalCode = postalCodes[randomIndex];

            return postalCode;
        }
    }
}