using EnergyCalculator.Model;

namespace EnergyCalculator.Services.Interfaces
{
    public interface IReportProcessingService
    {
        Task<GenerationOutput> ProcessAsync(GenerationReport report);
    }
}