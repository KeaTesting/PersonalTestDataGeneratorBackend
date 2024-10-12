using Microsoft.EntityFrameworkCore;
using PersonalTestDataGeneratorBackend.DB;
using PersonalTestDataGeneratorBackend.Generators;
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
            builder.Services.AddDbContext<GeneratorDB>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection")));
            });
            builder.Services.AddCors(p => p.AddPolicy("*", b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();


            app.MapGet("/cpr", () =>
            {
                var query = new PersonQuery()
                {
                    Address = false,
                    Birthday = false,
                    Cpr = true,
                    Gender = false,
                    Name = false,
                    Surname = false,
                    PhoneNumber = false
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            }); 
            app.MapGet("/name-gender", () =>
            {
                var query = new PersonQuery()
                {
                    Address = false,
                    Birthday = false,
                    Cpr = false,
                    Gender = true,
                    Name = true,
                    Surname = true,
                    PhoneNumber = false
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            }); ;
            app.MapGet("/name-gender-dob", () =>
            {
                var query = new PersonQuery()
                {
                    Address = false,
                    Birthday = true,
                    Cpr = false,
                    Gender = true,
                    Name = true,
                    Surname = true,
                    PhoneNumber = false
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            });
            app.MapGet("/cpr-name-gender", () =>
            {
                var query = new PersonQuery()
                {
                    Address = false,
                    Birthday = false,
                    Cpr = true,
                    Gender = true,
                    Name = true,
                    Surname = true,
                    PhoneNumber = false
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            });
            app.MapGet("/cpr-name-gender-dob", () =>
            {
                var query = new PersonQuery()
                {
                    Address = false,
                    Birthday = true,
                    Cpr = true,
                    Gender = true,
                    Name = true,
                    Surname = true,
                    PhoneNumber = false
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            });
            app.MapGet("/address", () =>
            {
                var query = new PersonQuery()
                {
                    Address = true,
                    Birthday = false,
                    Cpr = false,
                    Gender = false,
                    Name = false,
                    Surname = false,
                    PhoneNumber = false
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            });
            app.MapGet("/phone", () =>
            {
                var query = new PersonQuery()
                {
                    Address = false,
                    Birthday = false,
                    Cpr = false,
                    Gender = false,
                    Name = false,
                    Surname = false,
                    PhoneNumber = true
                };
                return PersonHelper.GeneratePersons(query, 1).First();

            });

            app.MapGet("/person", (int n) =>
            {
                var query = new PersonQuery()
                {
                    Address = true,
                    Birthday = true,
                    Cpr = true,
                    Gender = true,
                    Name = true,
                    Surname = true,
                    PhoneNumber = true
                };
                return PersonHelper.GeneratePersons(query, n);
            });

                var person = new Person();
                //if(query.)
                Console.WriteLine(JsonSerializer.Serialize(query));
            });
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("*");

            app.Run();
        }
    }
}