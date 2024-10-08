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
        List<Person> person = PersonHelper.GenerateData(1);

        
        //This could prolly be a method in itself
        //Example for a complete Person data (for now)
        foreach (var item in person)
        {
            var cpr = PersonHelper.GenerateCprWithGender(item.Gender);
            item.SetBirthdayFromCpr(cpr); //Cannot for the life of me get this to show as dd/MM/yy


            item.Cpr = PersonHelper.CleanCpr(cpr); //removes "/" from cpr

        }

        app.MapGet("/", () => person);


        app.Run();
    }
}