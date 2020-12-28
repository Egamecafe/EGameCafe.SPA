using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class ChatVm : IChatVm , INotifyPropertyChanged
    {
        private readonly IResponseService _responseService;
        private readonly IRepository _repository;
        private readonly ICurrentUserService _currentUser;

        public ChatVm(IResponseService responseService, IRepository repository, ICurrentUserService currentUser)
        {
            _responseService = responseService;
            _repository = repository;
            _currentUser = currentUser;

            notification = new NotificationModel();
            allGroups = new GetAllGroups();
            userGroups = new GetAllGroups();
            currentGroup = new GetAllGroupsDto();
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

        public GetAllGroups allGroups;
        public GetAllGroups AllGroups 
        {
            get => allGroups;
            set
            {
                allGroups = value;
                OnPropertyChanged();
            }
        }

        public GetAllGroups userGroups;
        public GetAllGroups UserGroups
        {
            get => userGroups;
            set
            {
                userGroups = value;
                OnPropertyChanged();
            }
        }


        public GetAllGroupsDto currentGroup;
        public GetAllGroupsDto CurrentGroup 
        {
            get => currentGroup;
            set
            {
                currentGroup = value;
                OnPropertyChanged();
            }
        }

        public ChatViewType ViewType { get; set; }

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
                allGroups = groupsResult.ResultVm;
            }

            OnPropertyChanged(nameof(AllGroups));
        }

        public async Task HandleGetUserGroups()
        {
            var userId = await _currentUser.GetUserId();

            var groupsResult = await _repository.AuthorizeGetAsync<GetAllGroups>($"api/v1/Group/GetAllUserGroups/{userId}");

            var notifResult = await _responseService.ResponseResultChecker(groupsResult.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (groupsResult.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                userGroups = groupsResult.ResultVm;
            }

            OnPropertyChanged(nameof(AllGroups));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
