using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Permissions Controller:
    // Handles permission management in the system.
    public class PermissionsController : Controller
    {
        private readonly AppDbContext _db;

        // Receive database context
        public PermissionsController(AppDbContext db)
        {
            _db = db;
        }


        // =========================
        // INDEX
        // Display all permissions
        // =========================
        public IActionResult Index()
        {
            var permissions = _db.Permissions.ToList();

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
                _db.Permissions.Add(permission);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
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
                _db.Permissions.Update(permission);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // DELETE
        // Delete a permission
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var permission = _db.Permissions.Find(id);

            if (permission != null)
            {
                _db.Permissions.Remove(permission);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}