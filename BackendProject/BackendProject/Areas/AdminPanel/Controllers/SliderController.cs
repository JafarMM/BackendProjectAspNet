using BackendProject.Areas.Utils;
using BackendProject.DataAccesLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SliderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var slider = _dbContext.Slider.OrderByDescending(x => x.Id).ToList();
            return View(slider);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var slider = await _dbContext.Slider.FindAsync(id);
            if (id == null)
                return NotFound();

            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo field cannot be empty");
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Yuklediyiniz shekil deyildir!");
                return View();
            }
            if (!slider.Photo.IsSizeAllowed(4000))
            {
                ModelState.AddModelError("Photo", "Yuklediyiniz sheklin olchusu 4 mb dan az olmalidir!");
                return View();
            }
            var fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, "slider", slider.Photo);

            slider.Image = fileName;

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _dbContext.Slider.AddAsync(slider);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var slider = await _dbContext.Slider.FindAsync(id);
            if (slider == null)
                return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int? id)
        {
            if (id == null)
                return NotFound();

            var slider = await _dbContext.Slider.FindAsync(id);
            if (slider == null)
                return NotFound();

            var path = Path.Combine(Constants.ImageFolderPath, "slider", slider.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _dbContext.Slider.Remove(slider);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var slider = await _dbContext.Slider.FindAsync(id);
            if (slider == null)
                return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
                return NotFound();

            if (id != slider.Id)
                return BadRequest();

            var Slider = await _dbContext.Slider.FindAsync(id);
            if (Slider == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var fileName = Slider.Image;

            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Yukledyiniz shekil deyildir!");
                    return View();
                }

                if (!slider.Photo.IsSizeAllowed(4000))
                {
                    ModelState.AddModelError("Photo", "Yuklediyiniz sheklin olchusu 4 mb dan chox olmamalidir!");
                    return View();
                }

                var path = Path.Combine(Constants.ImageFolderPath, "slider", Slider.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                fileName = await FileUtil.GenerateFileAsync(Constants.ImageFolderPath, "slider", slider.Photo);
            }

            Slider.Image = fileName;
            Slider.Title = slider.Title;
            Slider.Description = slider.Description;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
