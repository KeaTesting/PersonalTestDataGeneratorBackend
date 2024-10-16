using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PersonalTestDataGeneratorBackend.DB
{
    public class GeneratorDB : DbContext
    {
        public GeneratorDB(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PostalCode> PostalCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IServiceCollection services = new ServiceCollection();

            var binpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var builder = new ConfigurationBuilder()
                              .SetBasePath(binpath)
                              .AddJsonFile("appsettings.json")
                              .Build();

            var connectionString = builder.GetConnectionString("MySqlConnection");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
