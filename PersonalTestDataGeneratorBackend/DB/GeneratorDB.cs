using Microsoft.EntityFrameworkCore;

namespace PersonalTestDataGeneratorBackend.DB
{
    public class GeneratorDB : DbContext
    {
        public GeneratorDB(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PostalCode> PostalCodes { get; set; }

    }
}
