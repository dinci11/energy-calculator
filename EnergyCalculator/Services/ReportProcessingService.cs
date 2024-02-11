using EnergyCalculator.Model;
using EnergyCalculator.Services.Interfaces;

namespace EnergyCalculator.Services
{
    public class ReportProcessingService : IReportProcessingService
    {
        public Task<GenerationOutput> ProcessAsync(GenerationReport report)
        {
            var output = new GenerationOutput();

            return Task.FromResult(output);
        }
    }
}