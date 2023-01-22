using AccountTransaction.WebAPI.Core.Configuration;
using AccountTransaction.WebUI.Configuration.Settings;
using Microsoft.AspNetCore.HttpOverrides;

namespace AccountTransaction.WebUI.Configuration
{
    public static class WebUIConfiguration
    {
        public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.Configure<ApplicationSettings>(configuration);
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDefaultHealthCheck(configuration);
        }

        public static void UseMvcConfiguration(this WebApplication app)
        {
            app.UseForwardedHeaders();

            app.UseExceptionHandler("/error");
            app.UseStatusCodePagesWithRedirects("/error");

            // Under certain scenarios, e.g minikube / linux environment / behind load balancer
            // https redirection could lead dev's to over complicated configuration for testing purpouses
            // In production is a good practice to keep it true
            if (app.Configuration["USE_HTTPS_REDIRECTION"] == "true")
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseDefaultHealthcheck();

            app.UseEndpoints(endpoints =>
            {
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); ;
            });
        }
    }
}
