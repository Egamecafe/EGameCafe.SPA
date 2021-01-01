using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.ResponseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace EGameCafe.SPA.ViewModels
{
    public class GroupInfoVm : IGroupInfoVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IResponseService _responseService;
        private readonly IRepository _repository;

        public GroupInfoVm(IResponseService responseService, IRepository repository)
        {
            _responseService = responseService;
            _repository = repository;

            notification = new NotificationModel();
            item = new GetGroupByIdVm();
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

        public string sharingLink;
        public string SharingLink
        {
            get => sharingLink;
            set
            {
                sharingLink = value;
                OnPropertyChanged();
            }
        }


        public string PageUri { set; get; } = "/groupchat";

        public GetGroupByIdVm item;
        public GetGroupByIdVm Item
        {
            get => item;
            set
            {
                item = value;
                sharingLink = "joingroup/" + item.GroupInfo.SharingLink;
                OnPropertyChanged();
            }
        }

        public async Task HandleGetGroup(string groupId)
        {
            var groupsResult = await _repository.AuthorizeGetAsync<GetGroupByIdVm>($"api/v1/Group/GetGroup/{groupId}");

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
