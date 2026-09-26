using AqarFlow.Models;
using AqarFlow.Repositories;
using AqarFlow.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AqarFlow.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountsController(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // =========================
        // LOGIN - GET
        // Display login page
        // =========================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // =========================
        // LOGIN - POST
        // Process login request
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // Find user by email
            var user =
                _userRepository.GetByEmail(model.Email);


            // Check if the user exists
            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View(model);
            }


            // Check if the account is active
            if (!user.IsActive)
            {
                ModelState.AddModelError(
                    "",
                    "This account is inactive."
                );

                return View(model);
            }


            // Verify the entered password
            var passwordHasher =
                new PasswordHasher<User>();

            var result =
                passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    model.Password
                );


            if (result ==
                PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View(model);
            }


            // Get the user's assigned role
            var roleName =
                user.RoleUsers
                    .FirstOrDefault()?
                    .Role?
                    .Name ?? "User";


            // Create user claims
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.FullName
                ),

                new Claim(
                    ClaimTypes.Email,
                    user.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    roleName
                )
            };


            // Create the user's identity
            var claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults
                        .AuthenticationScheme
                );


            // Configure authentication properties
            var authenticationProperties =
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };


            // Create the authentication cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults
                    .AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authenticationProperties
            );


            // Redirect to dashboard
            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // =========================
        // LOGOUT
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults
                    .AuthenticationScheme
            );

            return RedirectToAction(nameof(Login));
        }
    }
}