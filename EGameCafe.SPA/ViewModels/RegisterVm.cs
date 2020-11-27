using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class RegisterVm : IRegisterVm , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAccountService _accountService;
        private readonly NavigationManager _navigationManager;
        private readonly IResponseService _responseService;

        public RegisterVm(IAccountService accountService, NavigationManager navigationManager, IResponseService responseService)
        {
            _accountService = accountService;
            _navigationManager = navigationManager;
            _responseService = responseService;

            item = new RegisterModel();
            notification = new NotificationModel();
        }


        private RegisterModel item;
        public RegisterModel Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }
        public string PageUri { set; get; } = "/register";

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

        private string fullname;
        public string Fullname {
            get => fullname;
            set
            {
                fullname = value;

                var name = fullname.Split(' ');

                item.Firstname = name[0] ??= "";
                item.Lastname = name[1] ??= "";

                OnPropertyChanged();
            }
        }

        public async Task HandleRegister(RegisterModel model)
        {
            var result = await _accountService.CreateUserAsync(model);

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "لاگین با موفقیت انجام شد");

            if (result.Status != 200)
            {
                notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo("/OTP/accountconfirmation", true);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
