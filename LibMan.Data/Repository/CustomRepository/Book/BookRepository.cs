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

        public async Task<Domains.BorrowTransaction> GetLastTransactionOfBookBasedOnId(int id)
        {
            Domains.Book targetBook = (await _DbSet.Include(b => b.BorrowTransactions)
                                                   .FirstOrDefaultAsync(b => b.Id == id))!;

            Domains.BorrowTransaction targetBorrowTransaction = (targetBook?.BorrowTransactions
                                                                            .OrderByDescending(b => b.BorrowDate)
                                                                            .FirstOrDefault())!;

            return targetBorrowTransaction;
        }
    }
}
