
using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
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

        // =====================================
        // INDEX
        // =====================================

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleRepository.GetAll();

            return View(roles);
        }

        // =====================================
        // CREATE - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Add(role);
                _roleRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // EDIT - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var existingRole = _roleRepository.GetById(role.Id);

                if (existingRole == null)
                    return NotFound();

                existingRole.Name = role.Name;

                _roleRepository.Update(existingRole);
                _roleRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // DELETE - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var role = _roleRepository.GetById(id);

            if (role == null)
                return NotFound();

            _roleRepository.Delete(role);
            _roleRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // ASSIGN PERMISSIONS - GET
        // =====================================

        [HttpGet]
        public IActionResult AssignPermissions(int roleId)
        {
            var role = _roleRepository.GetById(roleId);

            if (role == null)
                return NotFound();

            var allPermissions = _permissionRepository.GetAll();

            var assignedPermissionIds = _permissionRoleRepository
                .GetPermissionIdsByRoleId(roleId);

            ViewBag.Role = role;
            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissionIds = assignedPermissionIds;

            return View();
        }

        // =====================================
        // ASSIGN PERMISSIONS - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignPermissions(
            int roleId,
            List<int> permissionIds)
        {
            var role = _roleRepository.GetById(roleId);

            if (role == null)
                return NotFound();

            _permissionRoleRepository.DeleteByRoleId(roleId);

            foreach (var permissionId in permissionIds ?? new List<int>())
            {
                var permissionRole = new PermissionRole
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };

                _permissionRoleRepository.Add(permissionRole);
            }

            _permissionRoleRepository.Save();

            return RedirectToAction(
                nameof(AssignPermissions),
                new { roleId });
        }
    }
}
