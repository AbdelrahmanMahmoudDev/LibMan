using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMan.Data.Models;

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
