namespace LibMan.Data.Repository.CustomRepository.BorrowTransaction
{
    public interface IBorrowTransactionRepository : IRepository<Domains.BorrowTransaction>
    {
        public Task<List<Domains.BorrowTransaction>> GetAllBorrowTransactionsWithBook();
        public Task<Domains.BorrowTransaction> GetBorrowTransactionBasedOnIdWithBook(int id);
    }
}
