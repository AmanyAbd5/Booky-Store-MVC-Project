using Booky.DAL.Models;
using Booky.PL.Helper;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booky.PL.Areas.Security.Controllers
{
    [Area("Security")]
    public class SecurityController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public SecurityController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Email = model.Email,
                    isAgree = model.isAgree
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }
        #endregion


        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var flag = await userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home", new { area = "Customer" });
                        }
                    }
                }
                TempData["ErrorMessage"] = "Email or Password is inCorrect";
            }
            return View(model);
        }
        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion



        #region Forget & ResetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPassword = Url.Action("ResetPassword", "Security", new { email = model.Email, token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = ResetPassword

                    };
                    EmailManagement.SendEmail(email);
                    return RedirectToAction("CheckInbox");

                }
            }
            return View(model);
        }


        public IActionResult CheckInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await userManager.FindByEmailAsync(email);
                var result = await userManager.ResetPasswordAsync(user, token, model.newPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        } 
        #endregion
    }
}
