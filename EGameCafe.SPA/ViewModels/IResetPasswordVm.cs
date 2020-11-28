using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;


namespace EGameCafe.SPA.ViewModels
{
    public interface IResetPasswordVm
    {
        event PropertyChangedEventHandler PropertyChanged;

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string PageUri { get; set; }
        NotificationModel Notification { get; set; }
        Task HandleResetPassword(string password);

    }
}
