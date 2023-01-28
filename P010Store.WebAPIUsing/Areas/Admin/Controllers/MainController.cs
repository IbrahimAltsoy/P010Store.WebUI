using Microsoft.AspNetCore.Mvc;

namespace P010Store.WebAPIUsing.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
