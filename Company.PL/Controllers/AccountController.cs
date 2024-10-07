using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly SignInManager<ApplicationUser> _signinmanger;
        private readonly IMailService _mailservice;

        public AccountController(UserManager<ApplicationUser> usermanger,
            SignInManager<ApplicationUser> signinmanger,
            IMailService mailservice)
        {
            _usermanger = usermanger;
            _signinmanger = signinmanger;
            _mailservice = mailservice;
        }
        public IActionResult Register()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,

                };
                var result = await _usermanger.CreateAsync(User, model.Password);
                if (result.Succeeded)

                    return RedirectToAction(nameof(Login));

                else

                    foreach (var error in result.Errors)

                        ModelState.AddModelError(string.Empty, error.Description);



            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _usermanger.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _usermanger.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signinmanger.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not exist");


                }
            }
            return View(model);

        }

        public new async Task<IActionResult> SignOut()
        {
            await _signinmanger.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usermanger.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token = await _usermanger.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = user.Email, Token = token });
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = "Reset Password"
                    };
                    // EmailSettings.SendEmail(email);
                    _mailservice.SendEmail(email);

                    return RedirectToAction(nameof(CheckYourInbox));

                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not exis");

                }
            }


            return View("ForgetPassword", model);





        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }


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
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;

                var User = await _usermanger.FindByEmailAsync(email);
                var result = await _usermanger.ResetPasswordAsync(User, token, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                else

                    foreach (var error in result.Errors)

                        ModelState.AddModelError(string.Empty, error.Description);

            }  
                return View(model);
            }


        }
    }


