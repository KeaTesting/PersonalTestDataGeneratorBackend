using PersonalTestDataGeneratorBackend.Generators;
using System.Text.Json;

namespace PersonalTestDataGeneratorBackend
{
    public static class PersonHelper
    {

        public static string CleanCpr(string cpr)
        {
            var clean = cpr.Replace("/", "");
            var partOne = clean.Substring(0, 4);
            var partTwo = clean.Substring(6);
            var finalCpr = $"{partOne}{partTwo}";
            return finalCpr;
        }
        public static List<Person> GeneratePersons(PersonQuery query, int amount)
        {
            List<Person> people = Reader("person-names.json");
            Random random = new Random();
            List<Person> returnvalue = new List<Person>();
            for (int i = 0; i < amount; i++)
            {
                var person = new Person();
                var gender = random.Next(0, 2) == 0 ? "female" : "male";
                var cpr = GenerateCprWithGender(gender);
                if(query.Cpr)
                {
                    person.Cpr = CleanCpr(cpr);
                }
                if(query.Gender)
                {
                    person.Gender = gender;
                }
                if(query.Birthday)
                {
                    person.Birthday = SetBirthdayFromCpr(cpr);
                }
                if (query.Name)
                {
                    var genderedNameList = people.Where(x => x.Gender == gender);
                    person.Name = genderedNameList.ToArray()[random.Next(0, genderedNameList.Count() - 1)].Name;
                    person.Surname = genderedNameList.ToArray()[random.Next(0, genderedNameList.Count() - 1)].Surname;
                }
                if(query.PhoneNumber)
                {
                    person.PhoneNumber = PhoneNumberGenerator.GeneratePhoneNumber();
                }
                if (query.Address)
                {
                    //TODO
                }
                returnvalue.Add(person);
            }
            return returnvalue;
        }
        //Mener denne metode kan/skal bruges til at generere en random person fra json filen.
        //Skal "bare" parse en int for antallet af personer man vil have
        public static List<Person> GenerateData(int amount)
        {

            // Deserialize the JSON file
            List<Person> people = Reader("person-names.json");

            //Makes list to hold random people
            List<Person> randomPeople = new List<Person>();

            // Generate random people after given amount
            if (people != null && people.Count > 0)
            {
                for (int i = 0; i < amount; i++)
                {


                    try
                    {
                        Random random = new Random();
                        int randomIndex = random.Next(people.Count);
                        Person randomPerson = people[randomIndex];
                        randomPeople.Add(randomPerson);

                    }
                    catch
                    {
                        Console.WriteLine("Random failed");
                    }

                }
            }
            else
            {
                Console.WriteLine("No data found");
                return null;
            }

            return randomPeople;
        }

        static List<Person> Reader(string filePath)//Refaktore til en anden return type. Måske parse fil navnet med i metoden??
        {
            //Sets an option to ignore the case of property names during deserialization.
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // Read the JSON file
            string jsonFilePath = filePath; // Path to your JSON file
            string jsonString = File.ReadAllText(jsonFilePath);

            // Parse the JSON file
            var jsonDoc = JsonDocument.Parse(jsonString);

            // Get the persons element
            var personsElements = jsonDoc.RootElement.GetProperty("persons");

            return JsonSerializer.Deserialize<List<Person>>(personsElements.ToString(), options);
        }

        //Generates a random CPR number.
        //Makes sure if the day number is true to the month and if it's a leap year.
        //Then formats the CPR number to look like a CPR number.
        //So SetBirthdayFromCpr (Person.cs) can parse it.
        //Runnning number is based on the year.
        //000-499 on years 1900-1999
        //750-999 on years 2000-2036
        public static string GenerateCprWithGender(string gender)
        {
            try
            {
                bool isLeapYear;
                string day;
                string month;
                string year;
                string runningNumber;
                string genderNumber = "";
                string Cpr;

                Random random = new Random();

                //Generates a random year
                int yearInt = random.Next(1900, 2024);

                if (yearInt % 4 == 0 && yearInt % 100 != 0 || yearInt % 400 == 0)
                {
                    isLeapYear = true;
                }
                else
                {
                    isLeapYear = false;
                }

                //Generates a random day
                int monthInt = random.Next(1, 12);
                if (monthInt == 2 && isLeapYear == false)
                {
                    day = random.Next(1, 28).ToString();
                }
                else if (monthInt == 2 && isLeapYear == true)
                {
                    day = random.Next(1, 29).ToString();
                }
                else if (monthInt == 4 || monthInt == 6 || monthInt == 9 || monthInt == 11)
                {
                    day = random.Next(1, 30).ToString();
                }
                else
                {
                    day = random.Next(1, 31).ToString();
                }

                //checks to see if month, day and year is less than 10 and adds a 0 in front of the number.
                if (monthInt < 10)
                {
                    month = "0" + monthInt.ToString();
                }
                else
                {
                    month = monthInt.ToString();
                }

                if (int.Parse(day) < 10)
                {
                    day = "0" + day;
                }

                year = yearInt.ToString();

                //Assign running number based on year
                if (yearInt < 2000)
                {
                    runningNumber = random.Next(0, 499).ToString();
                    if (runningNumber.Length == 1)
                    {
                        runningNumber = "00" + runningNumber;
                    }
                    else if (runningNumber.Length == 2)
                    {
                        runningNumber = "0" + runningNumber;
                    }
                }
                else
                {
                    runningNumber = random.Next(750, 999).ToString();
                }

                if (gender == "male")
                {
                    int rand = random.Next(1, 5);

                    switch (rand) //I will break the Geneva convention on anyone who deletes this code
                    {
                        case 1:
                            genderNumber = "1";
                            break;
                        case 2:
                            genderNumber = "3";
                            break;
                        case 3:
                            genderNumber = "5";
                            break;
                        case 4:
                            genderNumber = "7";
                            break;
                        case 5:
                            genderNumber = "9";
                            break;
                    }
                }
                else
                {
                    int rand = random.Next(1, 4);

                    switch (rand) //I will break the Geneva convention on anyone who deletes this code
                    {
                        case 1:
                            genderNumber = "2";
                            break;
                        case 2:
                            genderNumber = "4";
                            break;
                        case 3:
                            genderNumber = "6";
                            break;
                        case 4:
                            genderNumber = "8";
                            break;
                    }
                }

                //Formatted, so it looks like a CPR number and SetBirthdayFromCpr can parse it
                Cpr = $"{day}/{month}/{year}-{runningNumber}{genderNumber}";

                return Cpr;
            }
            catch
            {
                Console.WriteLine("CPR generation failed");
                return null;
            }
        }
        public static DateOnly SetBirthdayFromCpr(string cpr)
        {
            //Takes the first 8 digits of the cpr number (xx/xx/xx)
            //TODO: hvad hvis CPR ikke er formateret sådan der?
            string setDate = cpr.Substring(0, 10);
            DateOnly value;
            if (DateOnly.TryParse(setDate, out DateOnly date))
            {
                value = date;
            }
            else
            {
                throw new Exception("Invalid CPR");
            }
            return value;
        }


    }
}
