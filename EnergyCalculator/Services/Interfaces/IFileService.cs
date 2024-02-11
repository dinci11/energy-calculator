namespace EnergyCalculator.Services.Interfaces
{
    public interface IFileService
    {
        FileStream LoadFile(FileInfo file);

        Task WriteToFileAsync(string result, FileInfo outputFile);
    }
}