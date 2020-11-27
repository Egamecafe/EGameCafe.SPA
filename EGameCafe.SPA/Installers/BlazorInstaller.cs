using Blazored.LocalStorage;
using EGameCafe.SPA.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Installers
{
    public class BlazorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();

            services.AddRazorPages();

            services.AddServerSideBlazor();

            services.AddBlazoredLocalStorage();

            services.AddSingleton<WeatherForecastService>();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

    }
}
