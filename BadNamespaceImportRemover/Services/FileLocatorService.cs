using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BadNamespaceImportRemover.Services
{
    public class FileLocatorService
    {
        readonly List<string> directoriesOfInterest;
        bool isRecursive;
        List<string> filesOfInterestPaths = new List<string>();

        public FileLocatorService(bool isRecursive, params string[] directoriesOfInterest)
        {
            this.directoriesOfInterest = new List<string>(directoriesOfInterest);
            this.isRecursive = isRecursive;
        }

        public IEnumerable<string> GetFilePaths()
        {
            foreach (var directory in directoriesOfInterest)
            {
                var filePaths = Directory.EnumerateFiles(directory, ".cs", new EnumerationOptions { RecurseSubdirectories = isRecursive });

                filesOfInterestPaths.AddRange(filePaths.Select(x => $"{directory}/{x}"));
            }

            return filesOfInterestPaths
        }
    }
}
