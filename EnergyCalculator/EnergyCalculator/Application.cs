using Microsoft.Extensions.Logging;

namespace EnergyCalculator
{
    public class Application
    {
        private readonly ILogger<Application> _logger;

        public Application(ILogger<Application> logger)
        {
            _logger = logger;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Application started...");
            await Task.Delay(1000);
            _logger.LogInformation("Application shutting down...");
        }
    }
}