namespace LibMan.Data.Repository.CustomRepository.Book
{
    public interface IBookRepository : IRepository<Domains.Book>
    {
        Task<List<Domains.Book>> GetAllBooksWithAuthor();

        // Returns null if not found
        Task<Domains.Book> GetBookBasedOnIdWithAuthor(int id);
        Task<Domains.BorrowTransaction> GetLastTransactionOfBookBasedOnId(int id);
    }
}
