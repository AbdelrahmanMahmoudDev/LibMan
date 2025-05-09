using Microsoft.EntityFrameworkCore;

namespace LibMan.Data.Repository.CustomRepository.Book
{ 
    public class BookRepository : GenericRepository<Domains.Book>, IBookRepository
    {
        public BookRepository(MainContext context) : base(context) { }

        public async Task<List<Domains.Book>> GetAllBooksWithAuthor()
        {
            return await _DbSet.Include(books => books.Author)
                               .ToListAsync();
        }

        public async Task<Domains.Book> GetBookBasedOnIdWithAuthor(int id)
        {
            return await _DbSet.Include(books => books.Author)
                   .FirstOrDefaultAsync(book => book.Id == id);
        }
    }
}
