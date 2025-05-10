using LibMan.Data.Repository.CustomRepository.Author;
using LibMan.Data.Repository.CustomRepository.Book;
using LibMan.Data.Repository.CustomRepository.BorrowTransaction;
using LibMan.Domains;

namespace LibMan.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }
        IRepository<BorrowTransaction> BorrowTransactions { get; }
        IBookRepository CustomBookRepository { get; }
        IAuthorRepository CustomAuthorRepository { get; }
        IBorrowTransactionRepository CustomBorrowTransactionRepository { get; }

        Task<int> SaveAsync();
    }
}
