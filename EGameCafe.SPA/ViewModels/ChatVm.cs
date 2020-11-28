using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
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
    public class ChatVm : IChatVm
    {
        private readonly IResponseService _responseService;
        private readonly IRepository _repository;

        public ChatVm(IResponseService responseService, IRepository repository)
        {
            _responseService = responseService;
            _repository = repository;

            notification = new NotificationModel();
            item = new GetAllGroups();
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
        public string PageUri { set; get; } = "/groupchat";

        public GetAllGroups item;
        public GetAllGroups Item 
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        public string currentGroupId;
        public string CurrentGroupId 
        {
            get => currentGroupId;
            set
            {
                currentGroupId = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task HandleGetGroups()
        {
            var groupsResult = await _repository.AuthorizeGetAsync<GetAllGroups>("api/v1/Group/GetAllGroups/0/100/groupname");

            var notifResult = await _responseService.ResponseResultChecker(groupsResult.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (groupsResult.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                Item = groupsResult.ResultVm;
            }

            OnPropertyChanged(nameof(Item));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
