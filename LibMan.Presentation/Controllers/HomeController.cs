using LibMan.Business.Book.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibMan.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _BookService;

        public HomeController(IBookService bookService)
        {
            _BookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _BookService.GetAllBooksWithAuthor());
        }

    }
}
