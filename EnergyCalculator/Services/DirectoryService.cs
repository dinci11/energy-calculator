using EnergyCalculator.Services.Interfaces;

namespace EnergyCalculator.Services
{
    public class DirectoryService : IDirectoryService
    {
        public DirectoryInfo GetOrCreateDirectory(string path) => DirectoryNotExits(path) ? Directory.CreateDirectory(path) : new DirectoryInfo(path);

        private static bool DirectoryNotExits(string path)
        {
            return !Directory.Exists(path);
        }
    }
}