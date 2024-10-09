using System;
using System.IO;
using System.Text.Json;

namespace PersonalTestDataGeneratorBackend
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(p => p.AddPolicy("*", b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
            app.MapPost("/person", (PersonQuery query) =>
            {
                Console.WriteLine(JsonSerializer.Serialize(query));
            });
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("*");

            app.Run();
        }
    }
}