using Laboratory.Client.SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services
{
    public interface IRepository<T> where T : class
    {
        Task<Result> AuthorizePostAsync(T command, string rout);
    }
}
