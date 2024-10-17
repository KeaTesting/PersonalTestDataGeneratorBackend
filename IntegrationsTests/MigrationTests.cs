using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PersonalTestDataGeneratorBackend.DB;
using PersonalTestDataGeneratorBackend.Models;
using PersonalTestDataGeneratorBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationsTests
{
    public class MigrationTests
    {
        private readonly GeneratorDB _context;
        private readonly PostalCodesRepository _postalCodeRepository;
        private readonly DbContextOptionsBuilder<GeneratorDB> _options;
        public MigrationTests()
        {
            _options = new DbContextOptionsBuilder<GeneratorDB>()
            .UseSqlite("DataSource=:memory:");
            _context = new GeneratorDB(_options.Options);
            _context.Database.OpenConnection();
            _context.Database.Migrate();
            _postalCodeRepository = new PostalCodesRepository(_context);
        }

        [Fact]
        public async Task Migrations_Should_Apply_Successfully()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            Assert.Empty(pendingMigrations); 
        }


        [Fact]
        public void ContainsExistingPostCodes()
        {
            // Act
            var postalCodes = _postalCodeRepository.GetPostalCodes();

            // Assert
            Assert.True(postalCodes.Count != 0);
        }

        [Fact]
        public void ShouldRetrieveNewPostalCodesSuccessfully()
        {
            // Arrange
            var postCode = 1234;
            var townName = "test";
            _context.PostalCodes.Add(new PostalCode() { PostCode = postCode, TownName = townName });
            _context.SaveChanges();

            // Act
            var postalCodes = _postalCodeRepository.GetPostalCodes();

            // Assert
            Assert.Contains(postalCodes, u => u.TownName == townName && u.PostCode == postCode);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();  
            _context.Database.CloseConnection();  
            _context.Dispose();
        }
    }
}
