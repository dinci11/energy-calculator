using EnergyCalculator.Configuration;
using EnergyCalculator.Services;
using EnergyCalculator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EnergyCalculator
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var application = host.Services.GetService<Application>();

            await application.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            ConfigureLogging(host);
            ConfigureServices(host);
            return host;
        }

        private static void ConfigureLogging(IHostBuilder host)
        {
            host.ConfigureLogging(logging =>
            {
                logging.AddConsole();
            });
        }

        private static void ConfigureServices(IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.Configure<EnergyCalculatorSettings>(context.Configuration.GetSection(nameof(EnergyCalculatorSettings)));

                services.AddTransient<IFileService, FileService>();
                services.AddTransient<IXmlService, XmlService>();
                services.AddTransient<IXmlProcessingService, XmlProcessingService>();
                services.AddTransient<IDirectoryService, DirectoryService>();
                services.AddTransient<IReportProcessingService, ReportProcessingService>();

                services.AddSingleton<IFileObserverService, FileObserverService>();

                services.AddSingleton<Application>();
            });
        }
    }
}