using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Roles Controller:
    // Handles role management and assigning permissions to roles.
    public class RolesController : Controller
    {
        private readonly AppDbContext _db;

        public RolesController(AppDbContext db)
        {
            _db = db;
        }


        // =========================
        // INDEX
        // Display all roles
        // =========================
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();

            return View(roles);
        }


        // =========================
        // CREATE
        // Add a new role
        // =========================
        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // EDIT
        // Update an existing role
        // =========================
        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Update(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // DELETE
        // Delete a role
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = _db.Roles.Find(id);

            if (role != null)
            {
                _db.Roles.Remove(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // ASSIGN PERMISSIONS - GET
        // Display all permissions for the selected role
        // =========================
        [HttpGet]
        public IActionResult AssignPermissions(int roleId)
        {
            var role = _db.Roles.Find(roleId);

            if (role == null)
            {
                return NotFound();
            }

            // Get all permissions
            var allPermissions = _db.Permissions.ToList();

            // Get permissions already assigned to this role
            var assignedPermissionIds = _db.PermissionRoles
                .Where(pr => pr.RoleId == roleId)
                .Select(pr => pr.PermissionId)
                .ToList();

            ViewBag.Role = role;
            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissionIds = assignedPermissionIds;

            return View();
        }


        // =========================
        // ASSIGN PERMISSIONS - POST
        // Save selected permissions for the role
        // =========================
        [HttpPost]
        public IActionResult AssignPermissions(
            int roleId,
            List<int> permissionIds)
        {
            var role = _db.Roles.Find(roleId);

            if (role == null)
            {
                return NotFound();
            }

            // Get old permissions for this role
            var oldPermissions = _db.PermissionRoles
                .Where(pr => pr.RoleId == roleId)
                .ToList();

            // Remove old permissions
            _db.PermissionRoles.RemoveRange(oldPermissions);

            // Add selected permissions
            foreach (var permissionId in permissionIds)
            {
                var permissionRole = new PermissionRole
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };

                _db.PermissionRoles.Add(permissionRole);
            }

            // Save changes
            _db.SaveChanges();

            return RedirectToAction(
                "AssignPermissions",
                new { roleId = roleId });
        }
    }
}