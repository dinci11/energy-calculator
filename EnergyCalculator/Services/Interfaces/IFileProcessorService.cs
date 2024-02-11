using EnergyCalculator.DTOs;

namespace EnergyCalculator.Services.Interfaces
{
    public interface IFileProcessorService
    {
        Task<FileProcessingResult> ProcessFileAsync(string fullPath);
    }
}