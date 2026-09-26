using AqarFlow.Models;
using AqarFlow.Repositories;
using AqarFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Only Admin users can access Users management
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleUserRepository _roleUserRepository;

        public UsersController(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRoleUserRepository roleUserRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleUserRepository = roleUserRepository;
        }


        // =========================
        // INDEX
        // =========================
        public IActionResult Index()
        {
            var users = _userRepository.GetAll();

            return View(users);
        }


        // =========================
        // CREATE - GET
        // =========================
        public IActionResult Create()
        {
            ViewBag.Roles = _roleRepository.GetAll();

            return View();
        }


        // =========================
        // CREATE - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now
                };

                // Hash password before saving
                var passwordHasher =
                    new PasswordHasher<User>();

                user.PasswordHash =
                    passwordHasher.HashPassword(
                        user,
                        model.Password
                    );

                _userRepository.Add(user);
                _userRepository.Save();

                // Assign selected role
                if (model.RoleId.HasValue)
                {
                    var roleUser = new RoleUser
                    {
                        UserId = user.Id,
                        RoleId = model.RoleId.Value
                    };

                    _roleUserRepository.Add(roleUser);
                    _roleUserRepository.Save();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = _roleRepository.GetAll();

            return View(model);
        }


        // =========================
        // EDIT - GET
        // =========================
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = _roleRepository.GetAll();

            ViewBag.SelectedRoleId =
                user.RoleUsers
                    .FirstOrDefault()?.RoleId;

            return View(user);
        }


        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            User user,
            int? roleId)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            var existingUser =
                _userRepository.GetById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Update basic information
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.IsActive = user.IsActive;

            // Remove old roles
            _roleUserRepository.DeleteByUserId(
                existingUser.Id
            );

            // Assign selected role
            if (roleId.HasValue)
            {
                var roleUser = new RoleUser
                {
                    UserId = existingUser.Id,
                    RoleId = roleId.Value
                };

                _roleUserRepository.Add(roleUser);
            }

            _userRepository.Update(existingUser);

            _userRepository.Save();
            _roleUserRepository.Save();

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DELETE - GET
        // =========================
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user =
                _userRepository.GetById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // =========================
        // DELETE - POST
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user =
                _userRepository.GetById(id);

            if (user != null)
            {
                // Remove user roles first
                _roleUserRepository.DeleteByUserId(
                    user.Id
                );

                _roleUserRepository.Save();

                // Remove user
                _userRepository.Delete(user);
                _userRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}