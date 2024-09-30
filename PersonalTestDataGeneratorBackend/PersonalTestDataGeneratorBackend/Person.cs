using System;

namespace PersonalTestDataGeneratorBackend
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }

        private DateOnly _birthday;


        public Person()
        {

        }

        public Person(string firstName, string lastName, string gender)
        {


        }

        public DateOnly Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }

        public void SetBirthdayFromCpr(string cpr)
        {
            //Takes the first 8 digits of the cpr number (xx/xx/xx)
            string test = cpr.Substring(0, 8);

            if(DateOnly.TryParse(test, out DateOnly date))
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
