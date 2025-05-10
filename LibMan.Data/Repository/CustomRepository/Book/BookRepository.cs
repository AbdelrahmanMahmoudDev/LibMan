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

        public async Task<List<Domains.Book>> GetBooksThatMatchBorrowDate(DateTime borrowDate)
        {
            var allBooksWithBorrowTransactions = _DbSet.Include(b => b.BorrowTransactions);

            var targetBooks = await allBooksWithBorrowTransactions
                             .Where(b => b.BorrowTransactions
                                          .Any(b => b.BorrowDate.Date == borrowDate.Date))
                                          .ToListAsync();

            return targetBooks;
        }

        public async Task<List<Domains.Book>> GetBooksThatMatchReturnDate(DateTime returnDate)
        {
            var allBooksWithBorrowTransactions = _DbSet.Include(b => b.BorrowTransactions);

            var targetBooks = await allBooksWithBorrowTransactions
                             .Where(b => b.BorrowTransactions
                                          .Any(b => b.ReturnDate.Value.Date == returnDate.Date))
                                          .ToListAsync();

            return targetBooks;
        }

        public async Task<List<Domains.Book>> GetBooksThatMatchBorrowAndReturnDates(DateTime borrowDate, DateTime returnDate)
        {
            var allBooksWithBorrowTransactions = _DbSet.Include(b => b.BorrowTransactions);

            var targetBooks = await allBooksWithBorrowTransactions
                             .Where(b => b.BorrowTransactions
                                          .Any(b => b.ReturnDate.Value.Date == returnDate.Date && b.BorrowDate.Date == borrowDate.Date))
                                          .ToListAsync();

            return targetBooks;
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

        public async Task<List<Domains.Book>> GetAllBooksWithAuthorAndTransactions()
        {
                return await _DbSet.Include(books => books.Author)
                                   .Include(books  => books.BorrowTransactions)
                                   .ToListAsync();
        }

        public async Task<Domains.Book> GetBookBasedOnIdWithAuthorAndTransactions(int id)
        {
            return await _DbSet.Include(books => books.Author)
                               .Include(books => books.BorrowTransactions)
                               .FirstOrDefaultAsync(book => book.Id == id);
        }
    }
}
