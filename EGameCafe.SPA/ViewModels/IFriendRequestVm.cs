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
        Task HandleGetAllFriendRequests();
        Task AcceptRequest(string id);
        Task DeclineRequest(string id);
    }
}
