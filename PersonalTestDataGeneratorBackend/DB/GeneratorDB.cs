using Microsoft.EntityFrameworkCore;

namespace PersonalTestDataGeneratorBackend.DB
{
    public class GeneratorDB : DbContext
    {
        public GeneratorDB(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PostalCode> PostalCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Port=3307;Database=mydatabase;Uid=root;Pwd=12345;", ServerVersion.AutoDetect("Server=localhost;Port=3307;Database=mydatabase;Uid=root;Pwd=12345;"));
            }
        }
    }
}
