using System.ComponentModel.DataAnnotations;

namespace PersonalTestDataGeneratorBackend
{
    public class PostalCode
    {
        [Key]
        public int PostCode { get; set; }
        public string TownName { get; set; }
        public string FullName
        {
            get
            {
                return PostCode + " " + TownName;
            }
        }

        public PostalCode()
        {

        }
    }
}
