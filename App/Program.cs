using System;
using System.Linq;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

using Microsoft.Extensions.DependencyInjection;
using NetCore.Docker.FulentMigration.Data.Migrations;

namespace NetCore.Docker.FulentMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
            
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString("Data Source=test.db")
                    .ScanIn(typeof(AddLogTable).Assembly).For.Migrations()
                ).AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
