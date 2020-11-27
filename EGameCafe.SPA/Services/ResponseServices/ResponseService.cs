using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services.AccountService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services.ResponseServices
{
    public class ResponseService : IResponseService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IAccountService _accountService;

        public ResponseService(NavigationManager navigationManager, IAccountService accountService)
        {
            _navigationManager = navigationManager;
            _accountService = accountService;
        }

        public async Task<NotificationModel> ResponseResultChecker(Result result, string pageUri, string succeedMessage)
        {
            var notificationModel = new NotificationModel();

            if (!result.Succeeded)
            {
                if (result.Status == 401)
                {
                    await UnAuthorizedResult(pageUri);
                }

                notificationModel.Message = "* " + result.EnError;
                notificationModel.Type = "alert-danger";
            }
            else
            {
                notificationModel.Message = succeedMessage;
                notificationModel.Type = "alert-success";
            }

            notificationModel.IsShow = true;

            return notificationModel;
        }

        private async Task UnAuthorizedResult(string pageRoute)
        {
            var refreshResult = await _accountService.RefreshTokenAsync();

            if (!refreshResult.Succeeded)
            {
                await _accountService.Logout();
                _navigationManager.NavigateTo("/Login", true);
            }
            else
            {
                _navigationManager.NavigateTo(pageRoute, true);
            }
        }
    }
}
