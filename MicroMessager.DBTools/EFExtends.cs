using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroMessager.DBTools
{
    public static class EFExtends
    {
        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder = null) where TContext : DbContext
        {
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();
            var configuration = services.GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            try
            {
                logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
                logger.LogInformation($"Connection string is {connectionString}");
                AsyncHelper.TryAsyncThreeTimes(async () =>
                {
                    await context.Database.MigrateAsync();
                    seeder?.Invoke(context, services);
                });
                logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
            }

            return host;
        }

    }
}