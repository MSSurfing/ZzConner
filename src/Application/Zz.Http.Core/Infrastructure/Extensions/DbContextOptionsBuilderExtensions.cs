using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zz.Core.Data;

namespace Zz.Http.Core.Infrastructure.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static void UseSqlServerOption(this DbContextOptionsBuilder optionsBuilder, IServiceCollection services)
        {
            var dbSettings = DbSettingsManager.Load();
            if (dbSettings == null || string.IsNullOrEmpty(dbSettings.DbConnectionString))
                return;

            var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
            dbContextOptionsBuilder.UseSqlServer(dbSettings.DbConnectionString, option => option.UseRowNumberForPaging());
        }

        public static void UseMySqlOption(this DbContextOptionsBuilder optionsBuilder, IServiceCollection services)
        {
            var dbSettings = DbSettingsManager.Load();
            if (dbSettings == null || string.IsNullOrEmpty(dbSettings.DbConnectionString))
                return;

            var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
            dbContextOptionsBuilder.UseMySql(dbSettings.DbConnectionString);
        }
    }
}
