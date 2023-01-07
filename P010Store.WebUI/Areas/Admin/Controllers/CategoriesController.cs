using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;
using P010Store.WebUI.Utils;
using System.Drawing.Drawing2D;

namespace P010Store.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoriesController : Controller

    {
        public CategoriesController(IService<Category> service)
        {
            _service = service;
        }
        private readonly IService<Category> _service;
        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var model =await _service.GetAllAsync();
            return View(model);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Category category, IFormFile? Image)
        {
            if (ModelState.IsValid) // Model class ımız olan brand nesnesinin validasyon için koyduğumuz kurallarınıa (örneğin marka adı required-boş geçilemez gibi) uyulmuşsa
            {
                try
                {
                    if (Image is not null) 
                    {
                        category.Image = await FileHelpers.FileLoaderAsync(Image);
                    } 
                   
                    _service.Add(category);
                    _service.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create2(Category category, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) category.Image = await FileHelpers.FileLoaderAsync(Image);
                    await _service.AddAsync(category);
                    await _service.SaveChangesAsync();
                    return RedirectToAction("Create", "Products");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(category);
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category category, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) 
                    {
                        category.Image = await FileHelpers.FileLoaderAsync(Image);
                    } 
                    _service.Update(category);
                    _service.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(category);
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View();
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _service.Delete(category);
                _service.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
