using EnergyCalculator.Model;
using EnergyCalculator.Model.Report;

namespace EnergyCalculator.Services.Interfaces
{
    public interface IReportProcessingService
    {
        Task<GenerationOutput> ProcessAsync(GenerationReport report);
    }
}