using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface ILoginVm
    {
        event PropertyChangedEventHandler PropertyChanged;

        public string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        LoginModel Item { get; set; }
        Task HandleLogin(LoginModel model);

    }
}
