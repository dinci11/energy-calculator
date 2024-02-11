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
        private readonly IFileProcessorService _fileProcessorService;
        private readonly FileSystemWatcher _fileSystemWatcher;

        public FileObserverService(ILogger<FileObserverService> logger,
            IOptions<FileObserverSettings> options,
            IDirectoryService directoryService,
            IFileProcessorService fileProcessorService)
        {
            _logger = logger;
            _settings = options.Value;
            _directoryService = directoryService;
            _fileProcessorService = fileProcessorService;

            _fileSystemWatcher = new FileSystemWatcher();
        }

        public void StartObservingInputFolderAsync()
        {
            _logger.LogInformation("Start observing...");

            var inputDirectory = _directoryService.GetOrCreateDirectory(_settings.InputDirectoryPath);

            _logger.LogInformation($"Input directory: {inputDirectory.FullName}");

            StartMonitoring(inputDirectory);
        }

        private void StartMonitoring(DirectoryInfo inputDirectory)
        {
            _fileSystemWatcher.Path = inputDirectory.FullName;

            _fileSystemWatcher.Created += async (sender, e) => await ProcessNewFileAsync(sender, e);

            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private async Task ProcessNewFileAsync(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"Start processing file: {e.FullPath}");
            var result = await _fileProcessorService.ProcessFileAsync(e.FullPath);

            if (!result.IsSuccess)
            {
                _logger.LogError($"Processing {e.FullPath} failed");
                return;
            }

            _logger.LogInformation($"File {e.FullPath} processed result saved to {result.ResultFile?.FullName ?? "N/A"}");
        }
    }
}