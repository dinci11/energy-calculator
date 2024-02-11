using EnergyCalculator.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EnergyCalculator
{
    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IFileObserverService _fileObserverService;

        public Application(ILogger<Application> logger, IFileObserverService fileObserverService)
        {
            _logger = logger;
            _fileObserverService = fileObserverService;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Application started...");
            _fileObserverService.StartObservingInputFolderAsync();
            _logger.LogInformation("Press Escape to exit");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                _logger.LogInformation("Press Escape to exit");
                await Task.Delay(500);
            }

            _logger.LogInformation("Application shutting down...");
        }
    }
}