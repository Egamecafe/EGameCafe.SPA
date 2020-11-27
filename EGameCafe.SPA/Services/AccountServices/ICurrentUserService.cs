using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services.AccountService
{
    public interface ICurrentUserService
    {
        Task<string> GetUserId();
        Task<string> GetAuthRefreshToken();
        Task<string> GetAuthToken();
        Task<bool> GetNewUser();
        Task<string> GetUsername();
        Task<string> GetEmail();

        Task SetAuthToken(string token);
        Task SetAuthRefreshToken(string refreshToken);
        Task SetNewUser(bool value);
        Task SetUsername(string username);
        Task SetUserId(string userId);
        Task SetEmail(string userId);
        Task RemoveNewUser();
        Task Logout();
    }
}
