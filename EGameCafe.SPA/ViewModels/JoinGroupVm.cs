using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class JoinGroupVm : IJoinGroupVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IResponseService _responseService;
        private readonly IRepository _repository;
        private readonly ICurrentUserService _currentUser;
        private readonly NavigationManager _navigationManager;

        public JoinGroupVm(IResponseService responseService, IRepository repository, ICurrentUserService currentUser, NavigationManager navigationManager)
        {
            _responseService = responseService;
            _repository = repository;
            _currentUser = currentUser;
            _navigationManager = navigationManager;

            notification = new NotificationModel();
        }

        public string PageUri { set; get; } = "/joingroup";

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

        public async Task HandleJoinGroup(string sharingCode)
        {
            var userId = await _currentUser.GetUserId();

            var parameters = new Dictionary<string, string>();

            parameters.Add("token", sharingCode);
            parameters.Add("userId", userId);

            var result = await _repository.AuthorizePostQueryAsync( $"api/v1/GroupMember/JoinViaGroupInvitation?userId={userId}&token={sharingCode}");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo($"/chat/{ChatViewType.clear}", true);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
