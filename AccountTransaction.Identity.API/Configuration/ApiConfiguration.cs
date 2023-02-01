using AccountTransaction.Commom.Core.UserIdentity;

namespace AccountTransaction.Identity.API.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Under certain scenarios, e.g minikube / linux environment / behind load balancer
            // https redirection could lead dev's to over complicated configuration for testing purpouses
            // In production is a good practice to keep it true
            if (app.Configuration["USE_HTTPS_REDIRECTION"] == "true")
                app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseJwksDiscovery();

            return app;
        }
    }
}
