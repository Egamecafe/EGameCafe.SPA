using EGameCafe.SPA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public interface IRegisterVm
    {
        event PropertyChangedEventHandler PropertyChanged;

        public string PageUri { get; set; }
        public string Fullname { get; set; }
        NotificationModel Notification { get; set; }
        RegisterModel Item { get; set; }
        Task HandleRegister(RegisterModel model);
    }
}
