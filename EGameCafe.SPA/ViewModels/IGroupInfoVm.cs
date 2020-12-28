using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IGroupInfoVm
    {
        event PropertyChangedEventHandler PropertyChanged;
        string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        GetGroupByIdVm Item { get; set; }
        Task HandleGetGroup(string groupId);
    }
}
