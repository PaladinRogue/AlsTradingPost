﻿using ApplicationManager.ApplicationServices.Identities.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class SettingRegistration
    {
        public static void RegisterSystemAdminIdentitySettings(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<SystemAdminIdentitySettings>(configuration.GetSection(nameof(SystemAdminIdentitySettings)));
        }
    }
}