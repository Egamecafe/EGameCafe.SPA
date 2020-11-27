using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IOTPVm
    {
        event PropertyChangedEventHandler PropertyChanged;

        public string PageUri { get; set; }
        NotificationModel Notification { get; set; }

        int OTPNumber { get; set; }
        string Type { get; set; }

        Task HandleOTP(int number);
    }
}
