using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class FriendRequestVm : IFriendRequestVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IResponseService _responseService;
        private readonly IRepository _repository;
        private readonly ICurrentUserService _currentUser;

        public FriendRequestVm(IResponseService responseService, IRepository repository, ICurrentUserService currentUser)
        {
            _responseService = responseService;
            _repository = repository;

            item = new List<FriendRequestModel>();
            notification = new NotificationModel();
            _currentUser = currentUser;
        }

        public List<FriendRequestModel> item;
        public List<FriendRequestModel> Items
        {
            get => item;
            set
            {
                item = value;
                //pageLoader = false;
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
        public string PageUri { set; get; } = "/friendrequest";

        public async Task HandleGetAllFriendRequests()
        {
            var userId = await _currentUser.GetUserId();

            var result = await _repository.AuthorizeGetAsync<List<FriendRequestModel>>($"api/v1/FriendRequest/GetAllFriendRequest?userId={userId}");

            var notifResult = await _responseService.ResponseResultChecker(result.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (result.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                Items = result.ResultVm;
            }

            OnPropertyChanged(nameof(Items));
        }

        public async Task AcceptRequest(string id)
        {
            var userId = await _currentUser.GetUserId();

            FriendRequestStatusModel model = new() { ReceiverId = userId, SenderId = id };

            var result = await _repository.AuthorizePostAsync(model, "api/v1/FriendRequest/AcceptFriendRequest");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                await HandleGetAllFriendRequests();
            }
        }

        public async Task DeclineRequest(string id)
        {
            var userId = await _currentUser.GetUserId();

            FriendRequestStatusModel model = new() { ReceiverId = userId, SenderId = id };

            var result = await _repository.AuthorizePostAsync(model, "api/v1/FriendRequest/DeclineFriendRequest");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                await HandleGetAllFriendRequests();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
