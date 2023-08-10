using Chinwag.DatabaseMigration.Exceptions;
using DbUp;
using DbUp.Engine;
using Microsoft.Data.SqlClient;
using Prism.Ioc;
using Serilog;
using System.Reflection;

namespace Chinwag.DatabaseMigration;

public static class DatabaseMigrationInstaller
{
    public static IContainerRegistry AddDatabaseMigrationServices(this IContainerRegistry containerRegistry, string connectionString)
    {
        Log.Logger.Information("Executing database migration at {@DatabaseMigrationStartTime}", DateTime.UtcNow);

        try
        {
            var upgradeEngine =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    // PreDeployment scripts
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Chinwag.DatabaseMigration.Scripts.PreDeployment."), new SqlScriptOptions { RunGroupOrder = 1 })
                    // Migration scripts
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Chinwag.DatabaseMigration.Scripts.Migrations."), new SqlScriptOptions { RunGroupOrder = 2 })
                    // PostDeployment scripts
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Chinwag.DatabaseMigration.Scripts.PostDeployment."), new SqlScriptOptions { RunGroupOrder = 3 })
                    .LogToAutodetectedLog()
                    .WithTransaction()
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

        return containerRegistry;
    }
}