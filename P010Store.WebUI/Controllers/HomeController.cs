using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;
using P010Store.WebUI.Models;
using System.Diagnostics;

namespace P010Store.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _service;
        private readonly IService<Carousel> _service1;
        private readonly IService<Brand> _service2;
        private readonly IService<Contact> _service3;

        public HomeController(IService<Product> service, IService<Carousel> service1, IService<Brand> service2, IService<Contact> service3)
        {
            _service = service;
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Carousels = await _service1.GetAllAsync(),
                Products = await _service.GetAllAsync(p => p.IsHome == true),
                Brands = await _service2.GetAllAsync()
            };
            return View(model);
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("Iletisim")]
        public IActionResult ContantUs()
        {
            return View();
        }
        [Route("Iletisim"), HttpPost]
        public async Task<IActionResult> ContantUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service3.AddAsync(contact);
                    await _service3.SaveChangesAsync();
                    TempData["mesaj"] = "<div class='alert alert-success'>Mesajınız gönderildi. Teşekkürler </div>";
                    return RedirectToAction(nameof(ContantUs));
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata mesaj mesajınızgönderilmedi.");
                }
            }
            return View(contact);
        }
        //public async Task<IActionResult> Brands(int id)
        //{
        //    var model = await _service2.FindAsync(id);
        //    return View(model);
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}