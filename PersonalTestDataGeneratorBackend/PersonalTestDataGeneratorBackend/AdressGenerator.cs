namespace PersonalTestDataGeneratorBackend.PersonalTestDataGeneratorBackend
{
    public class AdressGenerator
    {
        private static readonly Random random = new();

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
        private string GenerateStreet()
        {
            int length = random.Next(5, 15);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Number. A number from 1 to 999 optionally followed by an uppercase letter (e.g., 43B)
        private string GenerateNumber()
        {
            int number = random.Next(1, 1000);
            char? letter = random.Next(0, 2) == 1 ? (char?)random.Next('A', 'Z' + 1) : null;
            return letter.HasValue ? $"{number}{letter}" : number.ToString();
        }

        //Floor. Either “st” or a number from 1 to 99
        private string GenerateFloor()
        {
            return random.Next(0, 2) == 0 ? "st" : random.Next(1, 30).ToString(); 
        }

        //Door. “th”, “mf”, “tv”, a number from 1 to 50, or a lowercase letter optionally followed by a dash, then followed by one to three numeric digits (e.g., c3, d-14)
        private string GenerateDoor()
        {
            int choice = random.Next(0, 5);
            switch (choice)
            {
                case 0: return "th";
                case 1: return "mf";
                case 2: return "tv";
                case 3: return random.Next(1, 51).ToString();
                case 4:
                    char letter = (char)random.Next('a', 'z' + 1);
                    string number = random.Next(0, 1000).ToString();
                    return $"{letter}-{number}";
                default: return "";
            }
        }

        //Postal code and town. Randomly extracted from the provided database addresses.sql
        private static string GeneratePostalCode()
        {
            return null;
        }
    }
}