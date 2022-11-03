using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using HW.Models;
using System.Collections.Generic;

namespace HW.Repository
{
    public interface IAccountRepository
    {
        //Get user with role name
        Task<List<AppUser>> ListUser(string RoleName);
        //Update
        Task<IdentityResult> UpdateUser(AppUser user);
        //User
        Task<AppUser> GetUserById(string Id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(RegisterModel model);
        Task<string> GetRole(string email);
        Task<AppUser> GetUserByNameAsync(string Name);
        
        //Password
        Task<SignInResult> PasswordLoginAsync(LoginModel model);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task GenerateForgotPasswordTokenAsync(AppUser user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        string HashPass(AppUser user, string password);
        Task LogOutAsync();
        
        //Email
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(AppUser user);
    }
}
