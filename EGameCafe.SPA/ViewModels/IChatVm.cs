using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IChatVm
    {
        event PropertyChangedEventHandler PropertyChanged;
        GetAllGroupsDto CurrentGroup { get; set; }
        string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        GetAllGroups AllGroups { get; set; }
        GetAllGroups UserGroups { get; set; }
        ChatViewType ViewType { get; set; }
        Task HandleGetGroups();
        Task HandleGetUserGroups();
    }
}
