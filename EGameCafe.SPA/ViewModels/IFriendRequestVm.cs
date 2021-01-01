using EGameCafe.SPA.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IFriendRequestVm 
    {
        event PropertyChangedEventHandler PropertyChanged;
        public string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        List<FriendRequestModel> Items { get; set; }
        FriendRequestCreateModel ModelItem { get; set; }
        List<UserSearchModel> SearchItems { get; set; }
        bool IsShowResult { get; set; }
        Task HandleSearchUser(string username);
        Task HandleGetAllFriendRequests();
        Task AcceptRequest(string id);
        Task DeclineRequest(string id);
        Task HandleSearchClick(UserSearchModel user);
        Task HandleSendRequest();
    }
}
