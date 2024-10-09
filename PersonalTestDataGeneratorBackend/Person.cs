using System;

namespace PersonalTestDataGeneratorBackend
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Cpr { get; set; }

        private DateOnly _birthday;

        public Person()
        {

        }

        public DateOnly Birthday { get => _birthday; }

        public void SetBirthdayFromCpr(string cpr)
        {
            //Takes the first 8 digits of the cpr number (xx/xx/xx)
            //TODO: hvad hvis CPR ikke er formateret sådan der?
            string setDate = cpr.Substring(0, 10);

            if (DateOnly.TryParse(setDate, out DateOnly date))
            {
                _birthday = date;
            }
            else
            {
                throw new Exception("Invalid CPR");
            }
        }
    }
}
