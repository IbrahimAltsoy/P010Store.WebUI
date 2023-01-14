﻿using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;

namespace P010Store.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync(p=>p.IsActive);
            return View(model);
        }
    }
}
