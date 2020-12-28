using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface ICreateGroupVm
    {
        event PropertyChangedEventHandler PropertyChanged;
        string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        CreateGroupModel Item { get; set; }
        GetAllGames Games { get; set; }
        ChatViewType ViewType { get; set; }
        Task HabdleGetGames();
        Task HandleCreateGroup(CreateGroupModel item);
    }
}