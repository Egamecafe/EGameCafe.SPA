using EGameCafe.SPA.Models;
using EGameCafe.SPA.Validations;
using EGameCafe.SPA.ViewModels;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Installers
{
    public class ValidationsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<LoginVm>, LoginVmValidation>();
            services.AddTransient<IValidator<RegisterVm>, RegisterVmValidation>();
            services.AddTransient<IValidator<OTPVm>, OTPValidation>();
            services.AddTransient<IValidator<ForgotPasswordModel>, ForgotPasswordValidation>();
            services.AddTransient<IValidator<ResetPasswordVm>, ResetPasswordValodation>();
            services.AddTransient<IValidator<CreateGroupVm>, CreateGroupValidation>();
            //services.AddTransient<IValidator<CreateGroupVm>, CreateGroupValidation>();
        }
    }
}
