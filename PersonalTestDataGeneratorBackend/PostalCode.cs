using System.ComponentModel.DataAnnotations;

namespace PersonalTestDataGeneratorBackend
{
    public class PostalCode
    {
        [Key]
        public int PostCode { get; set; }
        public string TownName { get; set; }

        public PostalCode()
        {

        }
    }
}
