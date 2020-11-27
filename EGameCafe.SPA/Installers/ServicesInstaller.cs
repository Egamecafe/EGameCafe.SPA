using EGameCafe.SPA.Installers;
using EGameCafe.SPA.Security;
using EGameCafe.SPA.Services.AccountService;
using EGameCafe.SPA.Services.ResponseServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Installers
{
    public class ServicesInstaller : IInstaller
    {
        //private readonly string URL = "https://drtalachilabapi.azurewebsites.net";
        private readonly string URL = "https://localhost:44376";
        //private readonly string URL = "http://95.216.55.253:5000";

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IResponseService, ResponseService>();

            //services.AddScoped<IFileService, FileService>();

            services.AddHttpClient<IAccountService, AccountService > (config =>
            {
                config.BaseAddress = new Uri(URL);
            });
        }
    }
}
