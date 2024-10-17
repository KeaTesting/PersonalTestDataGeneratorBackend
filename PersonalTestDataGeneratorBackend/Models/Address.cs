using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.IO;

namespace PersonalTestDataGeneratorBackend.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Floor { get; set; }
        public string Door { get; set; } 
        public PostalCode PostalCode { get; set; }
        public override string ToString()
        {
            return $"{Street} {Number}, {Floor}. {Door}, {PostalCode.FullName}";
        }
    }
}
