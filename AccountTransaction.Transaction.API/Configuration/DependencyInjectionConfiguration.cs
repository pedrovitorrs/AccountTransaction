using AccountTransaction.MessageBus;
using AccountTransaction.MessageBus.RabbitMQSender;
using AccountTransaction.Transaction.API.Configuration.UserIdentity;
using AccountTransaction.Transaction.API.Data.Repository;
using AccountTransaction.Transaction.API.Models;
using AccountTransaction.Transaction.API.Services;
using AccountTransaction.Transaction.API.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountTransaction.Transaction.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddMessageBus(configuration);

            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IRepository<Transacao>, Repository<Transacao>>();
        }
    }
}