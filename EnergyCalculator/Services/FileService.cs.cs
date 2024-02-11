using EnergyCalculator.Services.Interfaces;

namespace EnergyCalculator.Services
{
    public class FileService : IFileService
    {
        public FileStream LoadFile(FileInfo file)
        {
            if (FileNotExists(file.FullName))
            {
                throw new FileNotFoundException($"File {file.FullName} not exits");
            }

            var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);

            return stream;
        }

        private static bool FileNotExists(string filePath)
        {
            return !File.Exists(filePath);
        }

        public async Task WriteToFileAsync(string result, FileInfo outputFile)
        {
            using (var writer = new StreamWriter(File.Open(outputFile.FullName, FileMode.OpenOrCreate)))
            {
                await writer.WriteAsync(result);
            }
        }
    }
}