using LibMan.Business.Book.Service;
using LibMan.Business.BorrowTransaction.Service;
using LibMan.Business.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibMan.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaginatedBookService _PaginatedBookService;
        private readonly IBookService _BookService;
        private readonly IBorrowTransactionService _BorrowTransactionService;
        private readonly IPaginatedDateFilteredBookService _PaginatedDateFilteredBookService;

        public HomeController(IPaginatedBookService paginatedBookService, IBorrowTransactionService borrowTransactionService, IBookService bookService, IPaginatedDateFilteredBookService paginatedDateFilteredBookService)
        {
            _PaginatedBookService = paginatedBookService;
            _BorrowTransactionService = borrowTransactionService;
            _BookService = bookService;
            _PaginatedDateFilteredBookService = paginatedDateFilteredBookService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4, DateTime? borrowDate = null, DateTime? returnDate = null)
        {
            if (borrowDate.HasValue && returnDate.HasValue)
            {
                return View(await _PaginatedDateFilteredBookService.GetBooksThatMatchBorrowAndReturnDates((DateTime)borrowDate, (DateTime)returnDate, pageNumber, pageSize));
            }
            else if (borrowDate.HasValue && returnDate == null)
            {
                return View(await _PaginatedDateFilteredBookService.GetBooksThatMatchBorrowDate((DateTime)borrowDate, pageNumber, pageSize));
            }
            else if (borrowDate == null && returnDate.HasValue)
            {
                return View(await _PaginatedDateFilteredBookService.GetBooksThatMatchReturnDate((DateTime)returnDate, pageNumber, pageSize));
            }
            return View(await _PaginatedBookService.GetPaginatedBooksAsync(pageNumber, pageSize));
        }

        public IActionResult StartReturnTransaction(int bookId)
        {
            _BorrowTransactionService.StartReturnTransaction(bookId);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> FilterBooksAjax(List<string> statuses, int pageNumber = 1, int pageSize = 4)
        {
            return PartialView("_BooksPartial", await _PaginatedBookService.GetPaginatedBooksWithFiltersAsync(statuses, pageNumber, pageSize));
        }
    }
}
