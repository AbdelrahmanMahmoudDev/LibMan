using Microsoft.AspNetCore.Mvc;

namespace LibMan.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
