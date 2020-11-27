using EGameCafe.SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services.AccountService
{
    public interface IAccountService
    {
        Task<Result> CreateUserAsync(RegisterModel registerModel);
        Task<Result> Login(LoginModel loginModel);
        Task<AuthenticationResult> RefreshTokenAsync();
        Task<Result> SendOTPAgain(SendOTPAgainModel model);
        Task<Result> AccountConfirmedOTP(int randomNumber);
        Task<Result> ForgotPassword(ForgotPasswordModel model);
        Task<Result> ForgotPasswordOTPConfirmation(int randomNumber);
        Task<Result> ResetPassword(string password);
        Task Logout();
    }
}
