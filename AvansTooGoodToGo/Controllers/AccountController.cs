using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Security.Claims;

namespace Portal {

    public class AccountController : Controller {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr) {
            userManager = userMgr;
            signInManager = signInMgr;

            IdentitySeedData.EnsurePopulated(userMgr).Wait();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            return View(new LoginModel {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel) {
            if (ModelState.IsValid) {
                var user =
                    await userManager.FindByNameAsync(loginModel.Name);
                if (user != null) {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded) {
                        return Redirect(loginModel?.ReturnUrl ?? "/Package/Index");
                    } else {
                        ModelState.AddModelError("", "Ongeldig gebruikersnaam of wachtwoord");
                    }
                } else {
                    ModelState.AddModelError("", "Ongeldig gebruikersnaam of wachtwoord");
                }
            }

            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/") {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult Register(string returnUrl) {
            return View(new UserRegisterModel {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel userModel) {
            if (!ModelState.IsValid) {
                return View(userModel);
            }

            IdentityUser student = await userManager.FindByIdAsync(userModel.Name);

            if (student == null) {
                student = new IdentityUser("Student");
                var result = await userManager.CreateAsync(student, userModel.Password);
                await userManager.AddClaimAsync(student, new Claim("Student", "true"));

                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public IActionResult AccessDenied(string returnUrl) {
            return View();
        }
    }
}