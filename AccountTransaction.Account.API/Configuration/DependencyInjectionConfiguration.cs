using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Commom.Core.UserIdentity;
using AccountTransaction.MessageBus;
using GeekShopping.OrderAPI.MessageConsumer;
using Microsoft.Extensions.Configuration;

namespace AccountTransaction.Account.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddMessageBus(configuration);
            services.AddHostedService<RabbitMQTransactionConsumer>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICardService, CardService>();

            services.AddScoped<IRepository<Conta>, Repository<Conta>>();
            services.AddScoped<IRepository<Cartao>, Repository<Cartao>>();
        }
    }
}