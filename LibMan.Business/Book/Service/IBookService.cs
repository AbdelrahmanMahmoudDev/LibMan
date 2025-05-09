namespace LibMan.Business.Book.Service
{
    public interface IBookService
    {
        public Task<List<Domains.Book>> GetAllBooks();
        public Task<List<Domains.Book>> GetAllBooksWithAuthor();
        public Task<Domains.Book> GetBookBasedOnId(int id);
        public Task<Domains.Book> GetBookBasedOnIdWithAuthor(int id);
        public Task<bool> SaveNew(Domains.Book newBook);
        public Task<bool> SaveUpdate(Domains.Book newBook);
        public Task<bool> RemoveBook(int id);
    }
}
