using HW.Models;
using HW.Services;
using HW.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accRepo;
        private readonly IUserService _userService;

        public AccountController(IAccountRepository accRepo,
                                 IUserService userService)
        {
            _accRepo = accRepo;
            _userService = userService;
        }

        //ĐĂNG KÝ
        public IActionResult Register() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accRepo.CreateUserAsync(model);

                if(!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = model.Email });
            }
            return View(model);
        }

        //ĐĂNG NHẬP
        public IActionResult Login(string returnUrl)
        {
            LoginModel login = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accRepo.PasswordLoginAsync(model);
                if(result.Succeeded)
                {
                    //var role = await _accRepo.GetRole(model.Email);
                    //if (role == "teacher") return Redirect(model.ReturnUrl ?? "area/teacher/home");
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction("ConfirmEmail", new { email = model.Email});
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa. Hãy thử lại sau.");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin không hợp lệ");
                }
            }
            ModelState.AddModelError("", "Kiểm tra lại tài khoản và mật khẩu");
            return View(model);
        }

        //ĐĂNG XUẤT
        public async Task<IActionResult> Logout()
        {
            await _accRepo.LogOutAsync();
            return Redirect("/");
        }

        //XEM THÔNG TIN 
        public class PieData
        {
            public string xValue;
            public double yValue;
            public string text;
        }
        public async Task<IActionResult> Detail()
        {
            UserInfo userInfo = new UserInfo(await _accRepo.GetUserById(_userService.GetUserId()));

            PieViewModel dataPie = new PieViewModel();
            List<PieData> chartData = new List<PieData>
            {
                new PieData { xValue =  "Accepted", yValue = 10, text = "33%" },
                new PieData { xValue =  "Wrong Answer", yValue = 10, text = "33%" },
                new PieData { xValue =  "Mistake", yValue = 10, text = "33%" },
            };
            ViewBag.dataSource = chartData;

            return View(userInfo);
        }

        //CẬP NHẬT THÔNG TIN
        public async Task<IActionResult> Edit()
        {
            UserInfo userInfo = new UserInfo(await _accRepo.GetUserById(_userService.GetUserId()));
            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserInfo user)
        {
            var appUser = await _accRepo.GetUserById(_userService.GetUserId());
            if (ModelState.IsValid)
            {
                appUser.Email = user.Email;
                appUser.UserName = user.UserName;
                appUser.FirstName = user.FirstName;
                appUser.LastName = user.LastName;
                if (user.AvartarImg != null) appUser.AvartarImg = user.AvartarImg;
                if (user.Password != null)
                {
                    appUser.PasswordHash = _accRepo.HashPass(appUser, user.Password);
                }

                var result = await _accRepo.UpdateUser(appUser);
                if (result.Succeeded)
                    RedirectToAction("Detail");
            }
            return View(user);
        }

        //THAY ĐỔI MẬT KHẨU
        public IActionResult ChangePassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accRepo.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    //return Json(new { success = true, message = "Cập nhật thành công!"});
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        
        //QUÊN MẬT KHẨU
        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // code here
                var user = await _accRepo.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accRepo.GenerateForgotPasswordTokenAsync(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                Token = token,
                UserId = uid
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accRepo.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        
        //XÁC THỰC EMAIL

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirm model = new EmailConfirm
            {
                Email = email
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accRepo.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }

            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirm model)
        {
            var user = await _accRepo.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;

                    return View(model);
                }

                await _accRepo.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong.");
            }
            return View(model);
        }
    }
}
