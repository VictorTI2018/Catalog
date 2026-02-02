using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGeek.Catalog
{
    public static class DependencyInjection
    {

        public static void AddCatalogServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EGeekCatalogDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("SqlServerDbConnection");
                options.UseSqlServer(connectionString,
                    config => config.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog"));
            });

            services.AddScoped<IUnitOfWork>(provider =>
               provider.GetRequiredService<EGeekCatalogDbContext>());
        }
    }
}
