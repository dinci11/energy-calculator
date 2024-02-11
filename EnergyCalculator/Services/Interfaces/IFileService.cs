namespace EnergyCalculator.Services.Interfaces
{
    public interface IFileService
    {
        FileStream LoadFile(string filePath);
    }
}