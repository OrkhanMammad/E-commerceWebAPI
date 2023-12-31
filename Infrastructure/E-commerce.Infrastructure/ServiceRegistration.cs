﻿using E_commerce.Application.Abstractions.Token;
using E_commerce.Application.Services;
using E_commerce.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_commerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, Services.Token.TokenHandler>();
            //services.AddScoped<IMailService, MailService>();

        }


    }
}
