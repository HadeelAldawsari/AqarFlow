using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Roles Controller:
    // Handles role management and assigning permissions to roles.

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionRoleRepository _permissionRoleRepository;

        public RolesController(
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IPermissionRoleRepository permissionRoleRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _permissionRoleRepository = permissionRoleRepository;
        }


        // =========================
        // INDEX
        // Display all roles
        // =========================
        public IActionResult Index()
        {
            var roles = _roleRepository.GetAll();

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
                _roleRepository.Add(role);
                _roleRepository.Save();
            }

            return RedirectToAction(nameof(Index));
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
                _roleRepository.Update(role);
                _roleRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DELETE
        // Delete a role
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = _roleRepository.GetById(id);

            if (role != null)
            {
                _roleRepository.Delete(role);
                _roleRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // ASSIGN PERMISSIONS - GET
        // Display permissions for selected role
        // =========================
        [HttpGet]
        public IActionResult AssignPermissions(int roleId)
        {
            var role = _roleRepository.GetById(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var allPermissions =
                _permissionRepository.GetAll();

            var assignedPermissionIds =
                _permissionRoleRepository
                    .GetPermissionIdsByRoleId(roleId);

            ViewBag.Role = role;
            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissionIds = assignedPermissionIds;

            return View();
        }


        // =========================
        // ASSIGN PERMISSIONS - POST
        // Save selected permissions
        // =========================
        [HttpPost]
        public IActionResult AssignPermissions(
            int roleId,
            List<int> permissionIds)
        {
            var role = _roleRepository.GetById(roleId);

            if (role == null)
            {
                return NotFound();
            }

            // Remove old permissions
            _permissionRoleRepository
                .DeleteByRoleId(roleId);

            // Add selected permissions
            foreach (var permissionId in permissionIds)
            {
                var permissionRole =
                    new PermissionRole
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    };

                _permissionRoleRepository.Add(
                    permissionRole
                );
            }

            _permissionRoleRepository.Save();

            return RedirectToAction(
                nameof(AssignPermissions),
                new { roleId });
        }
    }
}