using EnergyCalculator.Model.Report;
using EnergyCalculator.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EnergyCalculator.Services
{
    public class XmlProcessingService : IXmlProcessingService
    {
        private readonly ILogger<XmlProcessingService> _logger;
        private readonly IXmlService _xmlService;
        private readonly IReportProcessingService _reportProcessingService;

        public XmlProcessingService(ILogger<XmlProcessingService> logger, IXmlService xmlService, IReportProcessingService reportProcessingService)
        {
            _logger = logger;
            _xmlService = xmlService;
            _reportProcessingService = reportProcessingService;
        }

        public async Task<string> ProcessXmlAsync(Stream xml)
        {
            var report = DeserializeFile(xml);

            var generationOutput = await _reportProcessingService.ProcessAsync(report);

            return _xmlService.SerializeToString(generationOutput);
        }

        private GenerationReport DeserializeFile(Stream stream)
        {
            return _xmlService.Deserialize<GenerationReport>(stream);
        }
    }
}