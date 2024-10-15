using Microsoft.EntityFrameworkCore;
using PersonalTestDataGeneratorBackend.Repositories;

namespace PersonalTestDataGeneratorBackend.Generators
{
    public class AddressGenerator
    {
        private readonly IRandomGenerator _randomGenerator;
        private readonly PostalCodesRepo _context;

        public AddressGenerator(PostalCodesRepo postalCodesRepo, IRandomGenerator randomGenerator)
        {
            _context = postalCodesRepo;
            _randomGenerator = randomGenerator;
        }

        public string GenerateAdress()
        {
            string street = GenerateStreet();
            string number = GenerateNumber();
            string floor = GenerateFloor();
            string door = GenerateDoor();
            string postalCode = GeneratePostalCode();

            return $"{street} {number}, {floor}. {door}, {postalCode}";
        }

        //Street. A random assortment of alphabetic characters
        public string GenerateStreet()
        {
            int length = _randomGenerator.Next(5, 15);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_randomGenerator.Next(s.Length)]).ToArray());
        }

        //Number. A number from 1 to 999 optionally followed by an uppercase letter (e.g., 43B)
        public string GenerateNumber()
        {
            int number = _randomGenerator.Next(1, 1000);
            string letter = _randomGenerator.Next(0, 2) == 1 ? ((char)_randomGenerator.Next('A', 'Z' + 1)).ToString() : "";
            return $"{number}{letter}";
        }

        //Floor. Either “st” or a number from 1 to 29
        public string GenerateFloor()
        {
            return _randomGenerator.Next(0, 2) == 0 ? "st" : _randomGenerator.Next(1, 30).ToString();
        }

        //Door. “th”, “mf”, “tv”, a number from 1 to 50, or a lowercase letter optionally followed by a dash, then followed by one to three numeric digits (e.g., c3, d-14)
        public string GenerateDoor()
        {
            int choice = _randomGenerator.Next(5);
            switch (choice)
            {
                case 0: return "th";
                case 1: return "mf";
                case 2: return "tv";
                case 3: return _randomGenerator.Next(1, 51).ToString();
                case 4:
                    char letter = (char)_randomGenerator.Next('a', 'z' + 1);
                    string number = _randomGenerator.Next(1, 1000).ToString();
                    return $"{letter}-{number}";
                default: return "";
            }
        }

        //Postal code and town. Randomly extracted from the provided database addresses.sql
        public string GeneratePostalCode()
        {
            var postalCodes = _context.GetPostalCodes();

            if (postalCodes.Count == 0)
            {
                throw new Exception("No postal codes found in the database");
            }

            int randomIndex = _randomGenerator.Next(postalCodes.Count);
            var postalCode = postalCodes[randomIndex];

            return $"{postalCode.PostCode} {postalCode.TownName}";
        }
    }
}