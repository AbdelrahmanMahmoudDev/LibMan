using LibMan.Domains;

namespace LibMan.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }
        IRepository<BorrowTransaction> BorrowTransactions { get; }

        Task<int> SaveAsync();
    }
}
