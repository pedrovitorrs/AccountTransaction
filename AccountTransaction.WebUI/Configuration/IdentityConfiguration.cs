using Microsoft.AspNetCore.Authentication.Cookies;

namespace AccountTransaction.WebUI.Configuration
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.LoginPath = "/users/login";
                    options.AccessDeniedPath = "/erro/403";
                });

            services.AddHttpContextAccessor();
        }

        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
