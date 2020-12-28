using EGameCafe.SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services
{
    public interface IRepository
    {
        Task<Result> AuthorizePostAsync<T>(T command, string rout);
        Task<(Result Result, T ResultVm)> AuthorizeGetAsync<T>(string rout);
        Task<Result> AuthorizePutAsync<T>(T command, string rout);
    }
}
