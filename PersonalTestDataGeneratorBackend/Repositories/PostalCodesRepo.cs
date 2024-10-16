using Microsoft.EntityFrameworkCore;
using PersonalTestDataGeneratorBackend.DB;
using System;

namespace PersonalTestDataGeneratorBackend.Repositories
{
    public class PostalCodesRepo
    {
        private readonly GeneratorDB _context;

        public PostalCodesRepo()
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
