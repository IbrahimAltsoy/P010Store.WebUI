﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;
using P010Store.WebUI.Utils;
using System.Drawing.Drawing2D;

namespace P010Store.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CarouselController : Controller
    {
        private readonly IService<Carousel> _service;

        public CarouselController(IService<Carousel> service)
        {
            _service = service;
        }

        // GET: CarouselController
        public async Task<ActionResult> IndexAsync()
        {
            var model =await _service.GetAllAsync();
            return View(model);
        }

        // GET: CarouselController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarouselController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarouselController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Carousel carousel, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) carousel.Image = await FileHelpers.FileLoaderAsync(Image);
                    await _service.AddAsync(carousel);
                    await _service.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexAsync));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(carousel);
        }

        // GET: CarouselController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarouselController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Carousel carousel, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) carousel.Image = await FileHelpers.FileLoaderAsync(Image);
                    _service.Update(carousel);
                    await _service.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexAsync));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(carousel);
        }

        // GET: CarouselController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarouselController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Carousel carousel)
        {
            try
            {
                FileHelpers.FieRemover(carousel.Image);
                _service.Delete(carousel);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
