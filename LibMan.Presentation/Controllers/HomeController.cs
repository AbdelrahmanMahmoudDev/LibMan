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
        private readonly IBorrowTransactionService _BorrowTransactionService;

        public HomeController(IPaginatedBookService paginatedBookService, IBorrowTransactionService borrowTransactionService)
        {
            _PaginatedBookService = paginatedBookService;
            _BorrowTransactionService = borrowTransactionService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            return View(await _PaginatedBookService.GetPaginatedBooksAsync(pageNumber, pageSize));
        }

        public IActionResult StartReturnTransaction(int bookId)
        {
            _BorrowTransactionService.StartReturnTransaction(bookId);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> FilterBooksAjax(List<string> statuses, int pageNumber = 1, int pageSize = 5)
        {
            return PartialView("_BooksPartial", await _PaginatedBookService.GetPaginatedBooksWithFiltersAsync(statuses, pageNumber, pageSize));
        }
    }
}
