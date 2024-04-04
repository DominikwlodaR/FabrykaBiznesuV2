using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FabrykaBiznesuV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _contex;

        public AccountController(UserManager<AppUser> user, SignInManager<AppUser> signInManager, AppDbContext  contex)
        {
            _contex = contex;   
            _signInManager = signInManager;
            _userManager = user;    
        }
        public IActionResult Login()
        {
            if (@User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var response = new LoginViewModel();
                return View(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if(user != null) 
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Błędne dane logowania. Spróbuj ponowanie.";
                return View(loginViewModel);
            }
            TempData["Error"] = "Błędne dane logowania. Spróbuj ponowanie.";
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
