using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Permissions Controller:
    // Handles permission management in the system.

    [Authorize(Roles = "Admin")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController(
            IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }


        // =========================
        // INDEX
        // Display all permissions
        // =========================
        public IActionResult Index()
        {
            var permissions =
                _permissionRepository.GetAll();

            return View(permissions);
        }


        // =========================
        // CREATE
        // Add a new permission
        // =========================
        [HttpPost]
        public IActionResult Create(Permission permission)
        {
            if (ModelState.IsValid)
            {
                _permissionRepository.Add(permission);
                _permissionRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // EDIT
        // Update an existing permission
        // =========================
        [HttpPost]
        public IActionResult Edit(Permission permission)
        {
            if (ModelState.IsValid)
            {
                _permissionRepository.Update(permission);
                _permissionRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DELETE
        // Delete a permission
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var permission =
                _permissionRepository.GetById(id);

            if (permission != null)
            {
                _permissionRepository.Delete(permission);
                _permissionRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}