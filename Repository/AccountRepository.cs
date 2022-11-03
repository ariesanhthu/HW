using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.Models;
using HW.Services;

namespace HW.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountRepository(UserManager<AppUser> userManager, 
                                SignInManager<AppUser> signInManager,
                                 IUserService userService,
                                 IEmailService emailService,
                                 IConfiguration configuration,
                                  RoleManager<IdentityRole> roleManager,
                                  IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }
        //GET ALL USER WITH ROLE
        public async Task<List<AppUser>> ListUser(string RoleName)
        {

            List<AppUser> students = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, RoleName))
                {
                    students.Add(user);
                }
            }

            return students;
        }
        //Update
        public async Task<IdentityResult> UpdateUser(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
        //GET USER
        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<AppUser> GetUserById(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }
        
        public async Task<AppUser> GetUserByNameAsync(string Name)
        {
            return await _userManager.FindByNameAsync(Name);
        }
        public async Task<string> GetRole(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return roles[0] ?? string.Empty;
        }
        public async Task<IdentityResult> CreateUserAsync(RegisterModel model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (!string.IsNullOrEmpty(token))
                {
                    await _userManager.AddToRoleAsync(user, "Student");
                    await SendEmailConfirmationEmail(user, token);
                }
            }
            return result;
        }
        //Password
        public async Task<SignInResult> PasswordLoginAsync(LoginModel model)
        {
            var appUser = await GetUserByEmailAsync(model.Email);
            return await _signInManager.PasswordSignInAsync(appUser, model.Password, false, false);
        }
        //generate token
        public async Task GenerateForgotPasswordTokenAsync(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }
        //Change password
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
        //reset
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }
        //hash password
        public string HashPass(AppUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
        //send mail
        private async Task SendForgotPasswordEmail(AppUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForForgotPassword(options);
        }
        //logout
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //Email
        private async Task SendEmailConfirmationEmail(AppUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

    }
}
