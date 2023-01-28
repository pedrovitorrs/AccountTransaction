using AccountTransaction.Identity.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NetDevPack.Security.PasswordHasher.Core;
using Microsoft.EntityFrameworkCore;

namespace AccountTransaction.Identity.API.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddMemoryCache()
                    .AddDataProtection();

            services.AddJwtConfiguration(configuration, "AppSettings")
                    .AddNetDevPackIdentity<IdentityUser>()
                    .PersistKeysToDatabaseStore<ApplicationDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredUniqueChars = 0;
                o.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.UpgradePasswordSecurity()
                .WithStrenghten(PasswordHasherStrenght.Moderate)
                .UseArgon2<IdentityUser>();

            return services;
        }
    }
}
