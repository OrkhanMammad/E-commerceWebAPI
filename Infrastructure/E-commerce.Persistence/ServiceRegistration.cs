using E_commerce.Application.Repositories.AccountRepos;
using E_commerce.Application.Repositories.BasketRepos;
using E_commerce.Application.Repositories.OrderRepos;
using E_commerce.Application.Repositories.ProductRepos;
using E_commerce.Domain.Entities.Identity;
using E_commerce.Persistence.DataAccessLayers;
using E_commerce.Persistence.Repositories.AccountRepos;
using E_commerce.Persistence.Repositories.BasketRepos;
using E_commerce.Persistence.Repositories.OrderRepos;
using E_commerce.Persistence.Repositories.ProductRepos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=DESKTOP-CJBAJUG\\SQLEXPRESS; Database=E-commerceWebApi; Trusted_Connection=true; Encrypt=False;"));
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IAccountReadRepository, AccountReadRepository>();
            services.AddScoped<IAccountWriteRepository, AccountWriteRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //options.Password.RequiredLength = 8;
                //options.User.RequireUniqueEmail = true;
                //options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        }


    }
}
