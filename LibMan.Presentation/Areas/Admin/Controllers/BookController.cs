using LibMan.Business.Author.Service;
using LibMan.Business.Book.Service;
using LibMan.Domains;
using LibMan.Presentation.Areas.Admin.ViewModels;
using LibMan.Presentation.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LibMan.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IBookService _BookService;
        private readonly IAuthorService _AuthorService;

        public BookController(IBookService bookService, IAuthorService authorService)
        {
            _BookService = bookService;
            _AuthorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _BookService.GetAllBooksWithAuthor());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            if (Id is not null)
            {
                Book targetBook = await _BookService.GetBookBasedOnIdWithAuthor(Convert.ToInt32(Id));

                bookViewModel = new BookViewModel(targetBook);

                bookViewModel.AllAuthors = await _AuthorService.GetAllAuthors();

                return View("Edit", bookViewModel);
            }


            bookViewModel = new BookViewModel();
            bookViewModel.AllAuthors = await _AuthorService.GetAllAuthors();

            return View("Edit", bookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", bookViewModel);
            }

            bool IsSuccess = false;
            Book newBook = ViewModelToModel.BookViewModelToModel(bookViewModel);

            if (newBook.Id == 0)
                IsSuccess = await _BookService.SaveNew(newBook);
            else
                IsSuccess = await _BookService.SaveUpdate(newBook);

            if (!IsSuccess)
                return StatusCode(500, "A server side error occured while processing your request");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool IsSuccess = await _BookService.RemoveBook(id);

            if (!IsSuccess)
            {
                return StatusCode(500, "A server side error occured while processing your request");
            }
            return RedirectToAction("Index");
        }
    }
}
