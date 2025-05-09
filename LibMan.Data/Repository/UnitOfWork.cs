using LibMan.Data.Repository.CustomRepository.Author;
using LibMan.Data.Repository.CustomRepository.Book;
using LibMan.Domains;

namespace LibMan.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _Context;
        public IRepository<Book> Books { get; private set; }
        public IRepository<Author> Authors { get; private set; }
        public IRepository<BorrowTransaction> BorrowTransactions { get; private set; }
        public IBookRepository CustomBookRepository { get; private set; }
        public IAuthorRepository CustomAuthorRepository { get; private set; }

        public UnitOfWork(MainContext context)
        {
            _Context = context;
            Books = new GenericRepository<Book>(_Context);
            Authors = new GenericRepository<Author>(_Context);
            BorrowTransactions = new GenericRepository<BorrowTransaction>(_Context);
            CustomBookRepository = new BookRepository(_Context);
        }

        public void Dispose() => _Context.Dispose();
        public async Task<int> SaveAsync() => await _Context.SaveChangesAsync();
    }
}
