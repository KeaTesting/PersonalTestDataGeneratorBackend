using System;

namespace PersonalTestDataGeneratorBackend.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public string? Cpr { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        private DateOnly? _birthday;

        public Person()
        {

        }

        public DateOnly? Birthday { get => _birthday; set => _birthday = value; }

    }
}
