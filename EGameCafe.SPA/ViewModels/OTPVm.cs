using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class OTPVm : IOTPVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAccountService _accountService;
        private readonly NavigationManager _navigationManager;
        private readonly IResponseService _responseService;

        public OTPVm(IAccountService accountService, NavigationManager navigationManager, IResponseService responseService)
        {
            _accountService = accountService;
            _navigationManager = navigationManager;
            _responseService = responseService;

            notification = new NotificationModel();
            PageUri = $"/otp/{type}";
        }

        public string PageUri { get; set; }
      

        private int otpNumber;
        public int OTPNumber
        {
            get => otpNumber;
            set
            {
                otpNumber = value;
                OnPropertyChanged();
            }
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

        private string type;
        public string Type
        {
            get => type;
            set
            {
                if(value is null)
                {
                    _navigationManager.NavigateTo("/login");
                    return;
                }

                type = value;
                OnPropertyChanged();
            }
        }

        public async Task HandleOTP(int number)
        {
           
            if (Type == "accountconfirmation")
            {
                await SendConfirmationAndNavigate(number);
            }
            else if (Type == "forgotpassword")
            {
                await ForgotPasswordAndNavigate(number);
            }

        }

        protected async Task SendConfirmationAndNavigate(int number)
        {
            var result = await _accountService.AccountConfirmedOTP(number);

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "عملیات با موفقیت انجام شد");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo("/", true);
            }
        }

        protected async Task ForgotPasswordAndNavigate(int number)
        {
            var result = await _accountService.ForgotPasswordOTPConfirmation(number);

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "عملیات با موفقیت انجام شد");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo("/resetpassword", true);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
