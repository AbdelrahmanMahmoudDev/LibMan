using LibMan.Data.Repository;
using LibMan.Domains;
using Microsoft.EntityFrameworkCore;
namespace LibMan.Business.Book.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<List<Domains.Book>> GetAllBooks()
        {
            IEnumerable<Domains.Book> result = await _UnitOfWork.Books.GetAllAsync();
            return result.ToList();
        }

        public async Task<Domains.Book> GetBookBasedOnId(int id)
        {
            return await _UnitOfWork.Books.GetByIdAsync(id);
        }

        public  async Task<Domains.Book> GetBookBasedOnIdWithAuthor(int id)
        {
            return await _UnitOfWork.CustomBookRepository.GetBookBasedOnIdWithAuthor(id);
        }
        public async Task<List<Domains.Book>> GetAllBooksWithAuthor()
        {
            return await _UnitOfWork.CustomBookRepository.GetAllBooksWithAuthor();
        }

        public async Task<bool> RemoveBook(int id)
        {
            try
            {
                Domains.Book targetBook = await _UnitOfWork.Books.GetByIdAsync(id);

                if (targetBook is not null)
                {
                    _UnitOfWork.Books.Delete(targetBook);

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

        public async Task<bool> SaveNew(Domains.Book newBook)
        {
            try
            {
                await _UnitOfWork.Books.AddAsync(newBook);
                await _UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveUpdate(Domains.Book newBook)
        {
            try
            {
                _UnitOfWork.Books.Update(newBook);
                await _UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task UpdateBookAvailability(int id)
        {
           Domains.Book chosenBook = await _UnitOfWork.Books.GetByIdAsync(id);

            chosenBook.IsAvailable = !chosenBook.IsAvailable;

            await SaveUpdate(chosenBook);
        }

    }
}
