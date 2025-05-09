using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibMan.Data.Repository.CustomRepository.Author
{
    public class AuthorRepository : GenericRepository<Domains.Author>, IAuthorRepository
    {
        public AuthorRepository(MainContext context) : base(context) { }

        public async Task<List<string>> GetAllAuthorNames() => await _DbSet.Select(e => e.FullName).ToListAsync();
    }
}
