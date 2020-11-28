using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IChatVm
    {
        event PropertyChangedEventHandler PropertyChanged;
        string CurrentGroupId { get; set; }
        string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        GetAllGroups Item { get; set; }

        Task HandleGetGroups();
    }
}
