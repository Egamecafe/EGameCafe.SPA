using EGameCafe.SPA.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Installers
{
    public class ViewModelsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILoginVm, LoginVm>();
            services.AddScoped<IRegisterVm, RegisterVm>();
            services.AddScoped<IOTPVm, OTPVm>();
            services.AddScoped<IChatVm, ChatVm>();
            services.AddScoped<IResetPasswordVm, ResetPasswordVm>();
            services.AddScoped<ICreateGroupVm, CreateGroupVm>();
            services.AddScoped<IGroupInfoVm, GroupInfoVm>();
        }
    }
}
