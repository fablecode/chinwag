using System;
using System.Data.SqlClient;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using Identity.DatabaseMigration.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Identity.DatabaseMigration
{
    public static class DatabaseMigrationInstaller
    {
        public static IServiceCollection AddDatabaseMigrationServices(this IServiceCollection services, string connectionString)
        {
            Log.Logger.Information("Executing database migration at {@DatabaseMigrationStartTime}", DateTime.UtcNow);

            try
            {
                var upgradeEngine =
                    DeployChanges.To
                        .SqlDatabase(connectionString)
                        // PreDeployment scripts
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Identity.DatabaseMigration.Scripts.PreDeployment."), new SqlScriptOptions { RunGroupOrder = 1 })
                        // Migration scripts
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Identity.DatabaseMigration.Scripts.Migrations."), new SqlScriptOptions { RunGroupOrder = 2 })
                        // PostDeployment scripts
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Identity.DatabaseMigration.Scripts.PostDeployment."), new SqlScriptOptions { RunGroupOrder = 3 })
                        .LogToAutodetectedLog()
                        .Build();

                EnsureDatabase.For.SqlDatabase(connectionString);

                var result = upgradeEngine.PerformUpgrade();

                if (result.Successful)
                {
                    Log.Logger.Information("Database migration successfully executed at {@DatabaseMigrationEndTime}!", DateTime.UtcNow);
                }
                else
                {
                    Log.Logger.Fatal(result.Error.Message);
                    throw new DatabaseMigrationException("Database migration failed. Stopping system! Please see the logs!");
                }
            }
            catch (SqlException ex)
            {
                Log.Logger.Fatal("Database migration failed with exception: {@Exception}", ex);
                throw new DatabaseMigrationException("Database migration failed. Stopping system! Please see the logs!", ex);
            }

            return services;
        }
    }
}
