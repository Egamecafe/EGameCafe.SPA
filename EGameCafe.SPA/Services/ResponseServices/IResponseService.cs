using EGameCafe.SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services.ResponseServices
{
    public interface IResponseService
    {
        Task<NotificationModel> ResponseResultChecker(Result result, string pageUri, string succeedMessage);
    }
}
