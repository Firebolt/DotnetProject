using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "User", Value = "Passenger" },
                new SelectListItem { Text = "Clerk", Value = "TicketAgent"},
                new SelectListItem { Text = "Admin", Value = "Administrator" }
            };

            roles.Insert(0, new SelectListItem { Text = "Select a role", Value = "" });

            var model = new UserRequest
            {
                Username = "",
                Email = "",
                Password = "",
                ConfirmPassword = "",
                UserRoleList = roles
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRequest model)
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "User", Value = "Passenger" },
                new SelectListItem { Text = "Clerk", Value = "TicketAgent"},
                new SelectListItem { Text = "Admin", Value = "Administrator" }
            };

            roles.Insert(0, new SelectListItem { Text = "Select a role", Value = "" });

            model.UserRoleList = roles;

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UserRole = model.UserRole
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(model.UserRole.ToString());
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.UserRole.ToString()));
                    }

                    await _userManager.AddToRoleAsync(user, model.UserRole.ToString());

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest model, [FromQuery] string? ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        var roles = await _userManager.GetRolesAsync(user);
                        var claims = User.Claims;

                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}