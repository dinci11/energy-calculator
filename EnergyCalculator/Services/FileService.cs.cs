using EnergyCalculator.Services.Interfaces;

namespace EnergyCalculator.Services
{
    public class FileService : IFileService
    {
        public FileStream LoadFile(string filePath)
        {
            if (FileNotExists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not exits");
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return stream;
        }

        private static bool FileNotExists(string filePath)
        {
            return !File.Exists(filePath);
        }
    }
}