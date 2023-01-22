using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccountTransaction.WebAPI.Core.DatabaseFlavor
{
    public class ProviderConfiguration
    {
        private readonly string _connectionString;
        public ProviderConfiguration With() => this;
        private static readonly string? MigrationAssembly = typeof(ProviderConfiguration).GetTypeInfo().Assembly.GetName().Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static ProviderConfiguration Build(string connString)
        {
            return new ProviderConfiguration(connString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        public ProviderConfiguration(string connString)
        {
            _connectionString = connString;
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<DbContextOptionsBuilder> SqlServer =>
            options => options.UseSqlServer(_connectionString, sql => sql.MigrationsAssembly(MigrationAssembly));


        /// <summary>
        /// it's just a tuple. Returns 2 parameters.
        /// Trying to improve readability at ConfigureServices
        /// </summary>
        public static (DatabaseType, string) DetectDatabase(IConfiguration configuration) => (
            configuration.GetValue<DatabaseType>("AppSettings:DatabaseType", DatabaseType.None),
            configuration.GetConnectionString("DefaultConnection"));
    }
}
