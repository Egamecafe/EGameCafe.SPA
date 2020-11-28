using EGameCafe.SPA.Data;
using EGameCafe.SPA.Extentions;
using EGameCafe.SPA.Models;
using EGameCafe.SPA.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ICurrentUserService _currentUser;

        public AccountService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ICurrentUserService currentUser)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            _currentUser = currentUser;
        }

        public async Task<Result> CreateUserAsync(RegisterModel registerModel)
        {
            try
            {
                var registerAsJson = JsonConvert.SerializeObject(registerModel);

                var response = await httpClient.PostAsync("api/v1/OAuth/Register",
                    new StringContent(registerAsJson, Encoding.UTF8, "application/json"));

                var result = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

                if (result.Succeeded)
                {
                    await _currentUser.SetEmail(registerModel.Email);
                }

                return result;
            }
            catch (Exception ex)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task<Result> Login(LoginModel loginModel)
        {
            try
            {
                var loginAsJson = JsonConvert.SerializeObject(loginModel);

                var response = await httpClient.PostAsync("api/v1/OAuth/Login",
                    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

                var loginResult = JsonConvert.DeserializeObject<AuthenticationResult>(await response.Content.ReadAsStringAsync());

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure(loginResult.EnError, loginResult.FaError);
                }

                await SetAuthentication(loginResult.Token, loginResult.RefreshToken);

                return Result.Success();
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task<Result> RefreshTokenAsync()
        {
            try
            {
                var refreshToken = new RefreshTokenModel()
                {
                    RefreshToken = await _currentUser.GetAuthRefreshToken(),
                    Token = await _currentUser.GetAuthToken()
                };
                var loginAsJson = JsonConvert.SerializeObject(refreshToken);

                var response = await httpClient.PostAsync("api/v1/OAuth/RefreshToken",
                    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

                var result = JsonConvert.DeserializeObject<AuthenticationResult>(await response.Content.ReadAsStringAsync());

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure(result.EnError, result.FaError);
                }

                await SetAuthentication(result.Token, result.RefreshToken);

                return Result.Success();
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task<Result> SendOTPAgain(SendOTPAgainModel model)
        {
            var usernameAsJson = JsonConvert.SerializeObject(model);

            var response = await httpClient.PostAsync("api/v1/OAuth/SendAccountConfirmationTokenAgain",
                new StringContent(usernameAsJson, Encoding.UTF8, "application/json"));

            return await response.DeserializeResponseMessageStatus();
        }

        public async Task<Result> AccountConfirmedOTP(int randomNumber)
        {
            try
            {
                var email = await _currentUser.GetEmail();

                OTPConfirmationModel otpModel = new()
                {
                    RandomNumber = randomNumber,
                    Email = email
                };

                var loginAsJson = JsonConvert.SerializeObject(otpModel);

                var response = await httpClient.PostAsync("api/v1/OAuth/AccountOTPConfirmation",
                    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

                var loginResult = JsonConvert.DeserializeObject<AuthenticationResult>(await response.Content.ReadAsStringAsync());

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure(loginResult.EnError, loginResult.FaError);
                }

                await SetAuthentication(loginResult.Token, loginResult.RefreshToken);

                return Result.Success();
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task<Result> ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                var usernameAsJson = JsonConvert.SerializeObject(model);

                var response = await httpClient.PostAsync("api/v1/OAuth/ForgotPassword",
                    new StringContent(usernameAsJson, Encoding.UTF8, "application/json"));

                var result = await response.DeserializeResponseMessageStatus(); ;

                if (result.Succeeded) await _currentUser.SetEmail(model.Email);

                return result;
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task<Result> ForgotPasswordOTPConfirmation(int randomNumber)
        {
            try
            {
                var email = await _currentUser.GetEmail();

                OTPConfirmationModel model = new()
                {
                    RandomNumber = randomNumber,
                    Email = email
                };

                var usernameAsJson = JsonConvert.SerializeObject(model);

                var response = await httpClient.PostAsync("api/v1/OAuth/ForgotPasswordOTPConfirmation",
                    new StringContent(usernameAsJson, Encoding.UTF8, "application/json"));

                await _currentUser.SetEmail(model.Email);

                return await response.DeserializeResponseMessageStatus();
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task<Result> ResetPassword(string password)
        {
            try
            {
                var email = await _currentUser.GetEmail();

                ResetPasswordModel model = new() { Email = email, Password = password };

                var usernameAsJson = JsonConvert.SerializeObject(model);

                var response = await httpClient.PostAsync("api/v1/OAuth/ResetPassword",
                    new StringContent(usernameAsJson, Encoding.UTF8, "application/json"));

                return await response.DeserializeResponseMessageStatus();
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }

        public async Task Logout()
        {
            await _currentUser.Logout();
            ((CustomAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private async Task SetAuthentication(string token, string refreshToken)
        {
            await _currentUser.SetAuthToken(token);
            await _currentUser.SetAuthRefreshToken(refreshToken);

            ((CustomAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(token);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
