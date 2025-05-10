using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMan.Data.Repository.CustomRepository.Book;
using Microsoft.EntityFrameworkCore;

namespace LibMan.Data.Repository.CustomRepository.BorrowTransaction
{
    public class BorrowTransactionRepository : GenericRepository<Domains.BorrowTransaction>, IBorrowTransactionRepository
    {
        public BorrowTransactionRepository(MainContext context) : base(context) { }
        public async Task<List<Domains.BorrowTransaction>> GetAllBorrowTransactionsWithBook()
        {
            return await _DbSet.Include(b => b.Book)
                   .ToListAsync();
        }

        public async Task<Domains.BorrowTransaction> GetBorrowTransactionBasedOnIdWithBook(int id)
        {
            return await _DbSet.Include(b => b.Book)
                               .FirstOrDefaultAsync(b  => b.Id == id);
        }
    }
}
