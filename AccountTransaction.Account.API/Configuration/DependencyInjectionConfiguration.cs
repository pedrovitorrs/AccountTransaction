using AccountTransaction.Account.API.Configuration.UserIdentity;
using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.Models;

namespace AccountTransaction.Account.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IRepository<Conta>, Repository<Conta>>();
            services.AddScoped<IRepository<Cartao>, Repository<Cartao>>();
        }
    }
}