using EnergyCalculator.Configuration;
using EnergyCalculator.Extensions;
using EnergyCalculator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnergyCalculator.Services
{
    public class FileObserverService : IFileObserverService
    {
        private readonly ILogger<FileObserverService> _logger;
        private readonly EnergyCalculatorSettings _settings;
        private readonly IDirectoryService _directoryService;
        private readonly IXmlProcessingService _fileProcessorService;
        private readonly IFileService _fileService;
        private readonly FileSystemWatcher _fileSystemWatcher;

        public FileObserverService(ILogger<FileObserverService> logger,
            IOptions<EnergyCalculatorSettings> options,
            IDirectoryService directoryService,
            IXmlProcessingService fileProcessorService,
            IFileService fileService)
        {
            _logger = logger;
            _settings = options.Value;
            _directoryService = directoryService;
            _fileProcessorService = fileProcessorService;
            _fileService = fileService;

            _fileSystemWatcher = new FileSystemWatcher();
        }

        public void StartObserving()
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
            var file = new FileInfo(e.FullPath);
            try
            {
                if (!file.IsXml())
                {
                    throw new FormatException("Input file should be an XML");
                }

                _logger.LogInformation($"Start processing file: {file.FullName}");

                var resultXml = ProcessFile(file);

                var outputFile = GetOutputFile(file);

                await _fileService.WriteToFileAsync(await resultXml, outputFile);

                _logger.LogInformation($"File {e.FullPath} processed successfully, saving to {outputFile.FullName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong during processing {file.FullName}");
                _logger.LogError(ex.ToString());
            }
        }

        private FileInfo GetOutputFile(FileInfo file)
        {
            var outputDirectory = _directoryService.GetOrCreateDirectory(_settings.OutputDirectoryPath);

            var outputFile = file.AddPrefix("-Result").ChangeDirectory(outputDirectory.FullName);
            return outputFile;
        }

        private async Task<string> ProcessFile(FileInfo file)
        {
            using (var xml = _fileService.LoadFile(file))
            {
                return await _fileProcessorService.ProcessXmlAsync(xml);
            }
        }

        public void StopObserving()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }
    }
}