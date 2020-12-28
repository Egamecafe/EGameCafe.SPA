using EGameCafe.SPA.Enums;
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
    public class CreateGroupVm : ICreateGroupVm, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IResponseService _responseService;
        private readonly IRepository _repository;
        private readonly ICurrentUserService _currentUser;
        private readonly NavigationManager _navigationManager;


        public CreateGroupVm(IResponseService responseService, IRepository repository, NavigationManager navigationManager, ICurrentUserService currentUser)
        {
            _responseService = responseService;
            _repository = repository;
            _currentUser = currentUser;

            item = new CreateGroupModel();
            notification = new NotificationModel();
            Games = new GetAllGames();
            _navigationManager = navigationManager;
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
        public string PageUri { set; get; } = "/chat/creategroup";

        private CreateGroupModel item;
        public CreateGroupModel Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        private GetAllGames games;
        public GetAllGames Games {
            get => games;
            set
            {
                games = value;
                OnPropertyChanged();
            }
        }

        public ChatViewType ViewType { get; set; }

        public async Task HabdleGetGames()
        {
            var groupsResult = await _repository.AuthorizeGetAsync<GetAllGames>("api/v1/Game/GetAllGames?from=0&count=100&sortType=gamename");
            var notifResult = await _responseService.ResponseResultChecker(groupsResult.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (groupsResult.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                games = groupsResult.ResultVm;
            }

            OnPropertyChanged(nameof(Item));
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task HandleCreateGroup(CreateGroupModel item)
        {
            item.CreatorId = await _currentUser.GetUserId();

            var result = await _repository.AuthorizePostAsync(item, "api/v1/Group/CreateGroup");

            var notifResult = await _responseService.ResponseResultChecker(result, PageUri, "Successful");

            if (result.Status != 201)
            {
                Notification = notifResult;
            }
            else
            {
                _navigationManager.NavigateTo($"/chat/{ChatViewType.clear}", true);
            }
        }
    }


}
