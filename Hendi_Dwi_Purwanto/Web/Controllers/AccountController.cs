using Domain;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Your email is not confirmed yet. Please check your inbox.");
                return View(model);
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked. Please try again later.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Login is not allowed for this account.");
            }
            else
            {
                ModelState.AddModelError("", "Login failed. Please try again.");
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel viewmodel)
        {
            var user = new ApplicationUser();
            user.UserName = viewmodel.UserName;
            user.Email = viewmodel.Email;
            var result = await _userManager.CreateAsync(user,viewmodel.ConfirmPassword);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Activation", new { email = user.Email });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", "Login failed. Please check your email and password.");
            }



            return View(viewmodel);
        }

        public IActionResult Activation(string email)
        {
            ViewBag.UserEmail = email;
            return View();
        }
        public async Task<IActionResult> Activate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User is not found!.");
            }

            user.EmailConfirmed = true;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("confirmation");
            }
            else
            {
                return Content("Gagal mengkonfirmasi email.");
            }


            return View();
        }
        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
