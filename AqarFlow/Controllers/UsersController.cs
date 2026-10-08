
using AqarFlow.Models;
using AqarFlow.Repositories;
using AqarFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
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

        // INDEX
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        // CREATE - GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _roleRepository.GetAll();
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateViewModel model)
        {
            if (model.RoleId.HasValue &&
                _roleRepository.GetById(model.RoleId.Value) == null)
            {
                ModelState.AddModelError(
                    nameof(model.RoleId),
                    "Selected role does not exist.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleRepository.GetAll();
                return View(model);
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                IsActive = model.IsActive,
                CreatedAt = DateTime.Now
            };

            var passwordHasher = new PasswordHasher<User>();

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                model.Password);

            _userRepository.Add(user);
            _userRepository.Save();

            if (model.RoleId.HasValue)
            {
                _roleUserRepository.Add(new RoleUser
                {
                    UserId = user.Id,
                    RoleId = model.RoleId.Value
                });

                _roleUserRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        // EDIT - GET
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var user = _userRepository.GetById(id.Value);

            if (user == null)
                return NotFound();

            var model = new UserEditViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                RoleId = user.RoleUsers.FirstOrDefault()?.RoleId
            };

            ViewBag.Roles = _roleRepository.GetAll();

            return View(model);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            UserEditViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            var existingUser = _userRepository.GetById(id);

            if (existingUser == null)
                return NotFound();

            if (model.RoleId.HasValue &&
                _roleRepository.GetById(model.RoleId.Value) == null)
            {
                ModelState.AddModelError(
                    nameof(model.RoleId),
                    "Selected role does not exist.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleRepository.GetAll();
                return View(model);
            }

            // Update only editable fields.
            // PasswordHash remains unchanged.
            existingUser.FullName = model.FullName;
            existingUser.Email = model.Email;
            existingUser.IsActive = model.IsActive;

            var currentRoleId = existingUser.RoleUsers
                .FirstOrDefault()?.RoleId;

            if (currentRoleId != model.RoleId)
            {
                _roleUserRepository.DeleteByUserId(existingUser.Id);

                if (model.RoleId.HasValue)
                {
                    _roleUserRepository.Add(new RoleUser
                    {
                        UserId = existingUser.Id,
                        RoleId = model.RoleId.Value
                    });
                }
            }

            _userRepository.Update(existingUser);
            _userRepository.Save();
            _roleUserRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // DELETE - GET
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var user = _userRepository.GetById(id.Value);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            _roleUserRepository.DeleteByUserId(user.Id);
            _roleUserRepository.Save();

            _userRepository.Delete(user);
            _userRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
