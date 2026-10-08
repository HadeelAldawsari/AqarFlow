
using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // =====================================
    // PERMISSIONS CONTROLLER
    // =====================================

    // Only Admin users can manage permissions
    [Authorize(Roles = "Admin")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController(
            IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        // =====================================
        // INDEX
        // Display all permissions
        // =====================================

        [HttpGet]
        public IActionResult Index()
        {
            var permissions = _permissionRepository.GetAll();

            return View(permissions);
        }

        // =====================================
        // CREATE - POST
        // Add a new permission
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Permission permission)
        {
            if (ModelState.IsValid)
            {
                _permissionRepository.Add(permission);
                _permissionRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // EDIT - POST
        // Update an existing permission
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Permission permission)
        {
            if (ModelState.IsValid)
            {
                _permissionRepository.Update(permission);
                _permissionRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // DELETE - POST
        // Delete an existing permission
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var permission = _permissionRepository.GetById(id);

            if (permission == null)
            {
                return NotFound();
            }

            _permissionRepository.Delete(permission);
            _permissionRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
