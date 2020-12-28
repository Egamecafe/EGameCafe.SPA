using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace EGameCafe.SPA.ViewModels
{
    public class IndexVm : IIndexVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IResponseService _responseService;
        private readonly IRepository _repository;
        private readonly ICurrentUserService _currentUser;

        public IndexVm(IResponseService responseService, IRepository repository, ICurrentUserService currentUser)
        {
            _responseService = responseService;
            _repository = repository;
            _currentUser = currentUser;

            item = new GetUserDashboardInfoModel();
            notification = new NotificationModel();
        }

        public GetUserDashboardInfoModel item;
        public GetUserDashboardInfoModel Item
        {
            get => item;
            set
            {
                item = value;
                pageLoader = false;
                OnPropertyChanged();
            }
        }

        public CreateActivityModel activityItem;
        public CreateActivityModel ActivityItem
        {
            get => activityItem;
            set
            {
                activityItem = value;
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
        public string PageUri { set; get; } = "/";

        private bool pageLoader = true;
        public bool PageLoader
        {
            get => pageLoader;
            set
            {
                pageLoader = value;
                OnPropertyChanged();
            }
        }

        public async Task HandleGetUserDashboard(string userId)
        {
            var result = await _repository.AuthorizeGetAsync<GetUserDashboardInfoModel>($"api/v1/Dashboard/GetUserDashboard?userId={userId}");

            var notifResult = await _responseService.ResponseResultChecker(result.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (result.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                Item = result.ResultVm;
            }

            OnPropertyChanged(nameof(Item));
        }

        public async Task HandleUpdateSystemInfo(UserSystemInfoModel item)
        {
            var result = await _repository.AuthorizePutAsync(item, "api/v1/UserSystemInfo/UpdateUserSystemInfo");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                OnPropertyChanged(nameof(Item));
            }
        }

        public async Task HandlePostActivity(CreateActivityModel item)
        {
            item.UserId = await _currentUser.GetUserId();

            var result = await _repository.AuthorizePostAsync(item, "api/v1/Activity/CreateActivity");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status != 201)
            {
                Notification = notifResult;
            }
            else
            {
                await HandleGetUserDashboard(item.UserId);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
