using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMan.Data.Repository;

namespace LibMan.Business.BorrowTransaction.Service
{
    public class BorrowTransactionService : IBorrowTransactionService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public BorrowTransactionService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<List<Domains.BorrowTransaction>> GetAllBorrowTransactions()
        {
            IEnumerable<Domains.BorrowTransaction> result = await _UnitOfWork.BorrowTransactions.GetAllAsync();
            return result.ToList();
        }

        public async Task<List<Domains.BorrowTransaction>> GetAllBorrowTransactionsWithBook()
        {
            return await _UnitOfWork.CustomBorrowTransactionRepository.GetAllBorrowTransactionsWithBook();
        }

        public async Task<Domains.BorrowTransaction> GetBorrowTransactionBasedOnId(int id)
        {
            return await _UnitOfWork.BorrowTransactions.GetByIdAsync(id);
        }

        public async Task<Domains.BorrowTransaction> GetBorrowTransactionBasedOnIdWithBook(int id)
        {
            return await _UnitOfWork.CustomBorrowTransactionRepository.GetBorrowTransactionBasedOnIdWithBook(id);
        }

        public async Task<bool> RemoveBorrowTransaction(int id)
        {
            try
            {
                Domains.BorrowTransaction targetBorrowTransaction = await _UnitOfWork.BorrowTransactions.GetByIdAsync(id);

                if (targetBorrowTransaction is not null)
                {
                    _UnitOfWork.BorrowTransactions.Delete(targetBorrowTransaction);

                    await _UnitOfWork.SaveAsync();

                    return true;
                }
                throw new ArgumentNullException();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveNew(int borrowedBookId)
        {
            Domains.BorrowTransaction newBorrowTransaction = new Domains.BorrowTransaction()
            {
                BorrowDate = DateTime.Now,
                BookId = borrowedBookId
            };

            try
            {
                await _UnitOfWork.BorrowTransactions.AddAsync(newBorrowTransaction);
                await _UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveUpdate(Domains.BorrowTransaction newBorrowTransaction)
        {
            try
            {
                _UnitOfWork.BorrowTransactions.Update(newBorrowTransaction);
                await _UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> StartReturnTransaction(int bookId)
        {
            Domains.BorrowTransaction targetBorrowTransaction = await _UnitOfWork.CustomBookRepository.GetLastTransactionOfBookBasedOnId(bookId);

            targetBorrowTransaction.ReturnDate = DateTime.Now;
            targetBorrowTransaction.Book.IsAvailable = true;

            bool IsSuccessful = await SaveUpdate(targetBorrowTransaction);

            return IsSuccessful;
        }
    }
}
