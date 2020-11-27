using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services.AccountService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ILocalStorageService _localStorage;


        public CurrentUserService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetUserId()
        {
            return await _localStorage.GetItemAsync<string>("DE37C30124A0406649E48C67851628B5ADA4A3B0");
        }

        public async Task<string> GetAuthToken()
        {
            return await _localStorage.GetItemAsync<string>("e049a201aa7b18acb7e9aca648e632a86da67763");
        }

        public async Task<string> GetAuthRefreshToken()
        {
            return await _localStorage.GetItemAsync<string>("3519ff7c3d315cd950bf0973140f7513ebf489d9");
        }
        public async Task<bool> GetNewUser()
        {
            return await _localStorage.GetItemAsync<bool>("D045A8E8FEEBBF4DE8B92D49468DB5544BB430FC");
        }

        public async Task<string> GetUsername()
        {
            return await _localStorage.GetItemAsync<string>("249BA36000029BBE97499C03DB5A9001F6B734EC");
        }

        public async Task<string> GetEmail()
        {
            return await _localStorage.GetItemAsync<string>("A88B7DCD1A9E3E17770BBAA6D7515B31A2D7E85D");
        }

        public async Task SetAuthToken(string token)
        {
            await _localStorage.SetItemAsync("e049a201aa7b18acb7e9aca648e632a86da67763", token);
        }

        public async Task SetAuthRefreshToken(string refreshToken)
        {
            await _localStorage.SetItemAsync("3519ff7c3d315cd950bf0973140f7513ebf489d9", refreshToken);
        }

        public async Task SetUsername(string username)
        {
            await _localStorage.SetItemAsync("249BA36000029BBE97499C03DB5A9001F6B734EC", username);
        }

        public async Task SetUserId(string userId)
        {
            await _localStorage.SetItemAsync("DE37C30124A0406649E48C67851628B5ADA4A3B0", userId);
        }

        public async Task SetEmail(string userId)
        {
            await _localStorage.SetItemAsync("A88B7DCD1A9E3E17770BBAA6D7515B31A2D7E85D", userId);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("249BA36000029BBE97499C03DB5A9001F6B734EC");
            await _localStorage.RemoveItemAsync("e049a201aa7b18acb7e9aca648e632a86da67763");
            await _localStorage.RemoveItemAsync("3519ff7c3d315cd950bf0973140f7513ebf489d9");
            await _localStorage.RemoveItemAsync("DE37C30124A0406649E48C67851628B5ADA4A3B0");
            await _localStorage.RemoveItemAsync("A88B7DCD1A9E3E17770BBAA6D7515B31A2D7E85D");
        }

        public async Task SetNewUser(bool value)
        {
            await _localStorage.SetItemAsync("D045A8E8FEEBBF4DE8B92D49468DB5544BB430FC", value);
        }

        public async Task RemoveNewUser()
        {
            await _localStorage.RemoveItemAsync("D045A8E8FEEBBF4DE8B92D49468DB5544BB430FC");
        }

    }
}