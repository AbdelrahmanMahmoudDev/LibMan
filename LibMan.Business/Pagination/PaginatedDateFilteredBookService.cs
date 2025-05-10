using LibMan.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibMan.Business.Pagination
{
    public class PaginatedDateFilteredBookService : PaginatedBookService, IPaginatedDateFilteredBookService
    {

        public PaginatedDateFilteredBookService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<PagedResult<Domains.Book>> GetBooksThatMatchBorrowAndReturnDates(DateTime borrowDate, DateTime returnDate, int pageNumber, int pageSize)
        {
            var allBooks = await base.GetAllBooksWithAuthorAndTransactions();

            var pagedBooks = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var targetBooks = pagedBooks
                             .Where(b => b.BorrowTransactions
                                          .Any(b => b.ReturnDate != null && b.ReturnDate.Value.Date == returnDate.Date && b.BorrowDate.Date == borrowDate.Date))
                                          .ToList();

            var model = new PagedResult<Domains.Book>
            {
                Items = targetBooks,
                TotalItems = allBooks.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return model;
        }

        public async Task<PagedResult<Domains.Book>> GetBooksThatMatchBorrowDate(DateTime borrowDate, int pageNumber, int pageSize)
        {
            var allBooks = await base.GetAllBooksWithAuthorAndTransactions();

            var pagedBooks = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var targetBooks = pagedBooks
                             .Where(b => b.BorrowTransactions
                                          .Any(b => b.BorrowDate.Date == borrowDate.Date))
                                          .ToList();

            var model = new PagedResult<Domains.Book>
            {
                Items = targetBooks,
                TotalItems = allBooks.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return model;
        }

        public async Task<PagedResult<Domains.Book>> GetBooksThatMatchReturnDate(DateTime returnDate, int pageNumber, int pageSize)
        {
            var allBooks = await base.GetAllBooksWithAuthorAndTransactions();

            var pagedBooks = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var targetBooks = pagedBooks
                             .Where(b => b.BorrowTransactions
                                          .Any(b => b.ReturnDate != null && b.ReturnDate.Value.Date == returnDate.Date))
                                          .ToList();

            var model = new PagedResult<Domains.Book>
            {
                Items = targetBooks,
                TotalItems = allBooks.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return model;
        }
    }
}
