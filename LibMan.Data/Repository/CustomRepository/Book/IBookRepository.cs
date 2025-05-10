namespace LibMan.Data.Repository.CustomRepository.Book
{
    public interface IBookRepository : IRepository<Domains.Book>
    {
        Task<List<Domains.Book>> GetAllBooksWithAuthor();
        Task<List<Domains.Book>> GetAllBooksWithAuthorAndTransactions();

        // Returns null if not found
        Task<Domains.Book> GetBookBasedOnIdWithAuthor(int id);
        Task<Domains.Book> GetBookBasedOnIdWithAuthorAndTransactions(int id);
        Task<Domains.BorrowTransaction> GetLastTransactionOfBookBasedOnId(int id);
        Task<List<Domains.Book>> GetBooksThatMatchBorrowDate(DateTime borrowDate);
        Task<List<Domains.Book>> GetBooksThatMatchReturnDate(DateTime returnDate);
        Task<List<Domains.Book>> GetBooksThatMatchBorrowAndReturnDates(DateTime borrowDate, DateTime returnDate);
    }
}
