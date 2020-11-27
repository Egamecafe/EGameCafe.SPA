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
            //services.AddTransient<IValidator<AddSamplingTimeVm>, SamplingTimePickerValidation>();
            
        }
    }
}
