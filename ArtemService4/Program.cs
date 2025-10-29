using ArtemService4;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Runtime.InteropServices;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: @"C:\Users\dubch\Documents\VS Publish Dir\MyService\Logs\log-.txt",
        rollingInterval: RollingInterval.Minute,
        retainedFileCountLimit: 5,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting up worker service...");
    IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseSerilog()
    .ConfigureServices((context, services) =>
    {
        services.Configure<WorkerSettings>(context.Configuration.GetSection("Workers"));

        services.AddHostedService<Worker>();
        services.AddHostedService<Worker2>();
    })
    .Build();

    try
    {
        if (args == null)
        {
            Log.Information("args is null");
        }
        else if (args.Length == 0)
        {
            Log.Information("args is empty");
        }
        else
        {
            Log.Information(String.Join("||", args));
        }


    }
    catch (Exception ex)
    {
        Log.Error($"Exception {ex}");
    }
    await host.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly.");
}
finally
{
    Log.CloseAndFlush();
}
