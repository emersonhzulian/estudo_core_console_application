using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<ITest, Test>();
                })
                .UseSerilog()
                .Build();
                
            var svc = host.Services.GetService<ITest>();
            // var svc = ActivatorUtilities.CreateInstance<Test>(host.Services);

            svc.Run();

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMET") ?? "Development"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
