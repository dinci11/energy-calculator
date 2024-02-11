namespace EnergyCalculator.Services.Interfaces
{
    public interface IXmlProcessingService
    {
        Task<string> ProcessXmlAsync(Stream xml);
    }
}