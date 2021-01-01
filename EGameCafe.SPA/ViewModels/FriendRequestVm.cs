using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

            items = new List<FriendRequestModel>();
            notification = new NotificationModel();
            _currentUser = currentUser;
            modelItem = new FriendRequestCreateModel();
            searchItem = new List<UserSearchModel>();
        }

        public List<FriendRequestModel> items;
        public List<FriendRequestModel> Items
        {
            get => items;
            set
            {
                items = value;
                //pageLoader = false;
                OnPropertyChanged();
            }
        }

        private bool isShowResult;
        public bool IsShowResult
        {
            get => isShowResult;
            set
            {
                isShowResult = value;
                OnPropertyChanged();
            }
        }

        public FriendRequestCreateModel modelItem;
        public FriendRequestCreateModel ModelItem
        {
            get => modelItem;
            set
            {
                modelItem = value;
                OnPropertyChanged();
            }
        }

        public List<UserSearchModel> searchItem;
        public List<UserSearchModel> SearchItems
        {
            get => searchItem;
            set
            {
                searchItem = value;
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

        public async Task HandleSearchUser(string username)
        {
            var userId = await _currentUser.GetUserId();

            var result = await _repository.AuthorizeGetAsync<List<UserSearchModel>>($"api/v1/user/GetUser?username={username}&currentUserId={userId}");

            var notifResult = await _responseService.ResponseResultChecker(result.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (result.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                searchItem = result.ResultVm;

                if (searchItem.Any())
                {
                    isShowResult = true;
                }
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

        public async Task HandleSendRequest()
        {
            var result = await _repository.AuthorizePostAsync(modelItem, "api/v1/FriendRequest/CreateFriendRequest");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status == 200)
            {
                await HandleGetAllFriendRequests();
            }

            Notification = notifResult;
        }

        public async Task HandleSearchClick(UserSearchModel user)
        {
            var userId = await _currentUser.GetUserId();

            modelItem.ReceiverId = user.UserId;
            modelItem.SenderId = userId;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
