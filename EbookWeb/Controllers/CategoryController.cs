using Microsoft.AspNetCore.Mvc;

namespace EbookWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
