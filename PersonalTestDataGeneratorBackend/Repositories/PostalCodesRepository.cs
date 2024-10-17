using Microsoft.EntityFrameworkCore;
using PersonalTestDataGeneratorBackend.DB;
using PersonalTestDataGeneratorBackend.Models;
using System;

namespace PersonalTestDataGeneratorBackend.Repositories
{
    public class PostalCodesRepository
    {
        private readonly GeneratorDB _context;

        public PostalCodesRepository()
        {
            var options = new DbContextOptionsBuilder<GeneratorDB>().Options;
            _context = new GeneratorDB(options);
        }

        public virtual List<PostalCode> GetPostalCodes()
        {
            return _context.PostalCodes.ToList();
        }
    }
}
