using LibMan.Business.Author.Service;
using LibMan.Domains;
using LibMan.Presentation.Areas.Admin.ViewModels;
using LibMan.Presentation.Helpers;
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
            return View(await _AuthorService.GetAllAuthors());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id is not null)
            {
                Author targetAuthor = await _AuthorService.GetAuthorBasedOnId(Convert.ToInt32(Id));
                AuthorViewModel authorViewModel = new AuthorViewModel(targetAuthor);

                return View("Edit", authorViewModel);
            }

            return View("Edit", new AuthorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AuthorViewModel authorViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit", authorViewModel);
            }

            bool IsSuccess = false;
            Author newAuthor = ViewModelToModel.AuthorViewModelToModel(authorViewModel);

            if (newAuthor.Id == 0)
                IsSuccess = await _AuthorService.SaveNew(newAuthor);
            else
                IsSuccess = await _AuthorService.SaveUpdate(newAuthor);

            if (!IsSuccess)
                return StatusCode(500, "A server side error occured while processing your request");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool IsSuccess = await _AuthorService.RemoveAuthor(id);

            if (!IsSuccess)
            {
                return StatusCode(500, "A server side error occured while processing your request");
            }
            return RedirectToAction("Index");
        }
    }
}
