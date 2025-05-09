using LibMan.Business.Book.Service;
using LibMan.Business.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace LibMan.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaginatedBookService _PaginatedBookService;

        public HomeController(IPaginatedBookService paginatedBookService)
        {
            _PaginatedBookService = paginatedBookService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 1)
        {
            return View(await _PaginatedBookService.GetPaginatedBooksAsync(pageNumber, pageSize));
        }

    }
}
