using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;


namespace EGameCafe.SPA.ViewModels
{
    public interface IIndexVm
    {
        event PropertyChangedEventHandler PropertyChanged;
        public string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        GetUserDashboardInfoModel Item { get; set; }
        CreateActivityModel ActivityItem { get; set; }
        bool PageLoader { get; set; }
        Task HandleGetUserDashboard(string userId);
        Task HandleUpdateSystemInfo(UserSystemInfoModel item);
        Task HandlePostActivity(CreateActivityModel item);
    }
}
