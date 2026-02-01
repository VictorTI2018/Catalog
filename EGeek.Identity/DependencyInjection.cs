using EGeek.Identity.Configuration;
using EGeek.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGeek.Identity
{
    public static class DependencyInjection
    {
        public static void AddIdentityServicesModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services
            .AddOptions<AuthenticateOptions>()
            .Bind(configuration.GetSection("Jwt"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EGeekIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<EGeekIdentityDbContext>(options =>
            {
                var connection = configuration.GetConnectionString("EGeekDbConnection");

                options.UseSqlServer(connection,
                    config => config.MigrationsHistoryTable("__EFMigrationsHistory", "EGeekIdentity"));
            });
        }
    }
}
