
namespace LibMan.Business.Pagination
{
    public interface IPaginatedDateFilteredBookService : IPaginatedBookService
    {
        Task<PagedResult<Domains.Book>> GetBooksThatMatchBorrowDate(DateTime borrowDate, int pageNumber, int pageSize);
        Task<PagedResult<Domains.Book>> GetBooksThatMatchReturnDate(DateTime returnDate, int pageNumber, int pageSize);
        Task<PagedResult<Domains.Book>> GetBooksThatMatchBorrowAndReturnDates(DateTime borrowDate, DateTime returnDate, int pageNumber, int pageSize);
    }
}
