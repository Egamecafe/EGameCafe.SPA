using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class LoginVm : ILoginVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAccountService _accountService;
        private readonly NavigationManager _navigationManager;
        private readonly IResponseService _responseService;

        public LoginVm(IAccountService accountService, NavigationManager navigationManager, IResponseService responseService)
        {
            _accountService = accountService;
            _navigationManager = navigationManager;
            _responseService = responseService;

            item = new LoginModel();
            notification = new NotificationModel();
        }

        private LoginModel item;
        public LoginModel Item {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        private NotificationModel notification;
        public NotificationModel Notification {
            get => notification;
            set
            {
                notification = value;
                OnPropertyChanged();
            }
        }
        public string PageUri { set; get; } = "/login";

        public async Task HandleLogin(LoginModel model)
        {
            var result = await _accountService.Login(model);

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "لاگین با موفقیت انجام شد");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo("/", true);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
