using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class ResetPasswordVm : IResetPasswordVm
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAccountService _accountService;
        private readonly NavigationManager _navigationManager;
        private readonly IResponseService _responseService;

        public ResetPasswordVm(IAccountService accountService, NavigationManager navigationManager, IResponseService responseService)
        {
            _accountService = accountService;
            _navigationManager = navigationManager;
            _responseService = responseService;

            notification = new NotificationModel();
        }

        private NotificationModel notification;
        public NotificationModel Notification
        {
            get => notification;
            set
            {
                notification = value;
                OnPropertyChanged();
            }
        }

        public string PageUri { set; get; } = "/resetpassword";

        private string password;
        public string Password 
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public async Task HandleResetPassword(string password)
        {
            var result = await _accountService.ResetPassword(password);

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "لاگین با موفقیت انجام شد");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo("/login", true);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
