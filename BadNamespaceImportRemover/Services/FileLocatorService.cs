using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BadNamespaceImportRemover.Services
{
    public class FileLocatorService
    {
        readonly List<string> subDirectoriesOfInterest;
        readonly bool isRecursive;
        readonly string directoryPath;
        List<string> filesOfInterestPaths = new List<string>();

        public FileLocatorService(
            bool isRecursive,
            string directoryPath,
            params string[] subDirectoriesOfInterest)
        {
            this.subDirectoriesOfInterest = new List<string>(subDirectoriesOfInterest);
            this.isRecursive = isRecursive;
            this.directoryPath = directoryPath;
        }

        public IEnumerable<string> GetFilePaths()
        {
            foreach (var subDirectory in subDirectoriesOfInterest)
            {
                var subDirectoryPath = $"{directoryPath}\\{subDirectory}";
                var filePaths = Directory.EnumerateFiles(subDirectoryPath, "*.cs", new EnumerationOptions { RecurseSubdirectories = isRecursive });

                filesOfInterestPaths.AddRange(filePaths);
            }

            return filesOfInterestPaths;
        }
    }
}
