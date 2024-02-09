using EnergyCalculator.Configuration;
using EnergyCalculator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnergyCalculator.Services
{
    public class FileObserverService : IFileObserverService
    {
        private readonly ILogger<FileObserverService> _logger;
        private readonly FileObserverSettings _settings;
        private readonly IDirectoryService _directoryService;

        public FileObserverService(ILogger<FileObserverService> logger,
            IOptions<FileObserverSettings> options,
            IDirectoryService directoryService)
        {
            _logger = logger;
            _settings = options.Value;
            _directoryService = directoryService;
        }

        public async Task ObservInputFolderAsync()
        {
            _logger.LogInformation("Start observing...");

            var inputDirectory = _directoryService.GetOrCreateDirectory(_settings.InputDirectoryPath);

            _logger.LogInformation($"Input directory: {inputDirectory.FullName}");

            var watcher = new FileSystemWatcher(inputDirectory.FullName);
            watcher.Created += async (s, e) => await ProcessNewFileAsync(s, e);

            watcher.EnableRaisingEvents = true;
        }

        private async Task ProcessNewFileAsync(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(e.FullPath))
            {
                throw new FileNotFoundException(e.FullPath);
            }

            _logger.LogInformation($"Start processing file: {e.FullPath}");
            await Task.Delay(1000);
        }
    }
}