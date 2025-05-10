using LibMan.Business.Book.Service;
using LibMan.Business.BorrowTransaction.Service;
using LibMan.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibMan.Presentation.Controllers
{
    public class IncomingJson
    {
        public int ChosenBookId { get; set; }
    }

    public class BorrowTransactionController : Controller
    {
        private readonly IBookService _BookService;
        private readonly IBorrowTransactionService _BorrowTransactionService;

        public BorrowTransactionController(IBookService bookService, IBorrowTransactionService borrowTransactionService)
        {
            _BookService = bookService;
            _BorrowTransactionService = borrowTransactionService;
        }

        public async Task<IActionResult> Index()
        {
            BookViewModel bookViewModel = new BookViewModel()
            {
                Id = 0,
                AllBooks = await _BookService.GetAllBooks()
            };
            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvailabilityAjax([FromBody] IncomingJson incomingJson)
        {
            if (incomingJson == null)
            {
                return BadRequest("No data received");
            }

            if(! await _BorrowTransactionService.SaveNew(incomingJson.ChosenBookId))
                return StatusCode(500, "A server side error occured while processing your request");

            await _BookService.UpdateBookAvailability(incomingJson.ChosenBookId);

            BookViewModel bookViewModel = new BookViewModel()
            {
                Id = 0,
                AllBooks = await _BookService.GetAllBooks()
            };

            return PartialView("_LiveBookList", bookViewModel);
        }
    }
}
