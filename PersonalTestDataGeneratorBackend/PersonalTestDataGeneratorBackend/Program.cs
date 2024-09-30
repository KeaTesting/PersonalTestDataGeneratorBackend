using PersonalTestDataGeneratorBackend;
using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        //Generate persons
        var person = GenerateData(10);


        //rough birthday from cpr
        Person birthday = new Person();
        birthday.SetBirthdayFromCpr(GenerateCpr());
        DateOnly date = birthday.Birthday;
        var cleanDate = date.ToString("dd/MM/yy");

        //Generates Cpr
        var cpr = GenerateCpr();


        app.MapGet("/", () => person);
        app.MapGet("/cpr", () => cpr);
        app.MapGet("/birthday", () => cleanDate);

        app.Run();
    }


    //Mener denne metode kan/skal bruges til at generere en random person fra json filen.
    //Skal "bare" parse en int for antallet af personer man vil have
    static List<Person> GenerateData(int amount)
    {
        // Read the JSON file
        string jsonFilePath = "person-names.json"; // Path to your JSON file
        string jsonString = File.ReadAllText(jsonFilePath);

        //Sets an option to ignore the case of property names during deserialization.
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        // Parse the JSON file
        var jsonDoc = JsonDocument.Parse(jsonString);

        // Get the persons element
        var personsElements = jsonDoc.RootElement.GetProperty("persons");

        // Deserialize the JSON file
        List<Person> people = JsonSerializer.Deserialize<List<Person>>(personsElements.ToString(), options);

        //Makes list to hold random people
        List<Person> randomPeople = new List<Person>();

        // Generate random people after given amount
        while (amount > 0)
        {
            if (people != null && people.Count > 0)
            {
                Random random = new Random();

                int randomIndex = random.Next(people.Count);
                Person randomPerson = people[randomIndex];
                randomPeople.Add(randomPerson);
                amount--;
            }
            else
            {
                Console.WriteLine("No data found");
                return null;
            }
        }

        return randomPeople;
    }

    //Generates a random CPR number.
    //Makes sure if the day number is true to the month and if it's a leap year.
    //Then formats the CPR number to look like a CPR number.
    //So SetBirthdayFromCpr (Person.cs) can parse it.
    static string GenerateCpr()
    {
        bool isLeapYear;
        string day;
        string month;
        string year;
        string lastFour;
        string Cpr;

        Random random = new Random();

        //Generates a random year
        int yearInt = random.Next(0, 24);

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

        if(int.Parse(day) < 10)
        {
            day = "0" + day;
        }


        if (yearInt < 10)
        {
            year = "0" + yearInt.ToString();
        }
        else 
        {  
            year = yearInt.ToString(); 
        }

        //This prolly should be better made to hold it true to CPR rules
        lastFour = random.Next(1000, 9999).ToString();

        Console.WriteLine(day);
        Console.WriteLine(month);
        Console.WriteLine(year);
        Console.WriteLine(lastFour);

        //Formatted, so it looks like a CPR number and SetBirthdayFromCpr can parse it
        Cpr = $"{day}/{month}/{year}-{lastFour}";

        return Cpr;
    }




}