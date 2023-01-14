using Microsoft.AspNetCore.Mvc;
using P010Store.Data.Absract;
using P010Store.Entities;
using P010Store.Service.Absract;

namespace P010Store.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
            
        }
        public async Task<IActionResult> Index(int id)
        {
            var model = await _service.GetCategoryByProducts(id);
            return View(model);
        }
    }
}
