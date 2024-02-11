using System.Xml.Serialization;
using EnergyCalculator.DTOs;
using EnergyCalculator.Model;
using EnergyCalculator.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EnergyCalculator.Services
{
    public class FileProcessorService : IFileProcessorService
    {
        private readonly ILogger<FileProcessorService> _logger;
        private readonly IFileService _fileService;

        public FileProcessorService(ILogger<FileProcessorService> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public Task<FileProcessingResult> ProcessFileAsync(string fullPath)
        {
            try
            {
                using (var stream = _fileService.LoadFile(fullPath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(GenerationReport));

                    var generationReport = (GenerationReport)xmlSerializer.Deserialize(stream);

                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return Task.FromResult(new FileProcessingResult(Enums.ProcessingResutl.Faild));
            }
        }
    }
}