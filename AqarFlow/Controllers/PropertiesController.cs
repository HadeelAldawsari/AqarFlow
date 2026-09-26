using AqarFlow.Models;
using AqarFlow.Repositories;
using AqarFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Only logged-in users can access Properties
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PropertiesController(
            IPropertyRepository propertyRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _propertyRepository = propertyRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        // =========================
        // INDEX
        // Display all properties
        // =========================
        public IActionResult Index()
        {
            var properties = _propertyRepository.GetAll();

            return View(properties);
        }


        // =========================
        // CREATE - GET
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // =========================
        // CREATE - POST
        // Add new property with image
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            PropertyCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? imagePath = null;

            if (model.ImageFile != null &&
                model.ImageFile.Length > 0)
            {
                imagePath = await SaveImage(model.ImageFile);
            }

            var property = new Property
            {
                Title = model.Title,
                PropertyType = model.PropertyType,
                City = model.City,
                Area = model.Area,
                Price = model.Price,
                Bedrooms = model.Bedrooms,
                Bathrooms = model.Bathrooms,
                PropertySize = model.PropertySize,
                Status = model.Status,
                Description = model.Description,
                ImagePath = imagePath,
                CreatedAt = DateTime.Now
            };

            _propertyRepository.Add(property);
            _propertyRepository.Save();

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DETAILS
        // =========================
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = _propertyRepository.GetById(id.Value);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }


        // =========================
        // EDIT - GET
        // =========================
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = _propertyRepository.GetById(id.Value);

            if (property == null)
            {
                return NotFound();
            }

            var model = new PropertyEditViewModel
            {
                Id = property.Id,
                Title = property.Title,
                PropertyType = property.PropertyType,
                City = property.City,
                Area = property.Area,
                Price = property.Price,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                PropertySize = property.PropertySize,
                Status = property.Status,
                Description = property.Description,
                CurrentImagePath = property.ImagePath
            };

            return View(model);
        }


        // =========================
        // EDIT - POST
        // Update property and optionally replace image
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PropertyEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingProperty =
                _propertyRepository.GetById(id);

            if (existingProperty == null)
            {
                return NotFound();
            }

            existingProperty.Title = model.Title;
            existingProperty.PropertyType = model.PropertyType;
            existingProperty.City = model.City;
            existingProperty.Area = model.Area;
            existingProperty.Price = model.Price;
            existingProperty.Bedrooms = model.Bedrooms;
            existingProperty.Bathrooms = model.Bathrooms;
            existingProperty.PropertySize = model.PropertySize;
            existingProperty.Status = model.Status;
            existingProperty.Description = model.Description;
            existingProperty.UpdatedAt = DateTime.Now;

            // Replace image if a new image was uploaded
            if (model.ImageFile != null &&
                model.ImageFile.Length > 0)
            {
                DeleteImage(existingProperty.ImagePath);

                existingProperty.ImagePath =
                    await SaveImage(model.ImageFile);
            }

            _propertyRepository.Update(existingProperty);
            _propertyRepository.Save();

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DELETE - GET
        // =========================
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = _propertyRepository.GetById(id.Value);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }


        // =========================
        // DELETE - POST
        // Delete property and its image
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var property =
                _propertyRepository.GetById(id);

            if (property != null)
            {
                DeleteImage(property.ImagePath);

                _propertyRepository.Delete(property);
                _propertyRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // SAVE IMAGE
        // =========================
        private async Task<string> SaveImage(
            IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "uploads",
                "properties"
            );

            Directory.CreateDirectory(uploadsFolder);

            var extension =
                Path.GetExtension(imageFile.FileName);

            var uniqueFileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                uploadsFolder,
                uniqueFileName
            );

            using (var fileStream =
                   new FileStream(
                       filePath,
                       FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/uploads/properties/{uniqueFileName}";
        }


        // =========================
        // DELETE IMAGE
        // =========================
        private void DeleteImage(
            string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return;
            }

            var relativePath =
                imagePath
                    .TrimStart('/')
                    .Replace(
                        '/',
                        Path.DirectorySeparatorChar
                    );

            var fullImagePath =
                Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    relativePath
                );

            if (System.IO.File.Exists(fullImagePath))
            {
                System.IO.File.Delete(fullImagePath);
            }
        }
    }
}