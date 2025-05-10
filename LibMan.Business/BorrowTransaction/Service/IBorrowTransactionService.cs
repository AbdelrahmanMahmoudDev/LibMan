using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMan.Business.BorrowTransaction.Service
{
    public interface IBorrowTransactionService
    {
        public Task<List<Domains.BorrowTransaction>> GetAllBorrowTransactions();
        public Task<List<Domains.BorrowTransaction>> GetAllBorrowTransactionsWithBook();
        public Task<Domains.BorrowTransaction> GetBorrowTransactionBasedOnId(int id);
        public Task<Domains.BorrowTransaction> GetBorrowTransactionBasedOnIdWithBook(int id);
        public Task<bool> SaveNew(int borrowedBookId);
        public Task<bool> SaveUpdate(Domains.BorrowTransaction newBorrowTransaction);
        public Task<bool> RemoveBorrowTransaction(int id);
    }
}
