using System;
using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IJoinGroupVm
    {
        event PropertyChangedEventHandler PropertyChanged;
        NotificationModel Notification { get; set; }

        string PageUri { get; set; }
        Task HandleJoinGroup(string sharingCode);
    }
}
