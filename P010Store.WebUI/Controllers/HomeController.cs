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

        public HomeController(IService<Product> service, IService<Carousel> service1, IService<Brand> service2)
        {
            _service = service;
            _service1 = service1;
            _service2 = service2;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}