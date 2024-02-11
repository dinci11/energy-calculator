namespace EnergyCalculator.Extensions
{
    public static class FileInfoExtensions
    {
        public static FileInfo ChangeDirectory(this FileInfo file, string newDirectory)
        {
            var newPath = Path.Combine(newDirectory, file.Name);
            return new FileInfo(newPath);
        }

        public static FileInfo AddPrefix(this FileInfo file, string prefix)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.Name);
            var fileNameWithPrefix = string.Concat(fileName, prefix, file.Extension);
            var newPath = Path.Combine(file.Directory.FullName, fileNameWithPrefix);

            return new FileInfo(newPath);
        }

        public static bool IsXml(this FileInfo file)
        {
            return file.Extension == ".xml";
        }
    }
}