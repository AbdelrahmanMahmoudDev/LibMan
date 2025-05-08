using LibMan.Business.Author;
using LibMan.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibMan.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _AuthorService;

        public AuthorController(IAuthorService authorService)
        {
            _AuthorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _AuthorService.GetAllAuthors());
        }
    }
}
