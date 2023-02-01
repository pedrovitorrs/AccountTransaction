using AccountTransaction.Commom.Core.UserIdentity;
using AccountTransaction.WebUI.Services.Handlers;
using AccountTransaction.WebUI.Services.Implementation;
using AccountTransaction.WebUI.Services.Interface;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace AccountTransaction.WebUI.Configuration
{
    public static class ServicesConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            #region HttpServices

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthService, AuthService>()
                .AddPolicyHandler(PollyExtensions.WaitAndRetry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            #endregion
        }

        #region PollyExtension

        public static class PollyExtensions
        {
            public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetry()
            {
                var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    }, (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retryCount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

                return retry;
            }
        }

        #endregion
    }
}
